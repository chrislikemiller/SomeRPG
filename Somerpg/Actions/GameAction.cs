using Somerpg.Common.Model;
using Somerpg.Common.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Somerpg.Client.Actions
{
    public class GameAction : NotifyBase, IGameAction
    {
        private IAction _action;
        private Player _player;
        private long _timeLeft;

        private string _description;

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        public IAction Action
        {
            get => _action; 
            set
            {
                _action = value;
                OnPropertyChanged();
            }
        }

        public Player Player
        {
            get => _player;
            set
            {
                _player = value;
                OnPropertyChanged();
            }
        }

        public long TimeLeft
        {
            get => _timeLeft;
            set
            {
                _timeLeft = value;
                OnPropertyChanged();
            }
        }

    }
}
