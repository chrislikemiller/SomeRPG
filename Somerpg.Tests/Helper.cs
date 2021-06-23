using Somerpg.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Somerpg.Tests
{
    public static class Helper
    {
        public static Inventory GetInventory(int seed)
        {
            var rand = new Random(seed);

            var inv = GetInventoryNoItems(seed);
            inv.Items.Add(new Weapon(rand.Next(1, 100)));
            inv.Items.Add(new Armor(rand.Next(1, 100)));
            return inv;
        }

        public static Inventory GetInventoryNoItems(int seed)
        {
            var rand = new Random(seed);

            var goldValue = rand.Next(1, 50000);
            var mats = GetMats(seed);

            return new Inventory
            {
                Gold = goldValue,
                Materials = mats,
            };
        }

        public static Material GetMats(int seed)
        {
            var rand = new Random(seed);

            return new Material
            {
                Leather = (uint)rand.Next(1, 1000),
                Metal = (uint)rand.Next(1, 1000),
                Wood = (uint)rand.Next(1, 1000)
            };
        }

        public static Armor GetArmor(int seed)
        {
            var rand = new Random(seed);
            return new Armor(rand.Next(1, 100))
            {
                PlusValue = rand.Next(1, 100)
            };
        }

        public static Weapon GetWeapon(int seed)
        {
            var rand = new Random(seed);
            return new Weapon(rand.Next(1, 100))
            {
                PlusValue = rand.Next(1, 100)
            };
        }
    }
}
