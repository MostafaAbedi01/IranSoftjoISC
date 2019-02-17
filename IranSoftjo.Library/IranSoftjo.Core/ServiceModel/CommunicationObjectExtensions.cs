using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Mehr.ServiceModel
{
    public static class CommunicationObjectExtensions
    {
        public static void DoAndClose<T>(this T client, Action<T> work)
        where T : ICommunicationObject
        {
            try
            {
                work(client);
                client.Close();
            }
            catch (CommunicationException)
            {
                client.Abort();
                throw;
            }
            catch (TimeoutException)
            {
                client.Abort();
                throw;
            }
            catch (Exception)
            {
                client.Abort();
                throw;
            }
        }

        public static T2 DoAndClose<T, T2>(this T client, Func<T, T2> work)
            where T : ICommunicationObject
        {
            try
            {
                var result = work(client);
                client.Close();
                return result;
            }
            catch (CommunicationException )
            {
                client.Abort();
                throw;
            }
            catch (TimeoutException )
            {
                client.Abort();
                throw;
            }
            catch (Exception )
            {
                client.Abort();
                throw;
            }
        }

    }
}
