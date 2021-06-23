using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;

namespace Somerpg.Client.Service
{
    public class TimerService : ITimerService
    {
        private const int DEFAULT_DELAY = 1;

        public int DelayInSeconds { get; set; } = DEFAULT_DELAY;

        public IDisposable StartNew(Action<long> onTick)
        {
            return Observable.Interval(TimeSpan.FromSeconds(DelayInSeconds)).Subscribe(onTick);
        }
    }
}
