using Somerpg.Client.Actions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Somerpg.Client.Util
{
    public class GameActionStore
    {
        private readonly List<IGameAction> _actions = new List<IGameAction>();

        public void Add(IGameAction action_)
        {
            _actions.Add(action_);
        }

        public IGameAction TryPopLastFinishedAction()
        {
            var action = _actions
                .Where(x => x.TimeLeft <= 0)
                .FirstOrDefault();

            if (action != null)
            {
                _actions.Remove(action);
                return action;
            }
            return null;
        }

        public void Tick()
        {
            _actions.ForEach(x => x.TimeLeft--);
        }
    }
}