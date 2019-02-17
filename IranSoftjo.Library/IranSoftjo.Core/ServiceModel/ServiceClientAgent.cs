using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Mehr;

namespace Mehr.ServiceModel
{
    public class ServiceClientAgent<TService> : IDisposable
        where TService : class, ICommunicationObject, new()
    {
        private TService service;
        public TService Service
        {
            get { return service ?? (service = new TService()); }
        }

        public void Dispose()
        {
            if (service != null)
                if (service.State != CommunicationState.Faulted)
                    service.Close();
                else
                    service.Abort();
        }
      
    }
}
