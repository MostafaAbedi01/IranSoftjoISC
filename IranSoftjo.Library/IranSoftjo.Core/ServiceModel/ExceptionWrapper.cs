using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Mehr.ServiceModel
{
    public static class ExceptionWrapper
    {
        static readonly string InternalErrorCode = CommonFaultCode.InternalError.ToString();

        public static void Do(Action action)
        {
            var igonre = Do(()=>
                {
                    action();
                    return default(string);
                });
        }

        public static T Do<T>(Func<T> action)
        {
            try
            {
                return action();
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new FaultException<FaultData>(new FaultData()
                {
                    Code = InternalErrorCode,
                    Detail = ex.ToString(),
                }, new FaultReason(InternalErrorCode));
            }
        }

    }
}
