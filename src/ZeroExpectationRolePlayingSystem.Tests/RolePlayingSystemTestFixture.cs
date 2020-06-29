using NUnit.Framework;

namespace ZeroExpectationRolePlayingSystem
{
    [TestFixture]
    public class RolePlayingSystemTestFixture
    {
        [Test]
        [TestCase(0.5, 1.0, false)]
        [TestCase(1.0, 1.0, false)]
        [TestCase(1.5, 1.0, false)]
        [TestCase(2.0, 1.0, true)]
        [TestCase(-1.5, -1.0, false)]
        [TestCase(-1.0, -1.0, false)]
        [TestCase(-0.5, -1.0, false)]
        [TestCase(0.0, -1.0, true)]
        public void IsCriticalFailure_WithCriticalFailureOffset(double roll, double skillCheck, bool expectedResult)
        {
            var rpg = new RolePlayingSystem().WithCriticalFailureOffset(0.8);
            Assert.AreEqual(expectedResult, rpg.IsCriticalFailure(roll, skillCheck));
        }

        [Test]
        [TestCase(0.5, 1.0, false)]
        [TestCase(1.0, 1.0, false)]
        [TestCase(1.5, 1.0, false)]
        [TestCase(2.0, 1.0, true)]
        [TestCase(-1.5, -1.0, false)]
        [TestCase(-1.0, -1.0, false)]
        [TestCase(-0.5, -1.0, false)]
        [TestCase(0.0, -1.0, true)]
        public void IsCriticalFailure_WithCriticalFailureProbability(double roll, double skillCheck,
            bool expectedResult)
        {
            var rpg = new RolePlayingSystem().WithCriticalFailureProbability(0.2);
            Assert.AreEqual(expectedResult, rpg.IsCriticalFailure(roll, skillCheck));
        }

        [Test]
        [TestCase(-0.5, 1.0, true)]
        [TestCase(0.5, 1.0, false)]
        [TestCase(1.0, 1.0, false)]
        [TestCase(1.5, 1.0, false)]
        [TestCase(-1.5, -1.0, false)]
        [TestCase(-2.5, -1.0, true)]
        [TestCase(-1.0, -1.0, false)]
        [TestCase(-0.5, -1.0, false)]
        public void IsCritucalSuccess_WithCriticalSuccessOffset(double roll, double skillCheck, bool expectedResult)
        {
            var rpg = new RolePlayingSystem().WithCriticalSuccessOffset(-0.8);
            Assert.AreEqual(expectedResult, rpg.IsCriticalSuccess(roll, skillCheck));
        }

        [Test]
        [TestCase(-0.5, 1.0, true)]
        [TestCase(0.5, 1.0, false)]
        [TestCase(1.0, 1.0, false)]
        [TestCase(1.5, 1.0, false)]
        [TestCase(-1.5, -1.0, false)]
        [TestCase(-2.5, -1.0, true)]
        [TestCase(-1.0, -1.0, false)]
        [TestCase(-0.5, -1.0, false)]
        public void IsCritucalSuccess_WithCriticalSuccessProbability(double roll, double skillCheck,
            bool expectedResult)
        {
            var rpg = new RolePlayingSystem().WithCriticalSuccessProbability(0.2);
            Assert.AreEqual(expectedResult, rpg.IsCriticalSuccess(roll, skillCheck));
        }

        [Test]
        [TestCase(0.5, 1.0, true)]
        [TestCase(1.0, 1.0, true)]
        [TestCase(1.5, 1.0, false)]
        [TestCase(-1.5, -1.0, true)]
        [TestCase(-1.0, -1.0, true)]
        [TestCase(-0.5, -1.0, false)]
        public void IsSuccess(double roll, double skillCheck, bool expectedResult)
        {
            var rpg = new RolePlayingSystem();
            Assert.AreEqual(expectedResult, rpg.IsSuccess(roll, skillCheck));
            Assert.IsFalse(rpg.IsCriticalFailure(roll, skillCheck));
        }
    }
}