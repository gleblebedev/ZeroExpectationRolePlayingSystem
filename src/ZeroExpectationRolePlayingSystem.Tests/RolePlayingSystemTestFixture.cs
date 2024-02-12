using System;
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

        [Test()]
        public void GetCriticalSuccessProbability_NoPercentDefined()
        {
            Assert.AreEqual(0.0, new RolePlayingSystem().GetCriticalSuccessProbability(0));
        }

        [Test]
        [TestCase(0.1)]
        [TestCase(0.4)]
        public void GetCriticalSuccessProbability(double probability)
        {
            Assert.AreEqual(probability, new RolePlayingSystem().WithCriticalSuccessProbability(probability).GetCriticalSuccessProbability(0), 1e-6);
        }

        [Test()]
        public void GetCriticalFailureProbability_NoPercentDefined()
        {
            Assert.AreEqual(0.0, new RolePlayingSystem().GetCriticalFailureProbability(0));
        }

        [Test()]
        public void WithCriticalSuccessProbability_MoreThan50Percent_ThrowsException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(()=>new RolePlayingSystem().WithCriticalSuccessProbability(0.6));
        }

        [Test()]
        public void WithCriticalFailureProbability_MoreThan50Percent_ThrowsException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new RolePlayingSystem().WithCriticalFailureProbability(0.6));
        }

        [Test]
        [TestCase(0.1)]
        [TestCase(0.4)]
        public void GetCriticalFailureProbability(double probability)
        {
            Assert.AreEqual(probability, new RolePlayingSystem().WithCriticalFailureProbability(probability).GetCriticalFailureProbability(0), 1e-6);
        }

        [Test]
        public void PrintBoost()
        {
            double boost = 0.1f;
            Console.WriteLine("Boost in {0:0.000} adds to success probability:", boost);
            var rpg = new RolePlayingSystem();
            foreach (var tuple in levels)
            {
                var probability = tuple.Item2*0.01;
                var successProbability = NDice.EvaluateProbability(NDice.EvaluateRoll(probability)+boost);
                Console.WriteLine("{0}:\t{1:0.00}% (from {2:0.00}% to {3:0.00}%)", tuple.Item1, (successProbability-probability)*100, tuple.Item2, successProbability*100);
            }
        }

        [Test]
        public void PrintCrits()
        {
            var rpg = new RolePlayingSystem().WithCriticalSuccessProbability(0.02).WithCriticalFailureProbability(0.02);
            foreach (var tuple in levels)
            {
                var probability = NDice.EvaluateRoll(tuple.Item2 * 0.01);
                var successProbability = rpg.GetCriticalSuccessProbability(probability);
                var failureProbability = rpg.GetCriticalFailureProbability(probability);
                Console.WriteLine("{0}:\tsuccess chance: {1:0.00}%\tcrit success: {2:0.00}%\tcrit failure: {3:0.00}%)", tuple.Item1, tuple.Item2, successProbability * 100, failureProbability * 100);
            }
        }

        public static readonly Tuple<string,double>[] levels = new Tuple<string, double>[]
        {
            Tuple.Create("Abysmal", 0.5),
            Tuple.Create("Inept", 10.0),
            Tuple.Create("Mediocre", 25.0),
            Tuple.Create("Average", 50.0),
            Tuple.Create("Trained", 75.0),
            Tuple.Create("Well Trained", 90.0),
            Tuple.Create("Veteran", 95.0),
            Tuple.Create("Expert", 98.0),
            Tuple.Create("Master", 99.8),
        };
    }
}