using System;
using System.Collections.Generic;
using System.Linq;

using Fibonacci.Lib.Calculators;
using Fibonacci.Lib.IO;

using FibonacciPro.ConsoleApplication.IO;

namespace FibonacciPro.ConsoleApplication
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                var options = ParseOptions(args);
                var inputHandler = GetInputHandler(options);
                var calculator = GetCalculator(options);
                var outputHandler = GetOutputHandler(options);

                CalculateAndWriteResults(inputHandler, outputHandler, calculator);
            }
            catch (ApplicationException ex)
            {
                Console.Error.Write(ex.Message);
                Environment.Exit(CommandLine.Parser.DefaultExitCodeFail);
            }
            catch (ArgumentException ex)
            {
                Console.Error.Write(ex.Message);
                Environment.Exit(CommandLine.Parser.DefaultExitCodeFail);
            }
        }

        public static void CalculateAndWriteResults(IInputHandler inputHandler, IOutputHandler outputHandler, IFibonacciCalculator calculator)
        {
            var number = inputHandler.GetNumber();
            var result = calculator.Calculate(number);
            outputHandler.Write(result);
        }

        public static Options ParseOptions(string[] args)
        {
            var options = new Options();

            CommandLine.Parser.Default.ParseArgumentsStrict(args, options);

            if (options.InputNumber > 0 || !string.IsNullOrWhiteSpace(options.InputFile) || options.UseInteractiveMode())
            {
                return options;
            }
            Console.Error.Write(CommandLine.Text.HelpText.AutoBuild(options));
            throw new ArgumentException("Invalid paramater set", "args");
        }

        public static IInputHandler GetInputHandler(Options options)
        {
            if (options.UseInteractiveMode())
            {
                return new ConsoleIOHandler();
            }

            if (options.InputNumber > 0)
            {
                return new GenericIOHandler { InputHandler = () => options.InputNumber };
            }

            switch (options.InputFileType)
            {
                default:
                case Options.FileType.Undefined:
                case Options.FileType.PlainText:
                    return new TextFileIOHandler(options.InputFile);
                case Options.FileType.Xml:
                    return new XmlIOHandler(options.InputFile);
            }
        }

        public static IOutputHandler GetOutputHandler(Options options)
        {
            IOutputHandler result = new ConsoleIOHandler();

            if (string.IsNullOrWhiteSpace(options.OutputFile))
            {
                return result;
            }

            switch (options.OutputFileType)
            {
                default:
                case Options.FileType.PlainText:
                    return new TextFileIOHandler(options.OutputFile);
                case Options.FileType.Xml:
                    return new XmlIOHandler(options.OutputFile);
            }
        }

        public static IFibonacciCalculator GetCalculator(Options options)
        {
            if (options.UseGenerator)
            {
                return new GeneratorCalculator();
            }

            return new ArrayCalculator();
        }
    }
}