using System;
using System.Collections.Generic;
using System.Text;

namespace Somerpg.Client.Service
{
    public interface ITimerService 
    {
        IDisposable StartNew(Action<long> onTimerTick);
    }
}
