using System;
using System.Linq;
using NUnit.Framework;

namespace ZeroExpectationRolePlayingSystem
{
    [TestFixture]
    public class DiceTestFixture
    {
        [Test]
        [TestCase(0.1)]
        [TestCase(0.2)]
        [TestCase(0.3)]
        [TestCase(0.9)]
        [TestCase(1.0)]
        public void Roll(double percentage)
        {
            var dice = new Dice(new Random(0).NextDouble);
            var values = Enumerable.Range(0, 10000).Select(_ => dice.Roll()).OrderBy(_ => (double)_).ToList();
            var samples = (double)values.Count;
            var sampleIndex = (int)((values.Count - 1) * percentage);
            var sample = values[sampleIndex];

            Assert.AreEqual(MathHelper.SuccessProbability(sample), sampleIndex / samples, 0.01);
        }
    }
}