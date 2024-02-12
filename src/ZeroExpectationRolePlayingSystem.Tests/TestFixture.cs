using System;
using NUnit.Framework;

namespace ZeroExpectationRolePlayingSystem
{
    [TestFixture]
    public class TestFixture
    {
        [Test]
        public void IsCriticalFailure_WithCriticalFailureOffset()
        {
            var rpg = new RolePlayingSystem().WithCriticalSuccessOffset(-3).WithCriticalFailureOffset(3);


            string Chance(double value)
            {
                var res = 1.0 / value;
                if ((int)res > 1)
                {
                    return $"1 of {(int)res}";
                }

                double m = 1.0;
                while ((int)(res * m) <= (int)m)
                {
                    m *= 10;
                }
                return $"{m} of {(int)(res*m)}";
            }


            {

                Console.WriteLine($"+1 {NDice.EvaluateRoll(0.985)}");

                //Console.WriteLine($"Trying at default {MathHelper.ArgumentFromProbability(0.15)}");
                //Console.WriteLine($"Complete begginer {MathHelper.ArgumentFromProbability(0.25)}");
                //Console.WriteLine($"Begginer {MathHelper.ArgumentFromProbability(0.37)}");
                //Console.WriteLine($"Amateur {MathHelper.ArgumentFromProbability(0.62)}");
                //Console.WriteLine($"Competent {MathHelper.ArgumentFromProbability(0.80)}");
                //Console.WriteLine($"Very competent {MathHelper.ArgumentFromProbability(0.90)}");
                //Console.WriteLine($"Specialist {MathHelper.ArgumentFromProbability(0.95)}");
                //Console.WriteLine($"Respected Specialist {MathHelper.ArgumentFromProbability(0.981)}");

                //16.2% 7 -: Trying at default ? : -)
                //25.9% 8 -: Complete begginer
                //37.5% 9 - 10: Begginer(Most educational system bring your skill to here)
                //62.5% 11: Amateur
                //80 12 - 13: Competent(Most military training and alike bring you to this level)
                //90.7% 14: Competent at jobs with risks, or very competent with normal jobs.
                //95.4% 15: Very competent on risk jobs and especialist among normal jobs.
                //98.1% 16: Especialist(in any job), respected among others in his profission in any profession
                //17 - 18: Highly Specialized
                //19 - 20: Among the best in earth
                //21 +: Among the best in history
            }

            {
                var a = rpg.GetSuccessProbability(-0.25);
                Console.WriteLine($"{a}, {Chance(a)}");
            }
            {
                var a = rpg.GetSuccessProbability(0.45);
                Console.WriteLine($"{a}, {Chance(a)}");
            }
            {
                var a = rpg.GetSuccessProbability(-0.7);
                Console.WriteLine($"{a}, {Chance(a)}");
            }
            {
                var a = rpg.GetSuccessProbability(-0.7+0.45);
                Console.WriteLine($"{a}, {Chance(a)}");
            }

            for (double check = -2.0; check <= 2.0; check += 0.5)
            {
                var ps = rpg.GetCriticalSuccessProbability(check);
                var pf = rpg.GetCriticalFailureProbability(check);
                var sp = rpg.GetSuccessProbability(check);
                Console.WriteLine($"| {check} | {100 * ps:0.000}% | 1 of {(int)(1.0/ps)} | {100 * sp:0.000}% | {Chance(sp)}  | {100 * pf:0.000}% | 1 of {(int)(1.0 / pf)} |");
                //Console.WriteLine($"| {check} | 1 of {(int)(1.0/ps)} | 1 of {(int)(1.0 / sp)} {100 * sp:0.000}% | {100 * pf:0.000}% |");
            }

            {
                var check = -0.25;
                var ps = rpg.GetCriticalSuccessProbability(check);
                var pf = rpg.GetCriticalFailureProbability(check);
                var sp = rpg.GetSuccessProbability(check);
                Console.WriteLine($"| {-check} | {100 * ps:0.000}% | 1 of {(int)(1.0 / ps)} | {100 * sp:0.000}% | {Chance(sp)}  | {100 * pf:0.000}% | 1 of {(int)(1.0 / pf)} |");
                //Console.WriteLine($"| {check} | 1 of {(int)(1.0/ps)} | 1 of {(int)(1.0 / sp)} {100 * sp:0.000}% | {100 * pf:0.000}% |");
            }
        }
    }
}