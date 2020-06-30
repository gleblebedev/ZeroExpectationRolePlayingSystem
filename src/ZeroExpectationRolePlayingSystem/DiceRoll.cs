using System.Globalization;

namespace ZeroExpectationRolePlayingSystem
{
    public struct DiceRoll
    {
        private readonly double _value;

        public DiceRoll(double value)
        {
            _value = value;
        }

        public static implicit operator double (DiceRoll roll)
        {
            return roll._value;
        }
        public static implicit operator DiceRoll(double roll)
        {
            return new DiceRoll(roll);
        }

        public override string ToString()
        {
            return _value.ToString(CultureInfo.InvariantCulture);
        }
    }
}