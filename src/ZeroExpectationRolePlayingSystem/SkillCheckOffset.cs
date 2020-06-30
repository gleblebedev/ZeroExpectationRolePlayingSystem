using System.Globalization;

namespace ZeroExpectationRolePlayingSystem
{
    public struct SkillCheckOffset
    {
        private readonly double _value;

        public SkillCheckOffset(double value)
        {
            _value = value;
        }

        public static implicit operator double(SkillCheckOffset roll)
        {
            return roll._value;
        }
        public static implicit operator SkillCheckOffset(double roll)
        {
            return new SkillCheckOffset(roll);
        }
        public static implicit operator SkillCheckOffset(float roll)
        {
            return new SkillCheckOffset((double)roll);
        }

        public override string ToString()
        {
            return _value.ToString(CultureInfo.InvariantCulture);
        }
    }
}