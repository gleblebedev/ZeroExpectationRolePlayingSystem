using System.Globalization;

namespace ZeroExpectationRolePlayingSystem
{
    public struct SkillCheck
    {
        private readonly double _value;

        public SkillCheck(double value)
        {
            _value = value;
        }

        public static implicit operator double(SkillCheck roll)
        {
            return roll._value;
        }
        public static implicit operator SkillCheck(double roll)
        {
            return new SkillCheck(roll);
        }
        public static implicit operator SkillCheck(float roll)
        {
            return new SkillCheck((double)roll);
        }

        public override string ToString()
        {
            return _value.ToString(CultureInfo.InvariantCulture);
        }

        public static SkillCheck operator+(SkillCheck check, SkillCheckOffset offset)
        {
            return new SkillCheck(check._value + (double)offset);
        }
        public static SkillCheck operator-(SkillCheck check, SkillCheckOffset offset)
        {
            return new SkillCheck(check._value - (double)offset);
        }
        public double SuccessProbability
        {
            get { return NDice.EvaluateProbability(_value); }
        }
    }
}