using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mehr
{
    public static class CommonRegex
    {
        public const string JustNumber = @"^[0-9]*$";

        public const string PostalCode = @"^[1-9][0-9]{9}$";

        public const string NumbericIrPhone = @"^0?[1-8][0-9]{9}$";

        public const string GlobalCellPhone = @"^(00|\+)?[0-9]{12}$";

        public const string IrCellPhone = @"^(0|98|\+98|0098)?9[123]{1}[0-9]{8}$";

        public const string IrOrGlobalCellPhone = " *(" + IrCellPhone + "|" + GlobalCellPhone + ") *";

        public const string Email = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";

        public const string MultiSemicolonSeperatedEmails = @"^" + Email + "(([;])" + Email + ")*$";

        public const string Latin = @"^[\w\d\s.,-]*$";//@"^[a-zA-z0-9!@#\-\+]*$";

        public const string Time = @"^(2[0-3]|[01]\d|\d)(:[0-5]?\d)(:[0-5]?\d)?$";

        public const string Ip =
            @"(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\." +
            @"(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\." +
            @"(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\." +
            @"(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)";

    }

    public static class CommonRegexMessage
    {
        public const string JustNumber = "لطفا فقط عدد وارد نمایید.";
        public const string Email = "فرمت ایمیل های وارد شده صحیح نیست.";
        public const string Latin = "لطفا فقط حروف لاتین وارد نمایید";
        public const string Time = "قرمت ساعت وارد شده صحیح نیست.";
        public const string PostalCode = "قرمت کد پستی وارد شده صحیح نیست.";

    }
}

