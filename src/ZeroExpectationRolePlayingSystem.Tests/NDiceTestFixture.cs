using System;
using System.Linq;
using System.Threading.Channels;
using NUnit.Framework;

namespace ZeroExpectationRolePlayingSystem
{
    [TestFixture]
    public class NDiceTestFixture
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
            var dice = new NDice(new Random(0).NextDouble);
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
            var dice = new NDice(new Random(0).NextDouble, mu, sigma);
            var values = Enumerable.Range(0, 10000).Select(_ => dice.Roll()).OrderBy(_ => (double)_).ToList();
            var samples = (double)values.Count;
            var sampleIndex = (int)((values.Count - 1) * percentage);
            var sample = values[sampleIndex];

            Assert.AreEqual(dice.EvaluateProbability(sample), sampleIndex / samples, 0.01);
        }

        [Test]
        public void ValidateAddDice()
        {
            var diceX = new NDice(new Random(0).NextDouble, 2.0, 3.0);
            var diceY = new NDice(new Random(1).NextDouble, 1.0, 1.0);

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
            var diceX = new NDice(new Random(0).NextDouble, 2.0, 3.0);
            var diceY = new NDice(new Random(1).NextDouble, 1.0, 1.0);

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

        [Test]
        [TestCase(0.001)]
        [TestCase(0.1)]
        [TestCase(0.4)]
        [TestCase(0.5)]
        [TestCase(0.6)]
        [TestCase(0.9)]
        [TestCase(0.999)]
        public void EvaluateProbability(double probability)
        {
            var argumentFromProbability = NDice.EvaluateRoll(probability);
            var p = NDice.EvaluateProbability(argumentFromProbability);
            Assert.AreEqual(probability, p, 0.001);
        }

        [Test]
        [TestCase(0.001)]
        [TestCase(0.1)]
        [TestCase(0.4)]
        [TestCase(0.5)]
        [TestCase(0.6)]
        [TestCase(0.9)]
        [TestCase(0.999)]
        public void EvaluateRollAndProbability(double probability)
        {
            var argumentFromProbability = NDice.EvaluateRoll(probability);
            var p = NDice.EvaluateProbability(argumentFromProbability);
            Assert.AreEqual(probability, p, 0.001);
        }

        [Test]
        [TestCase(0.001)]
        [TestCase(0.1)]
        [TestCase(0.4)]
        [TestCase(0.5)]
        [TestCase(0.6)]
        [TestCase(0.9)]
        [TestCase(0.999)]
        public void EvaluateRollAndProbabilityWithMeanAndStdDev(double probability)
        {
            var mean = 1.4;
            var stdDev = 2.1;
            var argumentFromProbability = NDice.EvaluateRoll(probability, mean, stdDev);
            var p = NDice.EvaluateProbability(argumentFromProbability, mean, stdDev);
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
        public void EvaluateProbability(double skillLevel, double expectedProbability)
        {
            var probability = NDice.EvaluateProbability(skillLevel);

            Assert.AreEqual(expectedProbability, probability, 1e-6);
        }

        [Test]
        public void CalculateBalance()
        {
            var creatueA = new SampleCreature()
            {
                HitPoints = 100,
                Attack = new NDice(20.0, 2.0),
                Defence = new NDice(10.0, 2.0),
            };
            var creatueB = new SampleCreature()
            {
                HitPoints = 40,
                Attack = new NDice(20.0, 2.0),
                Defence = new NDice(10.0, 2.0),
            };

            Console.WriteLine($"When attacking monster {creatueB}");
            Console.WriteLine($"Chance to win in {creatueA}: {GetWinChance(creatueA, creatueB)}");
            {
                var creatueAVariation = new SampleCreature()
                {
                    HitPoints = creatueA.HitPoints - 30,
                    Attack = creatueA.Attack + 1,
                    Defence = creatueA.Defence,
                };
                Console.WriteLine($"Chance to win in {creatueAVariation}: {GetWinChance(creatueAVariation, creatueB)}");
            }
            {
                var creatueAVariation = new SampleCreature()
                {
                    HitPoints = creatueA.HitPoints - 30,
                    Attack = creatueA.Attack,
                    Defence = creatueA.Defence+3,
                };
                Console.WriteLine($"Chance to win in {creatueAVariation}: {GetWinChance(creatueAVariation, creatueB)}");
            }
        }

        private double GetWinChance(SampleCreature attacker, SampleCreature defender)
        {
            var maxNumberOfHits = (int)Math.Ceiling(attacker.HitPoints / (defender.Attack.Mean - attacker.Defence.Mean));
            var expectedDamage = attacker.Attack - defender.Defence;
            var howMuchDamageRequired = defender.HitPoints / maxNumberOfHits;
            return Math.Pow(1.0 - expectedDamage.EvaluateProbability(howMuchDamageRequired), maxNumberOfHits);
        }

        class SampleCreature
        {
            public double HitPoints { get; set; }
            public NDice Attack { get; set; }
            public NDice Defence { get; set; }

            public override string ToString()
            {
                return $@"HP:{HitPoints}, A:{Attack}, D:{Defence}";
            }
        }
    }
}