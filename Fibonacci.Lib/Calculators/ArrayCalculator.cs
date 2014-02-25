using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Fibonacci.Lib.Calculators
{
    public class ArrayCalculator : IFibonacciCalculator
    {
        public IEnumerable<BigInteger> Calculate(int n)
        {
            if (n <= 0)
            {
                throw new ArgumentException("n must be greater than 0", "n");
            }

            //By definition
            if (n == 1)
            {
                return new BigInteger[] { 0 };
            }

            //By definition
            if (n == 2)
            {
                return new BigInteger[] { 0, 1 };
            }

            var result = new BigInteger[n];

            result[0] = 0;
            result[1] = 1;

            for (var i = 0; i < n - 2; i++)
            {
                result[i + 2] = result[i] + result[i + 1];
            }

            return result;
        }
    }
}