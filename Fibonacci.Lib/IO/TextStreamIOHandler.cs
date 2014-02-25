using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Fibonacci.Lib.IO
{
    public class TextStreamIOHandler : IInputHandler, IOutputHandler
    {
        private readonly TextReader _inputStream;
        private readonly TextWriter _outputStream;

        public TextStreamIOHandler(TextReader inputStream, TextWriter outputStream)
        {
            _inputStream = inputStream;
            _outputStream = outputStream;
        }

        public void Write(IEnumerable<System.Numerics.BigInteger> results)
        {
            _outputStream.Write((string.Join(" ", results.Select(r => r.ToString("R0")))));
        }

        public int GetNumber()
        {
            int result;

            do
            {
                _outputStream.Write("Please enter the number of sequences to calculate: ");
                var input = _inputStream.ReadLine();
                int.TryParse(input, out result);
            } while (result == 0);

            return result;
        }
    }
}