using System.Linq;
using System.Net;
using IranSoftjo.Common;
using IranSoftjo.Package.DataModel;
using IranSoftjo.Package.WebUi.Android.ZarinPal;


namespace IranSoftjo.Package.WebUi.Payment
{
    public class ZarinpalPayment
    {
        public bool ProcessPayment(int amount, string description, string callbackUrl, string accountNumberOnline, out string outAuthority)
        {
            if (amount > 0)
            {
                ServicePointManager.Expect100Continue = false;
                var zp = new PaymentGatewayImplementationServicePortTypeClient();
                string authority;
                int status = zp.PaymentRequest(accountNumberOnline, amount / 10,
                    description, "iransoftjo@gmail.com", "09124094634", callbackUrl,
                    out authority);
                outAuthority = authority;
                if (status == 100)
                {
                    return true;
                }
                outAuthority = GetValueError(status);
                return false;
            }
            outAuthority = "میلغ وارد شده صحیح نمی باشد";
            return true;
        }

        public PaymentResponse ProcessResponse(string requestAuthority, string requestStatus, int amount)
        {
            var paymentResponse = new PaymentResponse();
            if (!string.IsNullOrEmpty(requestStatus) && !string.IsNullOrEmpty(requestAuthority))
            {
                if (requestStatus.Equals("OK"))
                {
                    var db = new Entities();
                    var siteSettings = db.SiteSettings.FirstOrDefault();
                    if (siteSettings != null)
                    {
                        long refID;
                        ServicePointManager.Expect100Continue = false;
                        var zp = new PaymentGatewayImplementationServicePortTypeClient();
                        int status = zp.PaymentVerification(siteSettings.AccountNumberOnline,
                            requestAuthority, amount / 10, out refID);
                        if (status == 100)
                        {
                            paymentResponse.Successful = true;
                            paymentResponse.ResponseMessage = string.Format(
                                " شماره تراکنش زرین پال  : {0} ", refID);
                            paymentResponse.RefID = refID;
                        }
                        else
                        {
                            paymentResponse.Successful = false;
                            paymentResponse.ResponseMessage =
                                string.Format("شماره خطا {0}  : خطا در اتصال به دروازه پرداخت ", status);
                            paymentResponse.ResponseMessage = GetValueError(status);
                        }
                    }
                }
                else
                {
                    paymentResponse.Successful = false;
                    paymentResponse.ResponseMessage = "خطا ! پرداخت با تراکنش: " + requestAuthority + " موفق نبود و وضعیت برگشت  : " + requestStatus + " است";
                }
            }
            else
            {
                paymentResponse.Successful = false;
                paymentResponse.ResponseMessage = "داده وارد شده اشتباه است";
            }
            return paymentResponse;
        }

        private string GetValueError(int status)
        {
            if (status == -1)
            {
                return string.Format("شماره خطا {0}  : اطلاعات ارسال شده ناقص است ", status);
            }
            if (status == -2)
            {
                return
                    string.Format("شماره خطا {0}  : کد پذیرنده یا آی پی صحیح نیست ", status);
            }
            if (status == -3)
            {
                return
                    string.Format("شماره خطا {0}  : پرداخت با رقم درخواست شده امکان پذیر نیست ", status);
            }
            if (status == -4)
            {
                return
                    string.Format("شماره خطا {0}  : سطح پذیرنده از نقره ای پایین تر است ", status);
            }
            if (status == -11)
            {
                return
                    string.Format("شماره خطا {0}  : درخواست مورد نظر یافت نشد ", status);
            }
            if (status == -21)
            {
                return
                    string.Format("شماره خطا {0}  : هیچ عملیات مالی برای این تراکنش یافت نشد ", status);
            }
            if (status == -22)
            {
                return
                    string.Format("شماره خطا {0}  : تراکنش ناموفق میباشد ", status);
            }
            if (status == -22)
            {
                return string.Format("شماره خطا {0}  : تراکنش ناموفق میباشد ", status);
            }
            if (status == -33)
            {
                return string.Format("شماره خطا {0}  : رقم تراکنش با رقم پرداخت تطابق ندارد ", status);
            }
            if (status == -34)
            {
                return string.Format("شماره خطا {0}  : سقف تراکنش از لحاظ تعداد یا رقم عبور نموده است ", status);
            }
            if (status == -40)
            {
                return string.Format("شماره خطا {0}  : اجازه دسترسی به متد مربوطه امکان ندارد ", status);
            }
            if (status == -41)
            {
                return string.Format("شماره خطا {0}  : اطلاعات AdditionalData معتبر نمیباشد ", status);
            }
            if (status == -54)
            {
                return string.Format("شماره خطا {0}  :درخواست آرشیو شده است   ", status);
            }
            return string.Format("شماره خطا {0}  : خطا در اتصال به دروازه پرداخت ", status);
        }
    }
}