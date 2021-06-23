using System;
using System.Collections.Generic;
using System.Text;

namespace Somerpg.Client.Actions
{
    public interface IAction
    {
        
    }

    public class AddXPAction : IAction
    {
        public long XPToAdd { get; set; }
    }

    public class AddGoldAction : IAction
    {
        public long GoldToAdd { get; set; }
    }
}
