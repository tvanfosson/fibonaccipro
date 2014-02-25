using System;
using System.Collections.Generic;
using System.Linq;

using FibonacciPro.ConsoleApplication;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FibonacciPro.Tests
{
    [TestClass]
    public class ArgumentTests
    {
        [TestMethod]
        public void t_arg_enables_interactive_mode()
        {
            //Arrange
            var args = "-t".Split(' ');

            //Act
            var options = Program.ParseOptions(args);

            //Assert
            Assert.IsTrue(options.InteractiveMode);
            Assert.IsTrue(options.UseInteractiveMode());
        }

        [TestMethod]
        public void interactive_arg_enables_interactive_mode()
        {
            //Arrange
            var args = "--interactive".Split(' ');

            //Act
            var options = Program.ParseOptions(args);

            //Assert
            Assert.IsTrue(options.InteractiveMode);
            Assert.IsTrue(options.UseInteractiveMode());
        }

        [TestMethod]
        public void i_arg_registers_input_file()
        {
            //Arrange
            var args = "-i file.txt".Split(' ');

            //Act
            var options = Program.ParseOptions(args);

            //Assert
            Assert.IsFalse(string.IsNullOrEmpty(options.InputFile));
        }

        [TestMethod]
        public void input_file_with_txt_extension_registers_text_file()
        {
            //Arrange
            var args = "-i file.txt".Split(' ');
            var expected = Options.FileType.PlainText;

            //Act
            var options = Program.ParseOptions(args);

            //Assert
            Assert.AreEqual(expected, options.InputFileType);
        }

        [TestMethod]
        public void input_file_with_xml_extension_registers_xml_file()
        {
            //Arrange
            var args = "-i file.xml".Split(' ');
            var expected = Options.FileType.Xml;

            //Act
            var options = Program.ParseOptions(args);

            //Assert
            Assert.AreEqual(expected, options.InputFileType);
        }

        [TestMethod]
        public void input_file_with_no_extension_registers_undefined_file()
        {
            //Arrange
            var args = "-i file".Split(' ');
            var expected = Options.FileType.Undefined;

            //Act
            var options = Program.ParseOptions(args);

            //Assert
            Assert.AreEqual(expected, options.InputFileType);
        }

        [TestMethod]
        public void input_file_with_other_extension_registers_undefined_file()
        {
            //Arrange
            var args = "-i file.abc".Split(' ');
            var expected = Options.FileType.Undefined;

            //Act
            var options = Program.ParseOptions(args);

            //Assert
            Assert.AreEqual(expected, options.InputFileType);
        }

        [TestMethod]
        public void input_file_ending_with_a_dot_registers_undefined_file()
        {
            //Arrange
            var args = "-i file.".Split(' ');
            var expected = Options.FileType.Undefined;

            //Act
            var options = Program.ParseOptions(args);

            //Assert
            Assert.AreEqual(expected, options.InputFileType);
        }

        [TestMethod]
        public void input_file_ending_with_xml_registers_xml_file()
        {
            //Arrange
            var args = "-i file.xml".Split(' ');
            var expected = Options.FileType.Xml;

            //Act
            var options = Program.ParseOptions(args);

            //Assert
            Assert.AreEqual(expected, options.InputFileType);
        }

        [TestMethod]
        public void output_file_with_file_path_ending_with_xml_registers_xml_file()
        {
            //Arrange
            var args = "23 -o output.xml".Split(' ');
            var expected = Options.FileType.Xml;

            //Act
            var options = Program.ParseOptions(args);

            //Assert
            Assert.AreEqual(expected, options.OutputFileType);
        }

        [TestMethod]
        public void output_file_with_file_path_ending_with_txt_registers_txt_file()
        {
            //Arrange
            var args = "23 -o output.txt".Split(' ');
            var expected = Options.FileType.PlainText;

            //Act
            var options = Program.ParseOptions(args);

            //Assert
            Assert.AreEqual(expected, options.OutputFileType);
        }

        [TestMethod]
        public void output_file_with_file_path_ending_with_other_registers_undefined_file()
        {
            //Arrange
            var args = "23 -o output.abc".Split(' ');
            var expected = Options.FileType.Undefined;

            //Act
            var options = Program.ParseOptions(args);

            //Assert
            Assert.AreEqual(expected, options.OutputFileType);
        }

        [TestMethod]
        public void output_file_with_file_path_ending_with_a_dot_registers_undefined_file()
        {
            //Arrange
            var args = "23 -o output.".Split(' ');
            var expected = Options.FileType.Undefined;

            //Act
            var options = Program.ParseOptions(args);

            //Assert
            Assert.AreEqual(expected, options.OutputFileType);
        }

        [TestMethod]
        public void output_file_with_file_path_ending_with_no_extension_registers_undefined_file()
        {
            //Arrange
            var args = "23 -o output".Split(' ');
            var expected = Options.FileType.Undefined;

            //Act
            var options = Program.ParseOptions(args);

            //Assert
            Assert.AreEqual(expected, options.OutputFileType);
        }

        [TestMethod]
        public void output_directive_with_no_input_throws_ArgumentException()
        {
            //Arrange
            var args = "-o output.xml".Split(' ');

            //Act
            try
            {
                var options = Program.ParseOptions(args);
                Assert.Fail("Did not properly throw ArgumentException");
            }
            catch (ArgumentException)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void no_arguments_throws_ArgumentException()
        {
            //Arrange
            var args = string.Empty.Split(' ');

            //Act
            try
            {
                var options = Program.ParseOptions(args);
                Assert.Fail("Did not properly throw ArgumentException");
            }
            catch (ArgumentException)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void g_arg_enables_generated_mode()
        {
            //Arrange
            var args = "23 -g".Split(' ');

            //Act
            var options = Program.ParseOptions(args);

            //Assert
            Assert.IsTrue(options.UseGenerator);
        }

        [TestMethod]
        public void generator_arg_enables_generated_mode()
        {
            //Arrange
            var args = "23 --generator".Split(' ');

            //Act
            var options = Program.ParseOptions(args);

            //Assert
            Assert.IsTrue(options.UseGenerator);
        }

        [TestMethod]
        public void can_pass_number_for_input()
        {
            //Arrange
            var args = "23".Split(' ');
            var expected = 23;

            //Act
            var options = Program.ParseOptions(args);

            //Assert
            Assert.AreEqual(expected, options.InputNumber);
        }

        [TestMethod]
        public void interactive_mode_method_returns_true_if_interactive_flag_enabled()
        {
            //Arrange
            var args = "-t".Split(' ');

            //Act
            var options = Program.ParseOptions(args);

            //Assert
            Assert.IsTrue(options.UseInteractiveMode());
        }

        [TestMethod]
        public void interactive_mode_ignored_if_value_is_present()
        {
            //Arrange
            var args = "23 -t".Split(' ');

            //Act
            var options = Program.ParseOptions(args);

            //Assert
            Assert.IsFalse(options.UseInteractiveMode());
        }

        [TestMethod]
        public void interactive_mode_ignored_if_input_file_is_present()
        {
            //Arrange
            var args = "-t -i file.txt".Split(' ');

            //Act
            var options = Program.ParseOptions(args);

            //Assert
            Assert.IsFalse(options.UseInteractiveMode());
        }

        [TestMethod]
        public void interactive_mode_ignored_if_input_file_and_direct_value_are_present()
        {
            //Arrange
            var args = "23 -t -i file.txt".Split(' ');

            //Act
            var options = Program.ParseOptions(args);

            //Assert
            Assert.IsFalse(options.UseInteractiveMode());
        }
    }
}