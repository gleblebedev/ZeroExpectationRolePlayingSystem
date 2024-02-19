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
            var rpg = new RolePlayingSystem(new Random(0).NextDouble);
            var dice = new NDice(0.0, 1.0);
            var values = Enumerable.Range(0, 10000).Select(_ => rpg.Roll(dice)).OrderBy(_ => (double)_).ToList();
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
            var rpg = new RolePlayingSystem(new Random(0).NextDouble);
            var dice = new NDice(mu, sigma);
            var values = Enumerable.Range(0, 10000).Select(_ => rpg.Roll(dice)).OrderBy(_ => (double)_).ToList();
            var samples = (double)values.Count;
            var sampleIndex = (int)((values.Count - 1) * percentage);
            var sample = values[sampleIndex];

            Assert.AreEqual(dice.EvaluateProbability(sample), sampleIndex / samples, 0.01);
        }

        [Test]
        public void ValidateAddDice()
        {
            var rpg1 = new RolePlayingSystem(new Random(0).NextDouble);
            var rpg2 = new RolePlayingSystem(new Random(1).NextDouble);

            var diceX = new NDice(2.0, 3.0);
            var diceY = new NDice(1.0, 1.0);

            var diceZ = diceX + diceY;

            var values = Enumerable.Range(0, 10000).Select(_ => rpg1.Roll(diceX) + rpg2.Roll(diceY)).OrderBy(_ => _).ToList();
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

            var rpg1 = new RolePlayingSystem(new Random(0).NextDouble);
            var rpg2 = new RolePlayingSystem(new Random(1).NextDouble);

            var diceX = new NDice( 2.0, 3.0);
            var diceY = new NDice(1.0, 1.0);

            var diceZ = diceX - diceY;

            var values = Enumerable.Range(0, 10000).Select(_ => rpg1.Roll(diceX) - rpg2.Roll(diceY)).OrderBy(_ => _).ToList();
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
        public void EvaluateEvaluateRollMatchesProbability(double probability)
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
                Defense = new NDice(10.0, 2.0),
            };
            var creatueB = new SampleCreature()
            {
                HitPoints = 90,
                Attack = new NDice(20.0, 2.0),
                Defense = new NDice(10.0, 2.0),
            };

            Console.WriteLine($"Character stats: {creatueA}");
            Console.WriteLine($"Monster stats: {creatueB}");
            Console.WriteLine($"Chance to win: {GetWinChance(creatueA, creatueB)}");
            {
                int levels = 7;
                (var build, var chance) = GetOptimalBuild(creatueA, creatueB, levels);
                Console.WriteLine($"Best build +{levels}: {build}, chance {chance}");
            }
        }

        private (SampleCreature, double) GetOptimalBuild(SampleCreature attacker, SampleCreature defender, int levels)
        {
            SampleCreature bestBuild = attacker;
            var bestChance = -1.0;
            for (int attackLevels = 0; attackLevels <= levels; attackLevels++)
            {
                var attackerClone = new SampleCreature();
                int levelsLeft = levels - attackLevels;
                for (int defendLevels = 0; defendLevels <= levelsLeft; defendLevels++)
                {
                    int healthLevels = levelsLeft - defendLevels;

                    attackerClone.HitPoints = attacker.HitPoints + healthLevels*30;
                    attackerClone.Attack = attacker.Attack + attackLevels;
                    attackerClone.Defense = attacker.Defense + defendLevels;

                    var chance = GetWinChance(attackerClone, defender);
                    if (chance > bestChance)
                    {
                        bestBuild = attackerClone;
                        bestChance = chance;
                    }
                }
            }

            return (bestBuild, bestChance);
        }

        private double GetWinChance(SampleCreature attacker, SampleCreature defender)
        {
            var damageOnAttackerMean = GetDamageMean(defender.Attack, attacker.Defense);
            var maxNumberOfHits = (int)Math.Ceiling(attacker.HitPoints / damageOnAttackerMean);
            var expectedDamage = attacker.Attack - defender.Defense;
            var howMuchDamageRequired = defender.HitPoints / maxNumberOfHits;
            return Math.Pow(1.0 - expectedDamage.EvaluateProbability(howMuchDamageRequired), maxNumberOfHits);
        }

        private double GetDamageMean(NDice attack, NDice defence)
        {
            var damageOnAttacker = attack - defence;
            var thresholdProbability = damageOnAttacker.EvaluateProbability(0.0);
            var adjustedMeanProbability = 0.5 + thresholdProbability * 0.5;
            return damageOnAttacker.EvaluateRoll(adjustedMeanProbability);
        }

        class SampleCreature
        {
            public double HitPoints { get; set; }
            public NDice Attack { get; set; }
            public NDice Defense { get; set; }

            public override string ToString()
            {
                return $@"HP:{HitPoints}, A:{Attack}, D:{Defense}";
            }
        }
    }
}