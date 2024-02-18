using System;

namespace ZeroExpectationRolePlayingSystem
{
    /// <summary>
    /// NDice to roll.
    /// NDice is a magical dice that produce values with a normal distribution of probability.
    /// https://en.wikipedia.org/wiki/Box%E2%80%93Muller_transform
    /// </summary>
    public struct NDice
    {
        private static readonly Random RandomGenerator = new Random();

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
        /// <param name="mean">Mean (mu) of the normal distribution.</param>
        /// <param name="stdDev">Standard definition (sigma) of the normal distribution.</param>
        public NDice(double mean = 0.0f, double stdDev = 1.0f): this(DefaultRandomGenerator, mean, stdDev)
        {
        }

        /// <summary>
        /// Construct dice.
        /// </summary>
        /// <param name="randomNumberGenerator">Random number generator.</param>
        /// <param name="mean">Mean (mu) of the normal distribution.</param>
        /// <param name="stdDev">Standard definition (sigma) of the normal distribution.</param>
        public NDice(Func<double> randomNumberGenerator, double mean = 0.0f, double stdDev = 1.0f)
        {
            _randomNumberGenerator = randomNumberGenerator;
            _mean = mean;
            _stdDev = stdDev;
            _generatedValue = null;
        }

        /// <summary>
        /// Mean (mu) of the normal distribution.
        /// </summary>
        public double Mean => _mean;

        /// <summary>
        /// Standard definition (sigma) of the normal distribution.
        /// </summary>
        public double StdDev => _stdDev;

        /// <summary>
        /// Default random generator function.
        /// </summary>
        /// <returns>Randomly generated value.</returns>
        public static double DefaultRandomGenerator()
        {
            return RandomGenerator.NextDouble();
        }

        /// <summary>
        /// Roll dice.
        /// Random number is generated via Box–Muller transform.
        /// </summary>
        /// <returns>Dice roll result.</returns>
        public double Roll()
        {
            if (_generatedValue.HasValue)
            {
                var value = _generatedValue.Value;
                _generatedValue = null;
                return value;
            }

            double u1, u2;

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
            return EvaluateProbability(roll, _mean, _stdDev);
        }

        /// <summary>
        /// Evaluate probability of a roll that is less or equal to the dice roll.
        /// </summary>
        /// <param name="roll">Dice roll.</param>
        /// <returns>Probability in range of [0..1]</returns>
        public static double EvaluateProbability(double roll, double mean = 0.0, double stdDev = 1.0)
        {
            double z = (roll - mean) / stdDev;
            return (SpecialFunctions.Erf(z * 0.70710678118654752440084436210485) + 1.0) * 0.5;
        }

        /// <summary>
        /// Evaluate value from probability.
        /// </summary>
        /// <param name="probability">Probability in range [0..1]</param>
        /// <returns>Corresponding value.</returns>
        public double EvaluateRoll(double probability)
        {
            return EvaluateRoll(probability, _mean, _stdDev);
        }

        /// <summary>
        /// Evaluate value from probability.
        /// </summary>
        /// <param name="probability">Probability in range [0..1]</param>
        /// <returns>Corresponding value.</returns>
        public static double EvaluateRoll(double probability, double mean = 0.0, double stdDev = 1.0)
        {
            if (probability <= 0)
                return double.NegativeInfinity;
            if (probability >= 1)
                return double.PositiveInfinity;

            var z = -1.4142135623730950488016887242097 * SpecialFunctions.ErfcInv(2.0 * probability);

            return z * stdDev + mean;
        }

        /// <summary>
        /// Overload the multiplication operator *.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>Result of addition.</returns>
        public static NDice operator *(NDice left, NDice right)
        {
            var leftSigma2 = left._stdDev * left._stdDev;
            var rightSigma2 = right._stdDev * right._stdDev;
            return new NDice(left._randomNumberGenerator,
                (leftSigma2 + right._mean + rightSigma2 + left._mean)/(leftSigma2+rightSigma2),
                1.0/(1.0/leftSigma2 + 1.0/rightSigma2));
        }

        /// <summary>
        /// Overload the addition operator +.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>Result of addition.</returns>
        public static NDice operator +(NDice left, NDice right)
        {
            return new NDice(left._randomNumberGenerator, left._mean + right._mean, Math.Sqrt(left._stdDev * left._stdDev + right._stdDev * right._stdDev));
        }

        /// <summary>
        /// Overload the subtraction operator -.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>Result of subtraction.</returns>
        public static NDice operator -(NDice left, NDice right)
        {
            return new NDice(left._randomNumberGenerator, left._mean - right._mean, Math.Sqrt(left._stdDev * left._stdDev + right._stdDev * right._stdDev));
        }

        /// <summary>
        /// Overload the addition operator +.
        /// Shifts distribution mean value.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand, applied to mean value.</param>
        /// <returns>Result of addition.</returns>
        public static NDice operator +(NDice left, double right)
        {
            return new NDice(left._randomNumberGenerator, left._mean + right, left._stdDev);
        }

        /// <summary>
        /// Overload the subtraction operator -.
        /// Shifts distribution mean value.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand, applied to mean value.</param>
        /// <returns>Result of subtraction.</returns>
        public static NDice operator -(NDice left, double right)
        {
            return new NDice(left._randomNumberGenerator, left._mean - right, left._stdDev);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"\u03BC={_mean}, \u03C3={_stdDev}";
        }
    }
}