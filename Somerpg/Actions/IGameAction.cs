using Somerpg.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Somerpg.Client.Actions
{
    public interface IGameAction
    {
        public string Description { get; set; }
        public IAction Action { get; set; }
        public Player Player { get; set; }
        public long TimeLeft { get; set; }

    }
}
