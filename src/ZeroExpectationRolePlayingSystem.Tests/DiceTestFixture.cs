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
        [TestCase(0.5)]
        [TestCase(0.9)]
        [TestCase(1.0)]
        public void Roll(double percentage)
        {
            var dice = new Dice(new Random(0).NextDouble);
            var values = Enumerable.Range(0, 10000).Select(_ => dice.Roll()).OrderBy(_ => (double)_).ToList();
            var samples = (double)values.Count;
            var sampleIndex = (int)((values.Count - 1) * percentage);
            var sample = values[sampleIndex];

            Assert.AreEqual(dice.EvaluateProbability(sample), sampleIndex / samples, 0.01);
        }

        [Test]
        [TestCase(0.1)]
        [TestCase(0.2)]
        [TestCase(0.3)]
        [TestCase(0.5)]
        [TestCase(0.9)]
        [TestCase(1.0)]
        public void RollMuSigma(double percentage)
        {
            double mu = 2.0;
            double sigma = 3.0;
            var dice = new Dice(new Random(0).NextDouble, mu, sigma);
            var values = Enumerable.Range(0, 10000).Select(_ => dice.Roll()).OrderBy(_ => (double)_).ToList();
            var samples = (double)values.Count;
            var sampleIndex = (int)((values.Count - 1) * percentage);
            var sample = values[sampleIndex];

            Assert.AreEqual(dice.EvaluateProbability(sample), sampleIndex / samples, 0.01);
        }

        [Test]
        public void ValidateAddDice()
        {
            var diceX = new Dice(new Random(0).NextDouble, 2.0, 3.0);
            var diceY = new Dice(new Random(1).NextDouble, 1.0, 1.0);

            var diceZ = diceX + diceY;

            var values = Enumerable.Range(0, 10000).Select(_ => diceX.Roll() + diceY.Roll()).OrderBy(_ => _).ToList();
            var samples = (double)values.Count;
            for (double percentage = 0.1; percentage < 1.0; percentage += 0.1)
            {
                var sampleIndex = (int)((values.Count - 1) * percentage);
                var sample = values[sampleIndex];

                Assert.AreEqual(diceZ.EvaluateProbability(sample), sampleIndex / samples, 0.01);
            }
        }

        [Test]
        public void ValidateSubtractDice()
        {
            var diceX = new Dice(new Random(0).NextDouble, 2.0, 3.0);
            var diceY = new Dice(new Random(1).NextDouble, 1.0, 1.0);

            var diceZ = diceX - diceY;

            var values = Enumerable.Range(0, 10000).Select(_ => diceX.Roll() - diceY.Roll()).OrderBy(_ => _).ToList();
            var samples = (double)values.Count;
            for (double percentage = 0.1; percentage < 1.0; percentage += 0.1)
            {
                var sampleIndex = (int)((values.Count - 1) * percentage);
                var sample = values[sampleIndex];

                Assert.AreEqual(diceZ.EvaluateProbability(sample), sampleIndex / samples, 0.01);
            }
        }
    }
}