using System;
using MathNet.Numerics;

namespace ZeroExpectationRolePlayingSystem
{
    public class MathHelper
    {
        public const double Sqrt2 = 1.4142135623730950488016887242097;
        //private double? _generatedValue;

        public static readonly Random DefaultRandomGenerator = new Random();

        public static double BoxMuller()
        {
            return BoxMuller(DefaultRandomGenerator);
        }

        public static double BoxMuller(Random random)
        {
            double u1, u2;

            //if (_generatedValue.HasValue)
            //{
            //    var value = _generatedValue.Value;
            //    _generatedValue = null;
            //    return value;
            //}

            do
            {
                u1 = random.NextDouble();
                u2 = 6.283185307179586476925286766559 * random.NextDouble();
            } while (u1 <= double.Epsilon);

            var u1Log = Math.Sqrt(-2.0 * Math.Log(u1));
            //_generatedValue = u1Log * Math.Sin(u2);
            return u1Log * Math.Cos(u2);
        }

        public static double ArgumentFromProbability(double probability)
        {
            if (probability <= 0)
                return double.NegativeInfinity;
            if (probability >= 1)
                return double.PositiveInfinity;

            return -1.4142135623730950488016887242097 * SpecialFunctions.ErfcInv(2.0 * probability);
        }

        public static double SuccessProbability(double x)
        {
            return (SpecialFunctions.Erf(x * 0.70710678118654752440084436210485) + 1.0) * 0.5;

            //Via error function
            //return (ErrorFunction(x * 0.70710678118654752440084436210485) + 1.0) / 2.0;

            //Via complimentary error function:
            //return (1.0 - ErrorFunction(- x * 0.70710678118654752440084436210485)) / 2.0;
        }

        public static double ErrorFunction(double x)
        {
            double sign = 1;
            if (x < 0)
            {
                sign = -1;
                x = -x;
            }

            var t = 1.0 / (1.0 + 0.3275911 * x);
            var y =
                1.0 - ((((1.061405429 * t + -1.453152027) * t + 1.421413741) * t + -0.284496736) * t + 0.254829592) *
                t * Math.Exp(-x * x);

            return sign * y;
        }
    }
}