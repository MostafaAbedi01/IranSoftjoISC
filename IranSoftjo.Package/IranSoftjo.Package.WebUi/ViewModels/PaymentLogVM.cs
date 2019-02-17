using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using IranSoftjo.Package.DataModel;
using Mehr;
using Mehr.Reflection;

namespace IranSoftjo.Package.WebUi.ViewModels
{
    public class PaymentLogVM
    {
        [Key]
        [Display(Name = "سریال پرداخت")]
        public int PaymentLogID { get; set; }

        [Display(Name = "شماره فاکتور")]
        public int? OrderID { get; set; }

        [Display(Name = "مبلغ")]
        public decimal? Amount { get; set; }

        [Display(Name = "سریال تراکنش")]
        public string TrackingCode { get; set; }

        [Display(Name = "تاریخ پرداخت")]
        public DateTime? PaymentDate { get; set; }

        [Display(Name = "نام بانک")]
        public BankTypeEnum BankType { get; set; }
        [Display(Name = "نام بانک")]
        public string BankTypeVal { get; set; }

        [Display(Name = "نوع پرداخت")]
        public PaymentTypeEnum PaymentType { get; set; }

        [Display(Name = "نوع پرداخت")]
        public string PaymentTypeVal { get; set; }

        [Display(Name = "وضعیت")]
        public bool IsSuccessful { get; set; }

        [Display(Name = "پیام پرداخت")]
        public string PaymentResponseMessage { get; set; }

        [Display(Name = "نام کاربر")]
        public string UserName { get; set; }

        public static IEnumerable<PaymentLogVM> ToIEnumerable(IEnumerable<PaymentLog> models)
        {
            var enumMetadataFactory = ServiceLocator.ResolveOnCurrentInstance<IEnumMetadataFactory>();
            var agency = models.Select(model => new PaymentLogVM
            {
                OrderID = model.OrderID,
                UserName=model.User.Username,
                PaymentDate = model.PaymentDate,
                BankTypeVal = enumMetadataFactory.GetCaption((BankTypeEnum)model.BankType),
                PaymentTypeVal = enumMetadataFactory.GetCaption((PaymentTypeEnum)model.PaymentType),
                IsSuccessful = model.IsSuccessful != null && (bool)model.IsSuccessful,
                Amount = model.Amount,
                BankType = model.BankTypeEnum,
                TrackingCode = model.TrackingCode,
                PaymentResponseMessage = model.PaymentResponseMessage,
                PaymentLogID = model.PaymentLogID,
            });
            return agency;
        }
    }
}