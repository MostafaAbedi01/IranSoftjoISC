using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using IranSoftjo.Common;
using IranSoftjo.Core.Web.Mvc;
using IranSoftjo.Package.DataModel;
using IranSoftjo.Package.WebUi.Payment;
using IranSoftjo.Package.WebUi.ViewModels;
using Mehr;
using Mehr.Reflection;
using Mehr.Web.Mvc;

namespace IranSoftjo.Package.WebUi.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly Entities _db = new Entities();

        public ActionResult Index()
        {
            List<Product> lstProducts = _db.Products.ToList();
            var cartcontents = new List<ShoppingCartVM>();

            if (Session["ShoppingCartItems"] != null)
            {
                var productsInCart = Session["ShoppingCartItems"] as List<ProductInShoppingCartVM>;

                cartcontents = (from pr in lstProducts
                                join pic in productsInCart on pr.ProductID equals pic.ProductID
                                select new ShoppingCartVM
                                       {
                                           ProductID = pr.ProductID,
                                           ProductTitle = pr.ProductTitle,
                                           ProductPrice = pr.ProductPrice,
                                           ProductCount = pic.ProductCount,
                                           RowTotal = (pr.ProductPrice * pic.ProductCount)
                                       }).ToList();
                return View(cartcontents);
            }
            return View(cartcontents);
        }

        public ActionResult SubCount(int id)
        {
            if (Session["ShoppingCartItems"] != null)
            {
                var productsInCart = Session["ShoppingCartItems"] as List<ProductInShoppingCartVM>;

                int index = productsInCart.FindIndex(p => p.ProductID == id);

                ProductInShoppingCartVM item = productsInCart[index];
                if (item.ProductCount == 1)
                {
                    productsInCart.RemoveAt(index);
                }
                else
                {
                    item.ProductCount--;
                    productsInCart[index] = item;
                }
                Session["ShoppingCartItems"] = productsInCart;
            }
            return RedirectToAction("Index");
        }

        public ActionResult AddCount(int id)
        {
            if (Session["ShoppingCartItems"] != null)
            {
                var productsInCart = Session["ShoppingCartItems"] as List<ProductInShoppingCartVM>;

                int index = productsInCart.FindIndex(p => p.ProductID == id);

                ProductInShoppingCartVM item = productsInCart[index];
                item.ProductCount++;
                productsInCart[index] = item;
                Session["ShoppingCartItems"] = productsInCart;
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            if (Session["ShoppingCartItems"] != null)
            {
                var productsInCart = Session["ShoppingCartItems"] as List<ProductInShoppingCartVM>;
                int index = productsInCart.FindIndex(p => p.ProductID == id);
                productsInCart.RemoveAt(index);
                Session["ShoppingCartItems"] = productsInCart;
            }
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult DeleteFactor(int id)
        {
            try
            {
                var lstOrderDetails = _db.OrderDetails.Where(d => d.OrderID == id).ToList();
                foreach (var item in lstOrderDetails)
                {
                    _db.OrderDetails.Remove(item);
                }
                var order = _db.Orders.FirstOrDefault(d => d.OrderID == id);
                _db.Orders.Remove(order);
                _db.SaveChanges();
                TempData.SetMessage("فاکتور به شماره " + id + " با موفقیت حذف شد");
            }
            catch (Exception ex)
            {
                TempData.SetMessage(ex.Message, MessageType.Error);
            }
            return RedirectToAction("Orders");
        }

        [Authorize]
        public ActionResult FinalizeShopping()
        {
            if (Session["ShoppingCartItems"] != null)
            {
                var productsInCart = Session["ShoppingCartItems"] as List<ProductInShoppingCartVM>;
                List<Product> dbProducts = _db.Products.ToList();
                List<ShoppingCartVM> cartcontents = (from pr in dbProducts
                                                     join pic in productsInCart on pr.ProductID equals pic.ProductID
                                                     select new ShoppingCartVM
                                                            {
                                                                ProductID = pr.ProductID,
                                                                ProductTitle = pr.ProductTitle,
                                                                ProductPrice = pr.ProductPrice,
                                                                ProductCount = pic.ProductCount,
                                                                RowTotal = pr.ProductPrice * pic.ProductCount,
                                                            }).ToList();

                string username = User.Identity.Name;
                User user = _db.Users.FirstOrDefault(u => u.Username == username);
                if (user != null)
                {
                    int userid = user.UserID;
                    var order = new Order
                                {
                                    UserID = userid,
                                    OrderDate = DateTime.Now,
                                    IsFinalizedEnum = IsFinalizedEnum.Unpaid,
                                    //DomainName = domainName,
                                    //AgencyId = agencyId,
                                };
                    _db.Orders.Add(order);
                    foreach (ShoppingCartVM item in cartcontents)
                    {
                        _db.OrderDetails.Add(new OrderDetail
                                             {
                                                 OrderID = order.OrderID,
                                                 ProductID = item.ProductID,
                                                 OrderedCount = item.ProductCount,
                                                 PriceUnit = item.ProductPrice
                                             });
                    }
                    _db.SaveChanges();
                }
                Session["ShoppingCartItems"] = null;
            }
            return RedirectToAction("Orders");
        }

        [Authorize]
        public ActionResult Orders()
        {
            string username = User.Identity.Name;
            User user = _db.Users.FirstOrDefault(u => u.Username == username);
            if (user != null)
            {
                int userid = user.UserID;
                IEnumerable<OrderVM> orders =
                    OrderVM.ToIEnumerable(_db.Orders.OrderByDescending(d => d.OrderID).Where(d => d.UserID == userid));
                return View(orders);
            }
            return View();
        }

        [Authorize]
        public ActionResult OnlinePayment(int id)
        {
            var siteSettings = _db.SiteSettings.FirstOrDefault();
            int orderTotal = (from od in _db.OrderDetails
                              join p in _db.Products
                                  on od.ProductID equals p.ProductID
                              where od.OrderID == id
                              select od.OrderedCount * od.PriceUnit).Sum();

            string authority;
            bool zarinpalPayment = new ZarinpalPayment().ProcessPayment(orderTotal, "پرداخت وجه خرید",
                  siteSettings.WebSiteName + "/ShoppingCart/PaymentVerification?orderId=" + id, siteSettings.AccountNumberOnline, out authority);
            if (zarinpalPayment)
            {
                return Redirect("https://www.zarinpal.com/pg/StartPay/" + authority);
            }
            TempData.SetMessage(string.Format("شماره خطا {0}  : خطا در اتصال به دروازه پرداخت ", authority),
                MessageType.Error);
            return RedirectToAction("Orders");
        }

        [Authorize]
        public ActionResult PaymentVerification(int orderId)
        {
            int ordertotal = (from od in _db.OrderDetails
                              join pr in _db.Products
                                  on od.ProductID equals pr.ProductID
                              where od.OrderID == orderId
                              select od.OrderedCount * pr.ProductPrice).Sum();

            PaymentResponse zarinpalPayment = new ZarinpalPayment().ProcessResponse(Request.QueryString["Authority"],
                Request.QueryString["Status"], ordertotal);

            string userName = User.Identity.Name;
            User user = _db.Users.FirstOrDefault(d => d.Username == userName);
            if (user != null)
            {
                var paymentLogs = new PaymentLog
                                  {
                                      UserID = user.UserID,
                                      Amount = ordertotal,
                                      BankTypeEnum = BankTypeEnum.Zarinpal,
                                      IsSuccessful = zarinpalPayment.Successful,
                                      PaymentDate = DateTime.Now,
                                      PaymentTypeEnum = PaymentTypeEnum.PaymentOnline,
                                      PaymentResponseMessage = "خرید با پرداخت آنلاین",
                                      PaymentResponseCode = zarinpalPayment.ResponseMessage,
                                      TrackingCode = zarinpalPayment.RefID.ToString(),
                                  };
                _db.PaymentLogs.Add(paymentLogs);
                if (zarinpalPayment.Successful)
                {
                    TempData.SetMessage(zarinpalPayment.ResponseMessage, MessageType.Success);
                    Order order = _db.Orders.FirstOrDefault(o => o.OrderID == orderId);
                    if (order != null)
                        order.IsFinalizedEnum = IsFinalizedEnum.Paid;
                }
                else
                {
                    TempData.SetMessage(zarinpalPayment.ResponseMessage, MessageType.Error);
                }
                _db.SaveChanges();
            }
            else
            {
                TempData.SetMessage(zarinpalPayment.ResponseMessage, MessageType.Error);
            }

            return View(zarinpalPayment);
        }

        [HttpGet]
        [Authorize]
        public ActionResult BalancePayment(int id)
        {
            var increaseBalance = new IncreaseBalanceVM();
            string username = User.Identity.Name;
            User user = _db.Users.FirstOrDefault(u => u.Username == username);
            if (user != null)
                increaseBalance.AccountBalance = user.AccountBalance.NumericalMoney();
            int ordertotal = (from od in _db.OrderDetails
                              join pr in _db.Products
                                  on od.ProductID equals pr.ProductID
                              where od.OrderID == id
                              select od.OrderedCount * od.PriceUnit).Sum();
            increaseBalance.Amount = ordertotal;
            increaseBalance.OrderID = id;
            return View(increaseBalance);
        }

        [HttpPost]
        [Authorize]
        public ActionResult BalancePayment(IncreaseBalanceVM model)
        {
            string username = User.Identity.Name;
            User user = _db.Users.FirstOrDefault(u => u.Username == username);
            Order order = _db.Orders.FirstOrDefault(u => u.OrderID == model.OrderID);

            if (user != null && order != null)
            {
                if (user.AccountBalance < model.Amount)
                {
                    TempData.SetMessage("موجودی حساب شما کافی نمیباشد.", MessageType.Error);
                    return View(model);
                }
                user.AccountBalance = user.AccountBalance - model.Amount;
                //order.IsFinalizedEnum = IsFinalizedEnum.Paid;
                _db.SaveChanges();
                TempData.SetMessage(
                    string.Format("پرداخت فاکتور {0} با موفقیت انجام شد.", model.OrderID.LocalizeNumbers()),
                    MessageType.Success);
                return RedirectToAction("Orders");
            }
            TempData.SetMessage("خطا در پرداخت.", MessageType.Error);
            return View(model);
        }

        [HttpGet]
        [Authorize]
        public ActionResult PosPayment(int id)
        {
            var increaseBalance = new PosPaymentVM();
            string username = User.Identity.Name;
            User user = _db.Users.FirstOrDefault(u => u.Username == username);
            if (user != null)
                increaseBalance.AccountBalance = user.AccountBalance.NumericalMoney();
            int ordertotal = (from od in _db.OrderDetails
                              join pr in _db.Products
                                  on od.ProductID equals pr.ProductID
                              where od.OrderID == id
                              select od.OrderedCount * od.PriceUnit).Sum();
            increaseBalance.Amount = ordertotal;
            increaseBalance.OrderID = id;
            var enumMetadataFactory = ServiceLocator.ResolveOnCurrentInstance<IEnumMetadataFactory>();
            Dictionary<PaymentTypeEnum, string> item = enumMetadataFactory.Get<PaymentTypeEnum>().Items;
            var lstselect =
                item.ToDictionary(variable => (int)variable.Key, variable => variable.Value);
            increaseBalance.SelectListType = lstselect.ToSelectList();
            return View(increaseBalance);
        }

        [HttpPost]
        [Authorize]
        public ActionResult PosPayment(PosPaymentVM model)
        {
            return RedirectToAction("Orders");
        }

        [HttpGet]
        [Authorize]
        public ActionResult PaymentLogs()
        {
            var enumMetadataFactory = ServiceLocator.ResolveOnCurrentInstance<IEnumMetadataFactory>();
            string usrName = User.Identity.Name;
            var listpaymentLogs = new List<PaymentLogVM>();
            foreach (var paymentLogs in _db.PaymentLogs.Where(d => d.User.Username == usrName))
            {
                listpaymentLogs.Add(new PaymentLogVM
                                    {
                                        Amount = paymentLogs.Amount,
                                        BankType = paymentLogs.BankTypeEnum,
                                        BankTypeVal = enumMetadataFactory.GetCaption(paymentLogs.BankTypeEnum),
                                        TrackingCode = paymentLogs.TrackingCode,
                                        IsSuccessful = paymentLogs.IsSuccessful != null && (bool)paymentLogs.IsSuccessful,
                                        PaymentResponseMessage = paymentLogs.PaymentResponseMessage,
                                        PaymentTypeVal = enumMetadataFactory.GetCaption(paymentLogs.PaymentTypeEnum),
                                        PaymentDate = paymentLogs.PaymentDate,
                                        OrderID = paymentLogs.OrderID,
                                        PaymentLogID = paymentLogs.PaymentLogID,
                                    });
            }
            return View(listpaymentLogs.ToList());
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult PaymentAllLogs()
        {
            return View(PaymentLogVM.ToIEnumerable(_db.PaymentLogs).ToList());
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult AllOrders()
        {
            IEnumerable<OrderVM> orders =
                OrderVM.ToIEnumerable(_db.Orders.OrderByDescending(d => d.OrderID));
            return View(orders);
        }
    }
}