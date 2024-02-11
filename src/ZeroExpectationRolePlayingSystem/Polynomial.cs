//Copyright(c) 2002 - 2022 Math.NET

//Permission is hereby granted, free of charge, to any person obtaining
//a copy of this software and associated documentation files (the
//"Software"), to deal in the Software without restriction, including
//without limitation the rights to use, copy, modify, merge, publish,
//distribute, sublicense, and/or sell copies of the Software, and to
//permit persons to whom the Software is furnished to do so, subject to
//the following conditions:

//The above copyright notice and this permission notice shall be
//included in all copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
//EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
//MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
//NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
//LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
//OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
//WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;

namespace ZeroExpectationRolePlayingSystem
{
    /// <summary>
    /// A single-variable polynomial with real-valued coefficients and non-negative exponents.
    /// </summary>
    public class Polynomial
    {
        /// <summary>
        /// Evaluate a polynomial at point x.
        /// Coefficients are ordered ascending by power with power k at index k.
        /// Example: coefficients [3,-1,2] represent y=2x^2-x+3.
        /// </summary>
        /// <param name="z">The location where to evaluate the polynomial at.</param>
        /// <param name="coefficients">The coefficients of the polynomial, coefficient for power k at index k.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="coefficients"/> is a null reference.
        /// </exception>
        public static double Evaluate(double z, params double[] coefficients)
        {

            // 2020-10-07 jbialogrodzki #730 Since this is public API we should probably
            // handle null arguments? It doesn't seem to have been done consistently in this class though.
            if (coefficients == null)
            {
                throw new ArgumentNullException(nameof(coefficients));
            }

            // 2020-10-07 jbialogrodzki #730 Zero polynomials need explicit handling.
            // Without this check, we attempted to peek coefficients at negative indices!
            int n = coefficients.Length;
            if (n == 0)
            {
                return 0;
            }

            double sum = coefficients[n - 1];
            for (int i = n - 2; i >= 0; --i)
            {
                sum *= z;
                sum += coefficients[i];
            }

            return sum;

        }
    }
}