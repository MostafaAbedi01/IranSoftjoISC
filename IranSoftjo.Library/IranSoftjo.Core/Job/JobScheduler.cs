using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Mehr.Setting;

namespace Mehr.Job
{
    public interface IJobScheduler
    {
        void Schedule();
    }

    public class JobScheduler<JobT> : IJobScheduler
            where JobT : IJob, new()
    {
        public static readonly PackSettingReader SettingReader = JobBase.SettingReader;

        private Timer timer = null;
        public TimeSpan DueTime { get; set; }
        public TimeSpan Period { get; set; }
        public Func<bool> RunCondition { get; set; }
        public bool MultipleRunningInstance { get; set; }

        public JobScheduler(TimeSpan dueTime, TimeSpan period)
        {
            this.DueTime = dueTime;
            this.Period = period;
        }

        public static JobScheduler<JobT> BuildDaily(TimeSpan runTime, Func<bool> runCondition = null)
        {
            runTime = SettingReader.Get(typeof(JobT).Name + ".RunTime", runTime);

            var dueTime = (runTime - DateTime.Now.TimeOfDay);
            if (runTime < DateTime.Now.TimeOfDay)
                dueTime = TimeSpan.FromHours(24) - dueTime.Duration();
            return new JobScheduler<JobT>(
                 dueTime: dueTime,
                 period: TimeSpan.FromDays(1))
                 {
                     RunCondition = runCondition,
                 };
        }

        public JobScheduler<JobT> SetRunDay(bool monthIsOdd, int day)
        {
            var monthIsOddValue = monthIsOdd ? 1 : 0;
            return SetRunDay(monthIsOddValue, day);
        }

        public JobScheduler<JobT> SetRunDay(int monthIsOddValue, int day)
        {
            this.RunCondition =
                () => PersianDateTime.Now.Day == day && PersianDateTime.Now.Month % 2 == monthIsOddValue;

            return this;
        }

        public static JobScheduler<JobT> BuildPriodical(TimeSpan period, TimeSpan? delay = null)
        {
            period = SettingReader.Get(typeof(JobT).Name + ".RepeatPeriod", period);

            return new JobScheduler<JobT>(dueTime: delay ?? TimeSpan.Zero, period: period);
        }

        public void Schedule()
        {
            if (timer != null)
                throw new NotSupportedException("Already scheduled!");

            TimerCallback timerCallback = notusedState => DoJob();

            timer = new Timer(timerCallback, null, this.DueTime, this.Period);
        }

        private static bool alreadyRunning;
        private void DoJob()
        {
            if (!alreadyRunning)
            {
                try
                {

                    if (!this.MultipleRunningInstance)
                        alreadyRunning = true;
                    if (this.RunCondition == null || this.RunCondition())
                        new JobT().Do();
                }
                finally
                {
                    if (!this.MultipleRunningInstance)
                        alreadyRunning = false;
                }
            }
        }
    }
}
