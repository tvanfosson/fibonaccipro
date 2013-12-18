using System.Collections.Generic;
using System.Numerics;

namespace FibonacciPro.ConsoleApplication.IO
{
    public interface IOutputHandler
    {
        void Write(IEnumerable<BigInteger> results);
    }
}
