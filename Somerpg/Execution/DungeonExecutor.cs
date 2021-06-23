using Somerpg.Client.Actions;
using Somerpg.Common.Model;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Somerpg.Execution
{
    class DungeonExecutor : IActionExecutor
    {
        private const int BASE_XP_REWARD = 1000;
        private const int BASE_GOLD_REWARD = 10000;


        public ActionReward Execute(IAction action_, Player player_)
        {
            if (action_ is DungeonAction action)
            {
                return new ActionReward
                {
                    XP = action.Tier * BASE_XP_REWARD,
                    Inventory = new Inventory
                    {
                        Gold = action.Tier * BASE_GOLD_REWARD,
                        Items = GetLoot(action, player_)
                    }
                };
            }
            throw new ArgumentException();
        }

        private ObservableCollection<Item> GetLoot(DungeonAction action_, Player player_)
        {
            // todo: make it chance-based based on player stats
            return new ObservableCollection<Item>
            {
                new Weapon(action_.Tier),
                new Armor(action_.Tier)
            };
        }

        ActionReward IActionExecutor.Execute(IAction action_, Player player_)
        {
            return Execute(action_ as DungeonAction ?? throw new ArgumentException(), player_);
        }
    }
}
