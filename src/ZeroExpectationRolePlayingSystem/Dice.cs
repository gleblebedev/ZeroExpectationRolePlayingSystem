using System;

namespace ZeroExpectationRolePlayingSystem
{
    public class Dice
    {
        private double? _generatedValue;

        private readonly Func<double> _randomNumberGenerator;

        public Dice(Func<double> randomNumberGenerator)
        {
            _randomNumberGenerator = randomNumberGenerator;
        }

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

            var u1Log = Math.Sqrt(-2.0 * Math.Log(u1));
            _generatedValue = u1Log * Math.Sin(u2);
            return u1Log * Math.Cos(u2);
        }
    }
}