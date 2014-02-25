using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

namespace Fibonacci.Lib.IO
{
    public class TextFileIOHandler : IInputHandler, IOutputHandler
    {
        private readonly string _path;

        public TextFileIOHandler(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException("path must not be an empty string", "path");
            }

            _path = path;
        }

        public int GetNumber()
        {
            int number;

            if (!File.Exists(_path))
            {
                throw new ArgumentException("path for input files must be to an actual path to a file");
            }

            try
            {
                using (var fileReader = new StreamReader(_path))
                {
                    if (!int.TryParse(fileReader.ReadLine(), out number))
                    {
                        throw new ApplicationException("Input file did not contain a valid number on the first line.");
                    }
                }
            }
            catch (IOException ex)
            {
                throw new ApplicationException("There was a problem reading the file", ex);
            }

            return number;
        }

        public void Write(IEnumerable<BigInteger> results)
        {
            try
            {
                using (var writer = new StreamWriter(_path, append: false))
                {
                    foreach (var result in results)
                    {
                        writer.WriteLine(result.ToString("R0"));
                    }
                }
            }
            catch (IOException ex)
            {
                throw new ApplicationException("There was an error writing to the output file.", ex);
            }
        }
    }
}