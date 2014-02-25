using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Web.Mvc;

using FakeItEasy;

using Fibonacci.Lib.Calculators;
using Fibonacci.Web.Controllers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using MvcContrib.TestHelper;

namespace Fibonacci.Web.Tests
{
    [TestClass]
    public class HomeTests
    {
        [TestInitialize]
        public void Setup()
        {
        }

        [TestMethod]
        public void Index_returns_index_view()
        {
            //Arrange
            var controller = new HomeController(null);

            //Act
            var result = controller.Index();

            //Assert
            result.AssertViewRendered();
        }

        [TestMethod]
        public void Index_invokes_fibonacci_application_when_passed_an_input()
        {
            //Arrange
            var calculator = A.Fake<IFibonacciCalculator>();
            var controller = new HomeController(calculator);

            //Act
            var result = controller.Index(new Models.IndexViewModel() { InputValue = 5 });

            //Assert
            A.CallTo(() => calculator.Calculate(5)).MustHaveHappened();
        }

        [TestMethod]
        public void Index_returns_view_with_data_when_passed_an_input()
        {
            //Arrange
            var calculator = A.Fake<IFibonacciCalculator>();
            var expectedResult = new BigInteger[] { 0, 1, 1, 2, 3 };
            A.CallTo(() => calculator.Calculate(5)).Returns(expectedResult);
            var controller = new HomeController(calculator);

            //Act
            var result = controller.Index(new Models.IndexViewModel() { InputValue = 5 });

            //Assert
            result.AssertViewRendered().WithViewData<Models.IndexViewModel>();
            var viewData = ((ViewResult)result).Model as Models.IndexViewModel;
            Assert.AreEqual(expectedResult, viewData.Results);
        }

        [TestMethod]
        public void Index_returns_no_data_when_passed_0_value()
        {
            //Arrange
            var calculator = A.Fake<IFibonacciCalculator>();
            var controller = new HomeController(calculator);

            //Act
            var result = controller.Index(new Models.IndexViewModel() { InputValue = 0 });

            //Assert
            result.AssertViewRendered().WithViewData<Models.IndexViewModel>();
            var viewData = ((ViewResult)result).Model as Models.IndexViewModel;
            Assert.IsNull(viewData.Results);
        }

        [TestMethod]
        public void Index_returns_no_data_when_passed_negative_value()
        {
            //Arrange
            var calculator = A.Fake<IFibonacciCalculator>();
            var controller = new HomeController(calculator);

            //Act
            var result = controller.Index(new Models.IndexViewModel() { InputValue = -5 });

            //Assert
            result.AssertViewRendered().WithViewData<Models.IndexViewModel>();
            var viewData = ((ViewResult)result).Model as Models.IndexViewModel;
            Assert.IsNull(viewData.Results);
        }
    }
}