using Microsoft.VisualStudio.TestTools.UnitTesting;
using Somerpg.Common.Model;

namespace Somerpg.Tests
{
    [TestClass]
    public class ItemsTests
    {
        // for getting different kinds of instances
        private const int SEED_FIRST = 12345;
        private const int SEED_SECOND = 23456;

        [TestMethod]
        public void TestCopyArmor()
        {
            // arrange
            var armor = Helper.GetArmor(SEED_FIRST);

            // act
            var copy = armor.Copy();

            // assert
            Assert.IsTrue(armor.Equals(copy));
            Assert.IsTrue(copy.Equals(armor));
        }

        [TestMethod]
        public void TestCopyWeapon()
        {
            // arrange
            var weapon = Helper.GetWeapon(SEED_FIRST);

            // act
            var copy = weapon.Copy();

            // assert
            Assert.IsTrue(weapon.Equals(copy));
            Assert.IsTrue(copy.Equals(weapon));
        }

    }
}
