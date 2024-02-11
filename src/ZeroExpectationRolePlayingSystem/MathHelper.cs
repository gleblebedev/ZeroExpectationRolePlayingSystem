using System;

namespace ZeroExpectationRolePlayingSystem
{
    public class MathHelper
    {
        public const double Sqrt2 = 1.4142135623730950488016887242097;

        public static double ArgumentFromProbability(double probability)
        {
            if (probability <= 0)
                return double.NegativeInfinity;
            if (probability >= 1)
                return double.PositiveInfinity;

            return -1.4142135623730950488016887242097 * SpecialFunctions.ErfcInv(2.0 * probability);
        }

        public static double SuccessProbability(double x, double mean = 0.0, double stdDev = 1.0)
        {
            double z = (x - mean) / stdDev;
            return (SpecialFunctions.Erf(z * 0.70710678118654752440084436210485) + 1.0) * 0.5;
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