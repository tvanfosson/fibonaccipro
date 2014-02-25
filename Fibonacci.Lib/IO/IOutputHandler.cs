using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Fibonacci.Lib.IO
{
    public interface IOutputHandler
    {
        void Write(IEnumerable<BigInteger> results);
    }
}