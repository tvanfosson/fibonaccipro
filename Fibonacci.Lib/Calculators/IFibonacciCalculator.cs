using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Fibonacci.Lib.Calculators
{
    public interface IFibonacciCalculator
    {
        /// <summary>
        ///     Computes the first n digits of the fibonacci sequence
        /// </summary>
        /// <param name="n">number of digits of the fibonacci sequence to compute</param>
        /// <returns>an enumerable containing the first n numbers in the fibonacci sequence</returns>
        IEnumerable<BigInteger> Calculate(int n);
    }
}