using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using IranSoftjo.Package.DataModel;
using Kendo.Mvc.Extensions;

namespace IranSoftjo.Package.WebUi.ViewModels
{
    public class TblChatVm
    {
        [Key]
        [Display(Name = "سریال")]
        public int TblChatId { get; set; }
        [Display(Name = "فرستنده")]
        public int TblUserIdFrom { get; set; }
        [Display(Name = "دریافت کننده")]
        public int TblUserIdTo { get; set; }
        [Display(Name = "متن پیام")]
        public string TextChat { get; set; }
        [Display(Name = "نام")]
        public string FirstName { get; set; }
        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; }

        public virtual User User { get; set; }
        [Display(Name = "تاریخ ارسال")]
        public DateTime? DateSabt { get; set; }
        public bool IsRead { get; set; }

        public static explicit operator TblChat(TblChatVm model)
        {
            var obj = new TblChat
            {
                TblUserIdFrom = model.TblUserIdFrom,
                TblChatId = model.TblChatId,
                TextChat = model.TextChat,
                DateSabt = model.DateSabt,
                IsRead = model.IsRead,
                TblUserIdTo = model.TblUserIdTo,

            };
            return obj;
        }

        public static explicit operator TblChatVm(TblChat model)
        {
            var obj = new TblChatVm
            {
                TblUserIdFrom = (int) model.TblUserIdFrom,
                TblChatId = model.TblChatId,
                TextChat = model.TextChat,
                DateSabt = model.DateSabt,
                IsRead = model.IsRead,
                TblUserIdTo = (int) model.TblUserIdTo,
            };

            return obj;
        }

        public static IEnumerable<TblChatVm> ToIEnumerable(IEnumerable<TblChat> models)
        {
            var TblChat = models.Select(model => new TblChatVm
            {
                TblUserIdFrom = (int) model.TblUserIdFrom,
                TblChatId = model.TblChatId,
                TextChat = model.TextChat,
                DateSabt = model.DateSabt,
                IsRead = model.IsRead,
                TblUserIdTo = (int) model.TblUserIdTo,
            });
            return TblChat;
        }
    }
}