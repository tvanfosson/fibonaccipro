using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace FibonacciPro.ConsoleApplication.IO
{
    public class ConsoleIOHandler : IInputHandler, IOutputHandler
    {
        public int GetNumber()
        {
            var result = 0;

            do
            {
                Console.Write("Please enter the number of sequences to calculate: ");
                var input = Console.ReadLine();
                int.TryParse(input, out result);

            } while (result == 0);

            return result;
        }

        public void Write(IEnumerable<BigInteger> results)
        {
            Console.Write(string.Join(" ", results.Select(r => r.ToString("R0"))));
        }
    }
}
