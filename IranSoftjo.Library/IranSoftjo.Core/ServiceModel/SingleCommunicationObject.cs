using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Mehr.ServiceModel
{
    public class SingleCommunicationObject : IDisposable
    {
        Func<ICommunicationObject> builder;
        public SingleCommunicationObject(Func<ICommunicationObject> builder)
        {
            this.builder = builder;
        }

        ICommunicationObject client;
        public ICommunicationObject Client
        {
            get
            {
                if (client == null)
                    client = builder();
                return client;
            }
        }

        public event EventHandler<ExceptionOccuredEventArgs> ExceptionOccurred;

        public void Do(Action<ICommunicationObject> action)
        {
            try
            {
                if (client != null &&
                    client.State != CommunicationState.Opened)
                {
                    client.Abort();
                    client = null;
                }
                action(Client);
            }
            catch (Exception ex)
            {
                if (ExceptionOccurred != null)
                    ExceptionOccurred(this, new ExceptionOccuredEventArgs(ex));

                if (client != null)
                {
                    try { client.Close(); }
                    catch (Exception) { client.Abort(); }
                    client = null;
                }
                action(Client);
            }
        }

        public void Dispose()
        {
            try
            {
                if (client != null)
                    client.Close();
            }
            catch (Exception ex)
            {
                if (ExceptionOccurred != null)
                    ExceptionOccurred(this, new ExceptionOccuredEventArgs(ex));

                if (client != null)
                    client.Abort();
                client = null;
            }
        }

        public class ExceptionOccuredEventArgs : EventArgs
        {
            public Exception OccurredException { get; private set; }
            public ExceptionOccuredEventArgs(Exception OccurredException)
            { this.OccurredException = OccurredException; }
        }
    }
}
