using Somerpg.Client.Actions;
using Somerpg.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Somerpg.Execution
{
    interface IActionExecutor
    {
        public ActionReward Execute(IAction action_, Player player_);
    }
}
