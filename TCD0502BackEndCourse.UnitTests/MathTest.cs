using NUnit.Framework;


using TCD0502BackEndCourse.TestNinja;

namespace TCD0502BackEndCourse.UnitTests
{
    public class MathTest
    {
        private Math _math;

        [SetUp]
        public void Setup()
        {
            _math = new Math();
        }

        [Test]
        public void Sum_WhenCalled_ReturnSumOfTwoArguments()
        {
            // Arrange
            int a = 10;
            int b = 20;
            int expectedResult = 30;
            // Act
            int actualResult = _math.Sum(a, b);
            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestCase(2, 1, 2)]
        [TestCase(1, 2, 2)]
        //[TestCase(2, 2, 2)]

        public void Max_WhenCalled_ReturnTheGreaterArgument(int a, int b, int expectedResult)
        {
            // Arrange

            // Act
            int actualResult = _math.Max(a, b);
            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}