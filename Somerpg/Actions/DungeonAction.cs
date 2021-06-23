using Somerpg.Common.Model;

namespace Somerpg.Client.Actions
{
    class DungeonAction : IAction
    {
        public int Tier { get; set; }
        public int AtkRequirement { get; set; }
        public int HPRequirement { get; set; }
        public ActionReward Rewards { get; set; }

        // has:
        // - level 
        // - stat requirement
        // - xp reward
        // - gold reward
        // - loot reward (with chance)
    }
}
