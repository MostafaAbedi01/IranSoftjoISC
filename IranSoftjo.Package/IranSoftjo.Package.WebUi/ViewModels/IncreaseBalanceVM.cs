using System.ComponentModel.DataAnnotations;

namespace IranSoftjo.Package.WebUi.ViewModels
{
    public class IncreaseBalanceVM
    {
        [Key]
        public int UserID { get; set; }

        public int OrderID { get; set; }

        [Display(Name = "مبلغ")]
        [UIHint("Currency")]
        public int Amount { get; set; }

        [Display(Name = "موجودی اکنون حساب شما")]
        public string AccountBalance { get; set; }

    }
}