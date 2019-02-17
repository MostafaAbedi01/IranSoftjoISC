using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mehr;

namespace Mehr
{
    public static class CellPhoneLogic
    {
        [Obsolete("Use UnifyPhoneFormat instead.")]
        public static string UnifyCellPhoneFormat(string phone)
        { return UnifyPhoneFormat(phone); }

        public static string UnifyPhoneFormat(string phone, bool includeIranAreaCode = true)
        {
            phone = phone.TrimSignleWord();
            if (phone != null)
                if (phone.Length == 10)
                    phone = "98" + phone;
                else if (phone.Length == 11 && phone[0] == '0')
                    phone = "98" + phone.Substring(1);
                else if (phone.Length > 12)
                    if (phone.StartsWith("00"))
                        phone = phone.Substring(2);
                    else if (phone[0] == '+')
                        phone = phone.Substring(1);
            if (!includeIranAreaCode)
                phone = phone.TrimStart("98");
            return phone;
        }

        public static bool IsValid(string phone, bool includeIranAreaCode = true)
        {
            if (includeIranAreaCode)
                phone = phone.TrimStart("98");

            long val;
            return phone != null && phone.Length == 10  && long.TryParse(phone, out val);
        }

        public static bool IsIrCellPhone(string cellPhone, bool inludeIranAreaCode = true)
        {
            if (inludeIranAreaCode)
                cellPhone = cellPhone.TrimStart("98");

            return cellPhone.Length == 10 && cellPhone.StartsWith("9");
        }

        public static long? ConvertToUnifiedIrCellPhone(string cellPhone, long? defaultValue = null, bool includeIranAreaCode = true)
        {
            var shaped = UnifyPhoneFormat(cellPhone, includeIranAreaCode);
            if (IsValid(shaped, includeIranAreaCode) && IsIrCellPhone(shaped, includeIranAreaCode))
                return long.Parse(shaped);

            return defaultValue;
        }
    }
}
