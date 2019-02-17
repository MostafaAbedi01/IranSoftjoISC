using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.SessionState;
using IranSoftjo.Package.DataModel;
using IranSoftjo.Package.WebUi.ViewModels;
using Mehr;

namespace IranSoftjo.Package.WebUi.Controllers
{
    public class ShoppingCartApiController : ApiController
    {
        private readonly Entities _db = new Entities();

        public string Get()
        {
            HttpSessionState session = HttpContext.Current.Session;
            string htm = "<table width='100%'>";
            if (session["ShoppingCartItems"] != null)
            {
                var productsInCart = session["ShoppingCartItems"] as List<ProductInShoppingCartVM>;
                if (productsInCart != null)
                {
                    int sumProducts = productsInCart.Sum(d => d.ProductPrice * d.ProductCount);
                    int sumProductCount = productsInCart.Sum(d => d.ProductCount);
                    htm = productsInCart.Aggregate(htm,
                        (current, item) =>
                            current +
                            ("<tr><td>  <a href='/Product/Details/" + item.ProductID + "#" + item.ProductTitle + " style='color: #005B85;'>" +
                                        item.ProductTitle + "</a></td><td>" 
                                    + item.ProductCount.LocalizeNumbers() + "</td><td>" + item.ProductPrice.NumericalMoney() +
                             "</td></tr>"));
                    htm += " <tr class='BoxPrice'><td>جمع کل : </td><td><b>" + sumProductCount.LocalizeNumbers() + "</b></td><td><b>" + sumProducts.NumericalMoney() + "</b></td></tr> </table>";
                }
            }
            return htm;
        }

        public string Get(int ProductID)
        {
            HttpSessionState session = HttpContext.Current.Session;
            var products = new List<ProductInShoppingCartVM>();
            Product product = _db.Products.FirstOrDefault(d => d.ProductID == ProductID);
            if (session["ShoppingCartItems"] != null)
            {
                products = session["ShoppingCartItems"] as List<ProductInShoppingCartVM>;

                ProductInShoppingCartVM selected = products.Find(p => p.ProductID == ProductID);

                if (selected == null)
                {
                    selected = new ProductInShoppingCartVM
                               {
                                   ProductID = ProductID,
                                   ProductPrice = product.ProductPrice,
                                   ProductTitle = product.ProductTitle,
                                   ProductCount = 1
                               };
                    products.Add(selected);
                }
                else
                {
                    selected.ProductCount++;
                }
                session["ShoppingCartItems"] = products;
            }
            else
            {
                if (product != null)
                {
                    var selected = new ProductInShoppingCartVM
                                   {
                                       ProductID = ProductID,
                                       ProductPrice = product.ProductPrice,
                                       ProductTitle = product.ProductTitle,
                                       ProductCount = 1
                                   };
                    products.Add(selected);
                }
                session["ShoppingCartItems"] = products;
            }
            return products.Sum(productInShoppingCart => productInShoppingCart.ProductCount).LocalizeNumbers();
        }
    }
}