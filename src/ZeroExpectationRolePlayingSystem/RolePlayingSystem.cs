﻿using System;

namespace ZeroExpectationRolePlayingSystem
{
    public class RolePlayingSystem
    {
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
            return WithCriticalSuccessOffset(MathHelper.ArgumentFromProbability(probability));
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
            return WithCriticalFailureOffset(MathHelper.ArgumentFromProbability(1.0 - probability));
        }

        public RolePlayingSystem WithCriticalFailureOffset(double offset)
        {
            if (offset <= 0)
                throw new ArgumentOutOfRangeException(nameof(offset));
            _criticalFailure = offset;
            return this;
        }

        public bool IsCriticalSuccess(DiceRoll roll, double check)
        {
            return roll - check < _criticalSuccess;
        }

        public bool IsCriticalFailure(DiceRoll roll, double check)
        {
            return roll - check > _criticalFailure;
        }

        public bool IsSuccess(DiceRoll roll, double check)
        {
            return roll - check <= 0;
        }

        public double GetSuccessProbability(double check)
        {
            return MathHelper.SuccessProbability(check);
        }

        public double GetCriticalSuccessProbability(double check)
        {
            return MathHelper.SuccessProbability(check + _criticalSuccess);
        }

        public double GetCriticalFailureProbability(double check)
        {
            return 1.0-MathHelper.SuccessProbability(check + _criticalFailure);
        }
    }
}