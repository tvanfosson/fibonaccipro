using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

using FakeItEasy;

using Fibonacci.Lib.Calculators;
using Fibonacci.Lib.IO;

using FibonacciPro.ConsoleApplication;
using FibonacciPro.ConsoleApplication.IO;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FibonacciPro.Tests
{
    [TestClass]
    public class ProgramFlowTests
    {
        [TestMethod]
        public void interactive_uses_console_handler_input()
        {
            //Arrange
            var options = new Options()
            {
                InteractiveMode = true
            };

            //Act
            var handler = Program.GetInputHandler(options);

            //Assert
            Assert.IsInstanceOfType(handler, typeof(ConsoleIOHandler));
        }

        [TestMethod]
        public void interactive_and_input_file_uses_file_handler_input()
        {
            //Arrange
            var options = new Options()
            {
                InteractiveMode = true,
                InputFile = "input.txt"
            };

            //Act
            var handler = Program.GetInputHandler(options);

            //Assert
            Assert.IsInstanceOfType(handler, typeof(TextFileIOHandler));
        }

        [TestMethod]
        public void interactive_and_direct_input_uses_direct_input()
        {
            //Arrange
            var options = new Options()
            {
                InteractiveMode = true,
                InputNumber = 23
            };

            //Act
            var handler = Program.GetInputHandler(options);

            //Assert
            Assert.IsInstanceOfType(handler, typeof(GenericIOHandler));
        }

        [TestMethod]
        public void interactive_and_direct_and_file_input_uses_direct_input()
        {
            //Arrange
            var options = new Options()
            {
                InteractiveMode = true,
                InputNumber = 23,
                InputFile = "input.txt"
            };

            //Act
            var handler = Program.GetInputHandler(options);

            //Assert
            Assert.IsInstanceOfType(handler, typeof(GenericIOHandler));
        }

        [TestMethod]
        public void xml_file_input_uses_xml_input()
        {
            //Arrange
            var options = new Options()
            {
                InteractiveMode = true,
                InputNumber = 23,
                InputFile = "input.xml"
            };

            //Act
            var handler = Program.GetInputHandler(options);

            //Assert
            Assert.IsInstanceOfType(handler, typeof(GenericIOHandler));
        }

        [TestMethod]
        public void output_defaults_to_console()
        {
            //Arrange
            var options = new Options()
            {
                InputNumber = 23,
            };

            //Act
            var handler = Program.GetOutputHandler(options);

            //Assert
            Assert.IsInstanceOfType(handler, typeof(ConsoleIOHandler));
        }

        [TestMethod]
        public void output_with_text_users_text_handler()
        {
            //Arrange
            var options = new Options()
            {
                InputNumber = 23,
                OutputFile = "file.txt"
            };

            //Act
            var handler = Program.GetOutputHandler(options);

            //Assert
            Assert.IsInstanceOfType(handler, typeof(TextFileIOHandler));
        }

        [TestMethod]
        public void output_with_xml_users_xml_handler()
        {
            //Arrange
            var options = new Options()
            {
                InputNumber = 23,
                OutputFile = "file.xml"
            };

            //Act
            var handler = Program.GetOutputHandler(options);

            //Assert
            Assert.IsInstanceOfType(handler, typeof(XmlIOHandler));
        }

        [TestMethod]
        public void calculator_defaults_to_array_calculator()
        {
            //Arrange
            var options = new Options()
            {
                InputNumber = 23
            };

            //Act
            var handler = Program.GetCalculator(options);

            //Assert
            Assert.IsInstanceOfType(handler, typeof(ArrayCalculator));
        }

        [TestMethod]
        public void generator_flag_uses_generator_calculator()
        {
            //Arrange
            var options = new Options()
            {
                InputNumber = 23,
                UseGenerator = true
            };

            //Act
            var handler = Program.GetCalculator(options);

            //Assert
            Assert.IsInstanceOfType(handler, typeof(GeneratorCalculator));
        }

        [TestMethod]
        public void execution_retrieves_number_from_input_passes_to_calculator_and_exports_to_output()
        {
            //Arrange
            var input = A.Fake<IInputHandler>();
            A.CallTo(() => input.GetNumber())
                .Returns(5);

            var expectedResult = new BigInteger[] { 2, 3, 4 };
            var calculator = A.Fake<IFibonacciCalculator>();
            A.CallTo(calculator)
                .WithReturnType<IEnumerable<BigInteger>>()
                .Returns(expectedResult);

            var output = A.Fake<IOutputHandler>();

            //Act
            Program.CalculateAndWriteResults(input, output, calculator);

            //Assert
            A.CallTo(() => input.GetNumber()).MustHaveHappened();
            A.CallTo(() => calculator.Calculate(5)).MustHaveHappened();
            A.CallTo(() => output.Write(expectedResult)).MustHaveHappened();
        }
    }
}