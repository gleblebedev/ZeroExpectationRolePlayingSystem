using System;

namespace ZeroExpectationRolePlayingSystem
{
    public class RolePlayingSystem
    {
        private double _criticalSuccess = double.MinValue;
        private double _criticalFailure = double.MaxValue;

        public RolePlayingSystem WithCriticalSuccessProbability(double probability)
        {
            return WithCriticalSuccessOffset(MathHelper.ArgumentFromProbability(probability));
        }

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
            return MathHelper.SuccessProbability(check);
        }

        public double GetCriticalSuccessProbability(double check)
        {
            return MathHelper.SuccessProbability(check+ _criticalSuccess);
        }

        public double GetCriticalFailureProbability(double check)
        {
            return 1.0-MathHelper.SuccessProbability(check + _criticalFailure);
        }
    }
}