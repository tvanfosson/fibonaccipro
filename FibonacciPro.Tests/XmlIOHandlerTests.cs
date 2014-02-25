using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Xml.Linq;

using Fibonacci.Lib.IO;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FibonacciPro.Tests
{
    [TestClass]
    public class XmlIOHandlerTests
    {
        [TestMethod]
        public void handler_can_accept_xml_input()
        {
            //Arrange
            var handler = new XmlIOHandler("input.xml");
            var expectedResult = 22;

            //Act
            var result = handler.GetNumber();

            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void handler_throws_argument_exception_when_given_invalid_path()
        {
            //Arrange
            var handler = new XmlIOHandler("does-not-exist.xml");

            //Act
            try
            {
                var result = handler.GetNumber();
                Assert.Fail("did not throw ArgumentException with path paramater");
            }

                //Assert
            catch (ArgumentException ex)
            {
                Assert.AreEqual("path", ex.ParamName);
            }
        }

        [TestMethod]
        public void handler_throws_application_exception_when_given_invalid_markup()
        {
            //Arrange
            var path = "invalid-markup-input.xml";
            var handler = new XmlIOHandler(path);
            if (!File.Exists(path))
            {
                Assert.Inconclusive("invalid-markup-input.xml file was not present to test with");
            }
            //Act
            try
            {
                var result = handler.GetNumber();
                Assert.Fail("did not throw ArgumentException with path paramater");
            }

                //Assert
            catch (ApplicationException)
            {
                Assert.IsTrue(true); //Pass!
            }
        }

        [TestMethod]
        public void handler_throws_application_exception_when_given_invalid_schema()
        {
            //Arrange
            var path = "invalid-schema-elements-input.xml";
            var handler = new XmlIOHandler(path);
            if (!File.Exists(path))
            {
                Assert.Inconclusive("invalid-schema-elements-input.xml file was not present to test with");
            }
            //Act
            try
            {
                var result = handler.GetNumber();
                Assert.Fail("did not throw ArgumentException with path paramater");
            }

                //Assert
            catch (ArgumentException ex)
            {
                Assert.AreEqual("path", ex.ParamName);
            }
        }

        [TestMethod]
        public void handler_writes_xml_output()
        {
            //Arrange
            var path = "handler-writes-xml-output.xml";
            var expectedResults = new BigInteger[] { 2, 3, 5 };
            var handler = new XmlIOHandler(path);

            //Act
            handler.Write(expectedResults);

            //Assert
            var doc = XDocument.Load(path);
            var root = doc.Element("fiboutput");

            Assert.IsNotNull(root);
            Assert.IsTrue(root.Elements("result").Any());

            for (var i = 0; i < expectedResults.Length; i++)
            {
                Assert.IsNotNull(root.Elements("result").ElementAtOrDefault(i));
                Assert.AreEqual(expectedResults[i].ToString("R0"), root.Elements("result").ElementAt(i).Value);
            }
        }

        [TestMethod]
        public void handler_overwrites_existing_files_with_xml_output()
        {
            //Arrange
            var path = "sample-output.xml";
            var firstResultSet = new BigInteger[] { 2, 3, 5 };
            var firstHandler = new XmlIOHandler(path);
            firstHandler.Write(firstResultSet);
            if (!File.Exists(path))
            {
                Assert.Inconclusive("Unable to create test file to overwrite");
            }

            var updatedResultSet = new BigInteger[] { 1, 2, 3 };
            var newHandler = new XmlIOHandler(path);

            //Act
            newHandler.Write(updatedResultSet);

            //Assert
            var doc = XDocument.Load(path);
            var root = doc.Element("fiboutput");

            Assert.IsNotNull(root);
            Assert.IsTrue(root.Elements("result").Any());

            for (var i = 0; i < updatedResultSet.Length; i++)
            {
                Assert.IsNotNull(root.Elements("result").ElementAtOrDefault(i));
                Assert.AreEqual(updatedResultSet[i].ToString("R0"), root.Elements("result").ElementAt(i).Value);
            }
        }
    }
}