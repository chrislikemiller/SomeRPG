using System;
using System.Collections.Generic;
using Somerpg.Common.Model;

namespace Somerpg.Client.Util
{
    static class Extensions
    {
        public static void ClearAndReplace(this IList<Item> source_, IEnumerable<Item> new_ )
        {
            source_.Clear();
            foreach (var item in new_)
            {
                source_.Add(item);
            }
        }
    }
}
