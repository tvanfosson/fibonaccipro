using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Xml.Linq;

using FibonacciPro.Tests.Extensions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FibonacciPro.Tests
{
    [TestClass]
    public class AcceptanceTests
    {
        public const int SUCCESS = 0;
        public const int ERROR = 1;
        public const int TIMEOUT_MILLISECONDS = 500;

        [TestMethod]
        public void n_equals_1_returns_first_number()
        {
            //Arrange
            var expectedOutput = "0";

            //Act
            var results = FibPro("1");

            //Assert
            Assert.AreEqual(expectedOutput, results.StandardOut);
            Assert.AreEqual(SUCCESS, results.ExitCode);
        }

        [TestMethod]
        public void n_equals_2_returns_first_two_numbers()
        {
            //Arrange
            var expectedOutput = "0 1";

            //Act
            var results = FibPro("2");

            //Assert
            Assert.AreEqual(expectedOutput, results.StandardOut);
            Assert.AreEqual(SUCCESS, results.ExitCode);
        }

        [TestMethod]
        public void n_equals_3_returns_first_three_numbers()
        {
            //Arrange
            var expectedOutput = "0 1 1";

            //Act
            var results = FibPro("3");

            //Assert
            Assert.AreEqual(expectedOutput, results.StandardOut);
            Assert.AreEqual(SUCCESS, results.ExitCode);
        }

        [TestMethod]
        public void n_equals_4_returns_first_four_numbers()
        {
            //Arrange
            var expectedOutput = "0 1 1 2";

            //Act
            var results = FibPro("4");

            //Assert
            Assert.AreEqual(expectedOutput, results.StandardOut);
            Assert.AreEqual(SUCCESS, results.ExitCode);
        }

        [TestMethod]
        public void n_equals_negative_one_returns_error_exit_code_and_error_message()
        {
            //Arrange

            //Act
            var results = FibPro("-1");

            //Assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(results.StandardError));
            Assert.AreEqual(ERROR, results.ExitCode);
        }

        [TestMethod]
        public void non_numeric_n_returns_error_exit_code_and_error_message()
        {
            //Arrange

            //Act
            var results = FibPro("abcd");

            //Assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(results.StandardError));
            Assert.AreEqual(ERROR, results.ExitCode);
        }

        [TestMethod]
        public void can_compute_50_results()
        {
            //Arrange
            var fib49 = "7778742049";

            //Act
            var results = FibPro("50");
            var sequence = results.StandardOut.Split(' ');

            //Assert
            Assert.AreEqual(fib49, sequence.Last());
            Assert.AreEqual(SUCCESS, results.ExitCode);
        }

        [TestMethod]
        public void can_compute_100_results()
        {
            //Arrange fib(99)
            var fib99 = "218922995834555169026";

            //Act
            var results = FibPro("100");
            var sequence = results.StandardOut.Split(' ');

            //Assert
            Assert.AreEqual(fib99, sequence.Last());
            Assert.AreEqual(SUCCESS, results.ExitCode);
        }

        [TestMethod]
        public void can_compute_200_results()
        {
            //Arrange fib(199)
            var fib199 = "173402521172797813159685037284371942044301";

            //Act
            var results = FibPro("200");
            var sequence = results.StandardOut.Split(' ');

            //Assert
            Assert.AreEqual(fib199, sequence.Last());
            Assert.AreEqual(SUCCESS, results.ExitCode);
        }

        [TestMethod]
        public void can_compute_500_results()
        {
            //Arrange fib(499)
            BigInteger fib499 = new BigInteger(997)
                                * new BigInteger(492013)
                                * new BigInteger(3074837)
                                * BigInteger.Parse("57128608773902499888960755640879595424590122515509365520944618528846769695130838292100993");

            //Act
            var results = FibPro("500");
            var sequence = results.StandardOut.Split(' ');

            //Assert
            Assert.AreEqual(fib499.ToString("R0"), sequence.Last());
            Assert.AreEqual(SUCCESS, results.ExitCode);
        }

        [TestMethod]
        public void can_compute_1000_results()
        {
            //Arrange fib(999)
            BigInteger fib999 = new BigInteger(2)
                                * new BigInteger(17)
                                * new BigInteger(53)
                                * new BigInteger(73)
                                * new BigInteger(109)
                                * new BigInteger(149)
                                * new BigInteger(1997)
                                * new BigInteger(2221)
                                * new BigInteger(12653)
                                * new BigInteger(16061684237)
                                * new BigInteger(124134848933957)
                                * new BigInteger(1459000305513721)
                                * BigInteger.Parse("930507731557590226767593761")
                                * BigInteger.Parse("1687733481506255251903139456476245146806742007876216630876557")
                                * BigInteger.Parse("49044806374722940739127188459343134898237532255227554514970877");

            //Act
            var results = FibPro("1000");
            var sequence = results.StandardOut.Split(' ');

            //Assert
            Assert.AreEqual(fib999.ToString("R0"), sequence.Last());
            Assert.AreEqual(SUCCESS, results.ExitCode);
        }

        [TestMethod]
        public void can_compute_1000_results_using_generator()
        {
            //Arrange fib(999)
            BigInteger fib999 = new BigInteger(2)
                                * new BigInteger(17)
                                * new BigInteger(53)
                                * new BigInteger(73)
                                * new BigInteger(109)
                                * new BigInteger(149)
                                * new BigInteger(1997)
                                * new BigInteger(2221)
                                * new BigInteger(12653)
                                * new BigInteger(16061684237)
                                * new BigInteger(124134848933957)
                                * new BigInteger(1459000305513721)
                                * BigInteger.Parse("930507731557590226767593761")
                                * BigInteger.Parse("1687733481506255251903139456476245146806742007876216630876557")
                                * BigInteger.Parse("49044806374722940739127188459343134898237532255227554514970877");

            //Act
            var results = FibPro("--g 1000");
            var sequence = results.StandardOut.Split(' ');

            //Assert
            Assert.AreEqual(fib999.ToString("R0"), sequence.Last());
            Assert.AreEqual(SUCCESS, results.ExitCode);
        }

        [TestMethod]
        public void users_can_give_interactive_input()
        {
            //Arrange
            var fib4 = "3";

            //Act
            var results = FibPro("--interactive", "5\n");
            var sequence = results.StandardOut.Split(' ');

            //Assert
            Assert.AreEqual(fib4, sequence.Last());
            Assert.AreEqual(SUCCESS, results.ExitCode);
        }

        [TestMethod]
        public void can_take_text_input()
        {
            //Arrange
            var fib4 = "3";
            //See input.txt

            //Act
            var results = FibPro("-i input.txt");
            var sequence = results.StandardOut.Split(' ');

            //Assert
            Assert.AreEqual(fib4, sequence.Last());
            Assert.AreEqual(SUCCESS, results.ExitCode);
        }

        [TestMethod]
        public void text_input_with_negative_input_fails()
        {
            //Arrange
            //See negative-input.txt file

            //Act
            var results = FibPro("-i negative-input.txt");

            //Assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(results.StandardError));
            Assert.AreEqual(ERROR, results.ExitCode);
        }

        [TestMethod]
        public void text_input_with_non_numeric_input_fails()
        {
            //Arrange
            //See non-numeric-input.txt file

            //Act
            var results = FibPro("-i non-numeric-input.txt");

            //Assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(results.StandardError));
            Assert.AreEqual(ERROR, results.ExitCode);
        }

        [TestMethod]
        public void can_take_xml_input()
        {
            //Arrange
            var fib21 = "10946";
            //see input.xml

            //Act
            var results = FibPro("-i input.xml");
            var sequence = results.StandardOut.Split(' ');

            //Assert
            Assert.AreEqual(fib21, sequence.Last());
            Assert.AreEqual(SUCCESS, results.ExitCode);
        }

        [TestMethod]
        public void xml_input_with_negative_input_fails()
        {
            //Arrange
            //See negative-input.xml file

            //Act
            var results = FibPro("-i negative-input.xml");

            //Assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(results.StandardError));
            Assert.AreEqual(ERROR, results.ExitCode);
        }

        [TestMethod]
        public void xml_input_with_non_numeric_input_fails()
        {
            //Arrange
            //See non-numeric-input.xml file

            //Act
            var results = FibPro("-i non-numeric-input.xml");

            //Assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(results.StandardError));
            Assert.AreEqual(ERROR, results.ExitCode);
        }

        [TestMethod]
        public void xml_input_with_invalid_schema_elements_fails()
        {
            //Arrange
            //See invalid-schema-elements-input.xml file

            //Act
            var results = FibPro("-i invalid-schema-elements-input.xml");

            //Assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(results.StandardError));
            Assert.AreEqual(ERROR, results.ExitCode);
        }

        [TestMethod]
        public void xml_input_with_invalid_markup_fails()
        {
            //Arrange
            //See invalid-syntax-input.xml file

            //Act
            var results = FibPro("-i invalid-markup-input.xml");

            //Assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(results.StandardError));
            Assert.AreEqual(ERROR, results.ExitCode);
        }

        [TestMethod]
        public void can_output_text_files()
        {
            //Arrange
            var fib21 = "10946";
            //see input.xml

            //Act
            var results = FibPro("22 -o output.txt");

            //Assert
            var lastItem = GetLastItemFromTextFile("output.txt");

            Assert.AreEqual(fib21, lastItem);
            Assert.AreEqual(SUCCESS, results.ExitCode);
        }

        [TestMethod]
        public void can_output_xml_files()
        {
            //Arrange
            var fib21 = "10946";
            //see input.xml

            //Act
            var results = FibPro("22 -o output.xml");

            //Assert
            var lastItem = GetLastItemFromXmlFile("output.xml");

            Assert.AreEqual(fib21, lastItem);
            Assert.AreEqual(SUCCESS, results.ExitCode);
        }

        private string GetLastItemFromTextFile(string path)
        {
            string lastItem = null;

            using (var streamReader = new StreamReader(path))
            {
                var results = streamReader.ReadToEnd();
                lastItem = results.Split('\n').SecondLast().Trim(); //Last item is a blank line
            }

            return lastItem;
        }

        private string GetLastItemFromXmlFile(string path)
        {
            var doc = XDocument.Load(path);

            return doc.Element("fiboutput").Elements("result").Last().Value;
        }

        /// <summary>
        ///     Invokes FibPro and returns a string of the standard output
        /// </summary>
        private FibProOutput FibPro(string args, string interactiveInput = null)
        {
            var output = new StringBuilder();
            var error = new StringBuilder();
            var resultCode = 0;

            using (var process = new Process())
            {
                process.StartInfo = new ProcessStartInfo()
                {
                    Arguments = args,
                    FileName = "fibpro.exe",
                    RedirectStandardOutput = true,
                    RedirectStandardInput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                };

                using (var outputWaitHandle = new AutoResetEvent(false))
                using (var errorWaitHandle = new AutoResetEvent(false))
                {
                    process.OutputDataReceived += (sender, e) =>
                    {
                        if (e.Data == null)
                        {
                            outputWaitHandle.Set();
                        }
                        else
                        {
                            output.Append(e.Data);
                        }
                    };

                    process.ErrorDataReceived += (sender, e) =>
                    {
                        if (e.Data == null)
                        {
                            errorWaitHandle.Set();
                        }
                        else
                        {
                            error.Append(e.Data);
                        }
                    };

                    process.Start();

                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();

                    if (!string.IsNullOrWhiteSpace(interactiveInput))
                    {
                        process.StandardInput.WriteLine(interactiveInput);
                    }

                    if (process.WaitForExit(TIMEOUT_MILLISECONDS) &&
                        outputWaitHandle.WaitOne(TIMEOUT_MILLISECONDS) &&
                        errorWaitHandle.WaitOne(TIMEOUT_MILLISECONDS))
                    {
                        resultCode = process.ExitCode;
                    }
                    else
                    {
                        Assert.Fail(string.Format("FibPro timed out ({0}ms)", TIMEOUT_MILLISECONDS));
                    }
                }
            }

            return new FibProOutput
            {
                StandardOut = output.ToString(),
                StandardError = error.ToString(),
                ExitCode = resultCode
            };
        }

        public class FibProOutput
        {
            public string StandardOut { get; set; }
            public string StandardError { get; set; }
            public int ExitCode { get; set; }
        }
    }
}