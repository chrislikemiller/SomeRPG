using Somerpg.Client.Actions;
using Somerpg.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Somerpg.Client.Service
{
    public interface IService : IDisposable
    {
        void Subscribe(Action<IGameAction> action);
        IGameAction StartAction(Player player, IAction action);
    }
}
