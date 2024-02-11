using System.Globalization;

namespace ZeroExpectationRolePlayingSystem
{
    /// <summary>
    /// Structure to encapsulate dice roll.
    /// It is interchangeable with double.
    /// </summary>
    public struct DiceRoll
    {
        /// <summary>
        /// Dice roll value.
        /// </summary>
        private readonly double _value;

        /// <summary>
        /// Construct dice roll.
        /// </summary>
        /// <param name="value">Dice roll value.</param>
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

        /// <summary>
        /// Dice roll value.
        /// </summary>
        public double Value => _value;

        /// <inheritdoc/>
        public override string ToString()
        {
            return _value.ToString(CultureInfo.InvariantCulture);
        }
    }
}