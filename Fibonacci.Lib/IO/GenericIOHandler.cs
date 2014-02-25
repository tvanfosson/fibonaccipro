using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Fibonacci.Lib.IO
{
    public class GenericIOHandler : IInputHandler, IOutputHandler
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public Action<IEnumerable<BigInteger>> OutputHandler { get; set; }
        public Func<int> InputHandler { get; set; }

        public void Write(IEnumerable<BigInteger> results)
        {
            OutputHandler(results);
        }

        public int GetNumber()
        {
            return InputHandler();
        }
    }
}