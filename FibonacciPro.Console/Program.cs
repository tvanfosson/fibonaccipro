using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

using FibonacciPro.ConsoleApplication.IO;

namespace FibonacciPro.ConsoleApplication
{
    class Program
    {
        private static Options _options;

        static void Main(string[] args)
        {
            _options = ParseOptions(args);

            try
            {
                var inputHandler = GetInputHandler();
                var calculator = new FibonacciCalculator();
                var number = inputHandler.GetNumber();

                var result = _options.UseGenerator 
                                ? calculator.CalculateEnumerable(number)
                                : calculator.Calculate(number);

                var outputHandler = GetOutputHandler();
                outputHandler.Write(result);
            }
            catch (ApplicationException ex) { Console.Error.Write(ex.Message); Environment.Exit(CommandLine.Parser.DefaultExitCodeFail); }
            catch (ArgumentException ex) { Console.Error.Write(ex.Message); Environment.Exit(CommandLine.Parser.DefaultExitCodeFail); }
        }

        private static Options ParseOptions(string[] args)
        {
            var options = new Options();

            CommandLine.Parser.Default.ParseArgumentsStrict(args, options);

            if (options.InputNumber <= 0 && string.IsNullOrWhiteSpace(options.InputFile) && !options.UseInteractiveMode())
            {
                Console.Error.Write(CommandLine.Text.HelpText.AutoBuild(options));
                Environment.Exit(CommandLine.Parser.DefaultExitCodeFail);
            }

            return options;
        }

        private static IInputHandler GetInputHandler()
        {
            if (_options.UseInteractiveMode())
            {
                return new ConsoleIOHandler();
            }

            if (_options.InputNumber > 0)
            {
                return new GenericIOHandler() { InputHandler = () => _options.InputNumber };
            }

            switch (_options.InputFileType)
            {
                default:
                case Options.FileType.Undefined:
                case Options.FileType.PlainText:
                    return new TextFileIOHandler(_options.InputFile);
                case Options.FileType.Xml:
                    return new XmlIOHandler(_options.InputFile);
            }


        }

        private static IOutputHandler GetOutputHandler()
        {
            IOutputHandler result = new ConsoleIOHandler();

            if (!string.IsNullOrWhiteSpace(_options.OutputFile))
            {
                switch (_options.OutputFileType)
                {
                    default:
                    case Options.FileType.PlainText:
                        return new TextFileIOHandler(_options.OutputFile);
                    case Options.FileType.Xml:
                        return new XmlIOHandler(_options.OutputFile);
                }
            }

            return result;
        }
    }
}
