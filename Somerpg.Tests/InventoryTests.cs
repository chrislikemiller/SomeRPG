using Microsoft.VisualStudio.TestTools.UnitTesting;
using Somerpg.Common.Model;

namespace Somerpg.Tests
{
    [TestClass]
    public class InventoryTests
    {
        // for getting different kinds of instances
        private const int SEED_FIRST = 12345;
        private const int SEED_SECOND = 23456;

        [TestMethod]
        public void TestCopy()
        {
            // arrange
            var inv = Helper.GetInventory(SEED_FIRST);

            // act
            var copy = inv.Copy();

            // assert
            Assert.IsTrue(inv.Equals(copy));
            Assert.IsTrue(copy.Equals(inv));
        }

        [TestMethod]
        public void TestCopyWithoutItems()
        {
            // arrange
            var inv = Helper.GetInventoryNoItems(SEED_FIRST);

            // act
            var copy = inv.Copy();

            // assert
            Assert.IsTrue(inv.Equals(copy));
            Assert.IsTrue(copy.Equals(inv));
        }


        [TestMethod]
        public void TestEqualitySameObjects()
        {
            // arrange
            var inv1 = Helper.GetInventory(SEED_FIRST);
            var inv2 = Helper.GetInventory(SEED_FIRST);

            // act
            var result = inv1.Equals(inv2);

            // assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestEqualityDifferentObjects()
        {
            // arrange
            var inv1 = Helper.GetInventory(SEED_FIRST);
            var inv2 = Helper.GetInventory(SEED_SECOND);

            // act
            var result = inv1.Equals(inv2);

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestReplace()
        {
            // arrange
            var inv1 = Helper.GetInventory(SEED_FIRST);
            var inv2 = Helper.GetInventory(SEED_SECOND);
            var copy1 = inv1.Copy();

            // act
            inv1.Replace(inv2);

            // assert
            Assert.IsFalse(copy1.Equals(inv2));
            Assert.IsTrue(inv1.Equals(inv2));
        }


        [TestMethod]
        public void TestMerge()
        {
            // arrange
            var inv1 = Helper.GetInventory(SEED_FIRST);
            var inv2 = Helper.GetInventory(SEED_SECOND);

            var inv1Copy = inv1.Copy();
            var goldResult = inv1Copy.Gold + inv2.Gold;
            inv1Copy.Materials.Add(inv2.Materials);
            var matsResult = inv1Copy.Materials;
            var itemsResult = inv1Copy.Items;
            foreach (var item in inv2.Items)
            {
                itemsResult.Add(item);
            }
            var resultInv = new Inventory()
            {
                Gold = goldResult,
                Materials = matsResult,
                Items = itemsResult
            };

            // act
            inv1.Merge(inv2);

            // assert
            Assert.IsTrue(inv1.Equals(resultInv));
        }

    }
}
