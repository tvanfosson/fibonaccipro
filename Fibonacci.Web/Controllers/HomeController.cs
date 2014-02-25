using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using Fibonacci.Lib.Calculators;
using Fibonacci.Web.Models;

namespace Fibonacci.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFibonacciCalculator _fibonacciCalculator;

        public HomeController(IFibonacciCalculator fibonacciCalculator)
        {
            _fibonacciCalculator = fibonacciCalculator;
        }

        //GET: "/"
        [HttpGet]
        public ActionResult Index()
        {
            return View(new IndexViewModel());
        }

        //POST: "/"
        //this logic will almost always go through JS now, but we'll just keep this here for no-JS IE6 compatibility ;)
        [HttpPost]
        [Authorize]
        public ActionResult Index(IndexViewModel inputViewModel)
        {
            var viewModel = new IndexViewModel { InputValue = inputViewModel.InputValue };

            if (inputViewModel.InputValue > 0)
            {
                viewModel.Results = _fibonacciCalculator.Calculate(inputViewModel.InputValue);
            }

            return View("Index", viewModel);
        }

        //GET: /<n> (optional n)
        [HttpGet]
        [Authorize]
        public ActionResult Get(int urlInputValue)
        {
            return Index(new IndexViewModel { InputValue = urlInputValue });
        }
    }
}