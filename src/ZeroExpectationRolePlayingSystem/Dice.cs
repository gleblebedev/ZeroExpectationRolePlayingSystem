using System;
using System.Drawing;

namespace ZeroExpectationRolePlayingSystem
{
    /// <summary>
    /// Dice to roll.
    /// https://en.wikipedia.org/wiki/Box%E2%80%93Muller_transform
    /// </summary>
    public class Dice
    {
        // Second result from the Box–Muller transform.
        private double? _generatedValue;

        /// <summary>
        /// Random number generator with linear distribution in range of [0..1].
        /// </summary>
        private readonly Func<double> _randomNumberGenerator;

        /// <summary>
        /// Normal distribution mean (mu).
        /// </summary>
        private readonly double _mean;

        /// <summary>
        /// Normal distribution standard deviation (sigma).
        /// </summary>
        private readonly double _stdDev;

        /// <summary>
        /// Construct dice.
        /// </summary>
        /// <param name="randomNumberGenerator">Random number generator.</param>
        /// <param name="mean">Mean (mu) of the normal distribution.</param>
        /// <param name="stdDev">Standard definition (sigma) of the normal distribution.</param>
        public Dice(Func<double> randomNumberGenerator, double mean = 0.0f, double stdDev = 1.0f)
        {
            _randomNumberGenerator = randomNumberGenerator;
            _mean = mean;
            _stdDev = stdDev;
        }


        /// <summary>
        /// Roll dice.
        /// Random number is generated via Box–Muller transform.
        /// </summary>
        /// <returns>Dice roll result.</returns>
        public double Roll()
        {
            double u1, u2;

            if (_generatedValue.HasValue)
            {
                var value = _generatedValue.Value;
                _generatedValue = null;
                return value;
            }

            do
            {
                u1 = _randomNumberGenerator();
                u2 = 6.283185307179586476925286766559 * _randomNumberGenerator();
            } while (u1 <= double.Epsilon);

            var u1Log = _stdDev * Math.Sqrt(-2.0 * Math.Log(u1));
            _generatedValue = _mean + u1Log * Math.Sin(u2);
            return _mean + u1Log * Math.Cos(u2);
        }

        /// <summary>
        /// Evaluate probability of a roll that is less or equal to the dice roll.
        /// </summary>
        /// <param name="roll">Dice roll.</param>
        /// <returns>Probability in range of [0..1]</returns>
        public double EvaluateProbability(double roll)
        {
            return MathHelper.SuccessProbability(roll, _mean, _stdDev);
        }

        public static Dice operator +(Dice left, Dice right)
        {
            return new Dice(left._randomNumberGenerator, left._mean + right._mean, Math.Sqrt(left._stdDev * left._stdDev + right._stdDev * right._stdDev));
        }

        public static Dice operator -(Dice left, Dice right)
        {
            return new Dice(left._randomNumberGenerator, left._mean - right._mean, Math.Sqrt(left._stdDev * left._stdDev + right._stdDev * right._stdDev));
        }
    }
}