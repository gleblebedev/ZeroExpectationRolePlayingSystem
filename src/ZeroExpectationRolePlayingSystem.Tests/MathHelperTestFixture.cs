using NUnit.Framework;

namespace ZeroExpectationRolePlayingSystem
{
    [TestFixture]
    public class MathHelperTestFixture
    {
        [Test]
        [TestCase(0.001)]
        [TestCase(0.1)]
        [TestCase(0.4)]
        [TestCase(0.5)]
        [TestCase(0.6)]
        [TestCase(0.9)]
        [TestCase(0.999)]
        public void ArgumentFromProbability(double probability)
        {
            var argumentFromProbability = MathHelper.ArgumentFromProbability(probability);
            var p = MathHelper.SuccessProbability(argumentFromProbability);
            Assert.AreEqual(probability, p, 0.001);
        }

        [Test]
        [TestCase(-3.0, 0.00134996728131476)]
        [TestCase(-2.0, 0.0227500628872564)]
        [TestCase(-1.0, 0.158655263832364)]
        [TestCase(0.0, 0.5)]
        [TestCase(1.0, 0.841344736167636)]
        [TestCase(2.0, 0.977249937112744)]
        [TestCase(3.0, 0.998650032718685)]
        public void SuccessProbability(double skillLevel, double expectedProbability)
        {
            var probability = MathHelper.SuccessProbability(skillLevel);

            Assert.AreEqual(expectedProbability, probability, 1e-6);
        }
    }
}