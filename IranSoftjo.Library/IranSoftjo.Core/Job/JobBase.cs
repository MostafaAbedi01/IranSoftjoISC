using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Mehr.Setting;

namespace Mehr.Job
{
    public abstract class JobBase : IJob
    {
        public static readonly PackSettingReader SettingReader = new PackSettingReader("Jobs");

        public string JobName { get { return this.GetType().Name; } }

        public bool IsDisabled { get { return isDisabledInternal.Value; } }

        private Lazy<bool> isDisabledInternal;
        public JobBase()
        { isDisabledInternal = new Lazy<bool>(() => SettingReader.Get(JobName + ".IsDisabled", false)); }

        protected TraceSource TraceSource { get; set; }
        public virtual void Do()
        {
            TraceSource = new TraceSource(this.GetType().Assembly.GetName().Name);
            var baseTraceSource = new TraceSource(typeof(JobBase).Assembly.GetName().Name);

            if (this.IsDisabled)
            {
                baseTraceSource.TraceEvent(TraceEventType.Information, 200000, "{0} : IsDisabled", this.GetType().Name);
                return;
            }

            try
            {
                baseTraceSource.TraceEvent(TraceEventType.Start, 100000, this.GetType().Name);
                BeforeIteration();
                string result = DoAction();
                AfterIteration();
                baseTraceSource.TraceEvent(TraceEventType.Stop, 800000, "{0}:{1}", this.GetType().Name, result);
            }
            catch (Exception ex)
            {
                baseTraceSource.TraceEvent(TraceEventType.Critical, 900000, ex.ToString());
            }
            finally
            {
                TraceSource.Close();
                baseTraceSource.Close();
            }
        }

        protected virtual void BeforeIteration() { }
        protected virtual void AfterIteration() { }
        protected abstract string DoAction();
    }
}
