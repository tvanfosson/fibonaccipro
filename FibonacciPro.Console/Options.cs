using System;
using System.Collections.Generic;
using System.Linq;

using CommandLine;
using CommandLine.Text;

namespace FibonacciPro.ConsoleApplication
{
    /// <summary>
    ///     Options to be passed into the application from the command line
    /// </summary>
    public class Options
    {
        /// <summary>
        ///     User indicated flag for Interactive Mode
        /// </summary>
        /// <remarks>
        ///     Use the UseInteractiveMode() function to test whether or not to use interactive mode.
        ///     Special execptions may override this flag, such as ambiguity between flag and an input file.
        /// </remarks>
        [Option('t', "interactive", HelpText = "Enables interactive mode where the user will be prompted for input values.")]
        public bool InteractiveMode { get; set; }

        /// <summary>
        ///     Handles special exceptions to override Interactive mode argument.
        /// </summary>
        /// <returns></returns>
        public bool UseInteractiveMode()
        {
            //No valid direct input value was provided in the command line
            //and no input file was specified
            //and interactive mode was indicated
            return InputNumber <= 0 && string.IsNullOrWhiteSpace(InputFile) && InteractiveMode;
        }

        [Option('i', "input-file", HelpText = "File path to input file. XML or plain text accepted.")]
        public string InputFile { get; set; }

        public enum FileType
        {
            Undefined,
            PlainText,
            Xml
        }

        [Option('o', "output-file", HelpText = "File path to output file. Files ending in .xml will be an XML format.")]
        public string OutputFile { get; set; }

        public FileType OutputFileType
        {
            get { return GetFileTypeFromPath(OutputFile); }
        }

        public FileType InputFileType
        {
            get { return GetFileTypeFromPath(InputFile); }
        }

        private static FileType GetFileTypeFromPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path) || !path.Contains('.'))
            {
                return FileType.Undefined;
            }

            var extension = path.Substring(path.LastIndexOf('.'));

            switch (extension)
            {
                case ".txt":
                    return FileType.PlainText;
                case ".xml":
                    return FileType.Xml;
            }

            return FileType.Undefined;
        }

        [Option('n', "number", HelpText = "Number of items to compute in the sequence. Must be greater than 0.")]
        [ValueOption(0)]
        public int InputNumber { get; set; }

        /// <summary>
        ///     User indicated flag as to whether the generator method or an array of values should be computed.
        /// </summary>
        [Option('g', "generator", HelpText = "Use generator method to produce the sequence.")]
        public bool UseGenerator { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this, current => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}