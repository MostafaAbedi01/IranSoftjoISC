using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Mehr;

namespace IranSoftjo.Package.WebUi.ViewModels
{
    public class ShoppingCartVM
    {
        [Key]
        public int ProductID { get; set; }
        
        [Display(Name = "نام کالا")]
        public string ProductTitle { get; set; }
        
        public int ProductPrice { get; set; }
        [Display(Name = "قیمت کالا")]
        public string ProductPriceLocalize
        {get { return ProductPrice.NumericalMoney(); }}

        public int ProductCount { get; set; }
        [Display(Name = "تعداد")]
        public string ProductCountLocalize
        { get { return ProductCount.LocalizeNumbers(); } }

        public int RowTotal { get; set; }
        [Display(Name = "جمع")]
        public string RowTotalLocalize
        { get { return RowTotal.NumericalMoney(); } }

        public int? AgencyId { get; set; }

    }
}