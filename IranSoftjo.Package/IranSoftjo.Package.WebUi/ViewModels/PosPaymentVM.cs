using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace IranSoftjo.Package.WebUi.ViewModels
{ 
    public class PosPaymentVM :IncreaseBalanceVM
    {
             [Display(Name = "نوع پرداخت")]
           //  [FilterEnum(PaymentTypeEnum.BankDraft)]
        public SelectList SelectListType { get; set; }

        [Display(Name = "نوع پرداخت")]
        public int PaymentType { get; set; }

        [Display(Name = "تاریخ پرداخت")]
        public DateTime PaymentDate { get; set; }

        [Display(Name = "شماره فیش یا تراکنش")]
        public string TrackingCode { get; set; }

        [Display(Name = "توضیحات")]
        public string PaymentResponseMessage { get; set; }
    }
}