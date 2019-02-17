using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using IranSoftjo.Package.DataModel;
using Mehr;
using Mehr.Reflection;

namespace IranSoftjo.Package.WebUi.ViewModels
{
    public class OrderVM
    {
        [Key]
        [Display(Name = "شماره فاکتور")]
        public int OrderID { get; set; }

        public int UserID { get; set; }

        public DateTime OrderDate { get; set; }
        [Display(Name = "تاریخ فاکتور")]
        public string OrderDateLocalize
        { get { return OrderDate.LocalizeNumbers(); } }

        [Display(Name = "وضعیت پرداخت")]
        public IsFinalizedEnum IsFinalized { get; set; }

        [Display(Name = "وضعیت پرداخت")]
        public string IsFinalizedType { get; set; }

        public int OrderTotal { get; set; }

        [Display(Name = "مبلغ کل فاکتور")]
        public string OrderTotalLocalize
        { get { return OrderTotal.NumericalMoney(); } }

        [Display(Name = "نام کاربر")]
        public string UserName { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public static IEnumerable<OrderVM> ToIEnumerable(IEnumerable<Order> models)
        {
            var db = new Entities();
            var enumMetadataFactory = ServiceLocator.ResolveOnCurrentInstance<IEnumMetadataFactory>();
            var agency = models.Select(model => new OrderVM
            {
                OrderID = model.OrderID,
                OrderDate = model.OrderDate,
                UserName = model.User.Username,
                IsFinalizedType = enumMetadataFactory.GetCaption((IsFinalizedEnum)model.IsFinalized),
                IsFinalized = model.IsFinalizedEnum,
                OrderTotal = (from od in db.OrderDetails
                              where od.OrderID == model.OrderID
                              select od.PriceUnit * od.OrderedCount).Sum(),
                OrderDetails = model.OrderDetails
            });
            return agency;
        }
    }
}