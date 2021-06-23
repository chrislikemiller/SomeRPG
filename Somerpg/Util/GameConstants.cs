using System;
using System.Collections.Generic;
using System.Text;

namespace Somerpg.Client.Util
{
    static class GameConstants
    {
        public const int HIGHEST_TIER = 3;

        public static int GetNextPlusCost(int tier_, int plusValue_)
        {
            const double baseCost = 500;
            const double exponent = 1.3;
            return (int)Math.Floor(tier_ * baseCost * (Math.Pow(plusValue_ + 1, exponent)));
        }
    }
}
