using System;

namespace ZeroExpectationRolePlayingSystem
{
    public class RolePlayingSystem
    {
        private static readonly Random RandomGenerator = new Random();

        /// <summary>
        /// Random number generator with linear distribution in range of [0..1].
        /// </summary>
        private readonly Func<double> _randomNumberGenerator;

        /// <summary>
        /// Construct role playing system.
        /// </summary>
        public RolePlayingSystem(): this(DefaultRandomGenerator)
        {
        }

        /// <summary>
        /// Construct role playing system.
        /// </summary>
        public RolePlayingSystem(Func<double> randomNumberGenerator)
        {
            _randomNumberGenerator = randomNumberGenerator;
        }

        /// <summary>
        /// Default random generator function.
        /// </summary>
        /// <returns>Randomly generated value.</returns>
        public static double DefaultRandomGenerator()
        {
            return RandomGenerator.NextDouble();
        }

        /// <summary>
        /// Threshold for critical success.
        /// </summary>
        private double _criticalSuccess = double.MinValue;

        /// <summary>
        /// Threshold for critical failure.
        /// </summary>
        private double _criticalFailure = double.MaxValue;

        /// <summary>
        /// Set critical success probability.
        /// </summary>
        /// <param name="probability">Expected probability.</param>
        /// <returns>This role playing system with modified value.</returns>
        public RolePlayingSystem WithCriticalSuccessProbability(double probability)
        {
            return WithCriticalSuccessOffset(NDice.EvaluateRoll(probability));
        }

        /// <summary>
        /// Set critical success threshold value.
        /// </summary>
        /// <param name="offset">Threshold value. Every result less of the value is considered as a critical failure.</param>
        /// <returns>This role playing system with modified value.</returns>
        public RolePlayingSystem WithCriticalSuccessOffset(double offset)
        {
            if (offset >= 0)
                throw new ArgumentOutOfRangeException(nameof(offset));
            _criticalSuccess = offset;
            return this;
        }

        public RolePlayingSystem WithCriticalFailureProbability(double probability)
        {
            return WithCriticalFailureOffset(NDice.EvaluateRoll(1.0 - probability));
        }

        public RolePlayingSystem WithCriticalFailureOffset(double offset)
        {
            if (offset <= 0)
                throw new ArgumentOutOfRangeException(nameof(offset));
            _criticalFailure = offset;
            return this;
        }

        public bool IsCriticalSuccess(double roll, double check)
        {
            return roll - check < _criticalSuccess;
        }

        public bool IsCriticalFailure(double roll, double check)
        {
            return roll - check > _criticalFailure;
        }

        public bool IsSuccess(double roll, double check)
        {
            return roll - check <= 0;
        }

        public double GetSuccessProbability(double check)
        {
            return NDice.EvaluateProbability(check);
        }

        public double GetCriticalSuccessProbability(double check)
        {
            return NDice.EvaluateProbability(check + _criticalSuccess);
        }

        public double GetCriticalFailureProbability(double check)
        {
            return 1.0- NDice.EvaluateProbability(check + _criticalFailure);
        }

        /// <summary>
        /// Roll the dice.
        /// </summary>
        /// <param name="dice">Dice to roll.</param>
        /// <returns>Roll outcome.</returns>
        public double Roll(NDice dice)
        {
            return dice.Roll(_randomNumberGenerator);
        }
    }
}