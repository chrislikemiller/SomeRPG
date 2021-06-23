using Microsoft.VisualStudio.TestTools.UnitTesting;
using Somerpg.Common.Model;

namespace Somerpg.Tests
{
    [TestClass]
    public class MaterialsTests
    {
        // for getting different kinds of instances
        private const int SEED_FIRST = 12345;
        private const int SEED_SECOND = 23456;

        [TestMethod]
        public void TestCopy()
        {
            // arrange
            var mats = Helper.GetMats(SEED_FIRST);

            // act
            var copy = mats.Copy();

            // assert
            Assert.IsTrue(mats.Equals(copy));
            Assert.IsTrue(copy.Equals(mats));
        }

        [TestMethod]
        public void TestEqualitySameObjects()
        {
            // arrange
            var mats1 = Helper.GetMats(SEED_FIRST);
            var mats2 = Helper.GetMats(SEED_FIRST);

            // act
            var result = mats1.Equals(mats2);

            // assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestEqualityDifferentObjects()
        {
            // arrange
            var mats1 = Helper.GetMats(SEED_FIRST);
            var mats2 = Helper.GetMats(SEED_SECOND);

            // act
            var result = mats1.Equals(mats2);

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestIsEnoughTooMuch()
        {
            // arrange
            var mats1 = Helper.GetMats(SEED_FIRST);
            var mats2 = new Material()
            {
                Leather = mats1.Leather + 1,
                Metal = mats1.Metal + 1,
                Wood = mats1.Wood + 1,
            };

            // act
            var result = mats1.IsEnough(mats2);

            // assert
            Assert.IsFalse(result);
        }


        [TestMethod]
        public void TestIsEnoughMoreThanEnough()
        {
            // arrange
            var mats1 = Helper.GetMats(SEED_FIRST);
            var mats2 = new Material()
            {
                Leather = mats1.Leather - 1,
                Metal = mats1.Metal - 1,
                Wood = mats1.Wood - 1,
            };

            // act
            var result = mats1.IsEnough(mats2);

            // assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestIsEnoughExacltyEnough()
        {
            // arrange
            var mats1 = Helper.GetMats(SEED_FIRST);
            var mats2 = new Material()
            {
                Leather = mats1.Leather,
                Metal = mats1.Metal,
                Wood = mats1.Wood,
            };

            // act
            var result = mats1.IsEnough(mats2);

            // assert
            Assert.IsTrue(result);
        }


        [TestMethod]
        public void TestAdd()
        {
            // arrange
            var mats1 = Helper.GetMats(SEED_FIRST);
            var mats2 = Helper.GetMats(SEED_SECOND);
            var result = new Material
            {
                Leather = mats1.Leather + mats2.Leather,
                Metal = mats1.Metal + mats2.Metal,
                Wood = mats1.Wood + mats2.Wood,
            };

            // act
            mats1.Add(mats2);

            // assert
            Assert.IsTrue(mats1.Equals(result));
        }

        [TestMethod]
        public void TestAddDiminishing()
        {
            // arrange
            var diminishingFactor = Material.DIMINISHING_FACTOR;
            var mats1 = Helper.GetMats(SEED_FIRST);
            var mats2 = Helper.GetMats(SEED_SECOND);
            var result = new Material
            {
                Leather = mats1.Leather + (uint)(mats2.Leather * diminishingFactor),
                Metal = mats1.Metal + (uint)(mats2.Metal * diminishingFactor),
                Wood = mats1.Wood + (uint)(mats2.Wood * diminishingFactor),
            };

            // act
            mats1.AddDiminishing(mats2);

            // assert
            Assert.IsTrue(mats1.Equals(result));
        }


        [TestMethod]
        public void TestConsume()
        {
            // arrange
            var mats1 = Helper.GetMats(SEED_FIRST);
            var mats2 = mats1.Copy();

            // act
            var result = mats1.Consume(mats2);

            // assert
            Assert.IsTrue(result);
        }


        [TestMethod]
        public void TestConsumeFail()
        {
            // arrange
            var mats1 = Helper.GetMats(SEED_FIRST);
            var mats2 = mats1.Copy();
            mats2.Leather++;
            mats2.Metal++;
            mats2.Wood++;

            // act
            var result = mats1.Consume(mats2);

            // assert
            Assert.IsFalse(result);
        }
    }
}
