namespace ZeroExpectationRolePlayingSystem
{
    public class RolePlayingSystem
    {
        private double? _criticalSuccess = double.MinValue;
        private double? _criticalFailure = double.MaxValue;

        public RolePlayingSystem WithCriticalSuccessProbability(double probability)
        {
            _criticalSuccess = MathHelper.ArgumentFromProbability(probability);
            return this;
        }

        public RolePlayingSystem WithCriticalSuccessOffset(double offset)
        {
            _criticalSuccess = offset;
            return this;
        }

        public RolePlayingSystem WithCriticalFailureProbability(double probability)
        {
            _criticalFailure = MathHelper.ArgumentFromProbability(1.0 - probability);
            return this;
        }

        public RolePlayingSystem WithCriticalFailureOffset(double offset)
        {
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
    }
}