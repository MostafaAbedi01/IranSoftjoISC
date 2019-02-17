using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Mehr.ServiceModel
{
    public interface IAuthorizationContext
    {
        string GetClientIp();
        void AuthorizeIp(string validIps);
    }

    public class AuthorizationContext : IAuthorizationContext
    {
        private readonly static string InternalErrorCode = CommonFaultCode.InternalError.ToString();
        private readonly static string InvalidAccessCode = CommonFaultCode.InvalidAccess.ToString();

        public readonly static AuthorizationContext Instance = new AuthorizationContext();

        public string GetClientIp()
        {
            var messageProperties = OperationContext.Current.IncomingMessageProperties;
            var endpointProperty = messageProperties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            return endpointProperty.Address;
        }

        public void AuthorizeIp(string validIps)
        {
            try
            {
                var clientIp = GetClientIp();
                if (!validIps.Contains(clientIp))
                {
                    throw new FaultException<FaultData>(new FaultData()
                    {
                        Code = InvalidAccessCode,
                        Detail = "Invalid caller ip '" + clientIp + "'.",
                    }, new FaultReason("Insufficient access!"));
                }
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
