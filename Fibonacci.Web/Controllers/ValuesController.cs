using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Fibonacci.Lib.Calculators;
using Fibonacci.Web.Models;

namespace Fibonacci.Web.Controllers
{
    public class ValuesController : ApiController
    {
        private readonly IFibonacciCalculator _fibonacciCalculator;
        //constructor
        public ValuesController(IFibonacciCalculator fibonacciCalculator)
        {
            _fibonacciCalculator = fibonacciCalculator;
        }

        // GET api/5/<format>
        [Authorize]
        public HttpResponseMessage Get(int urlInputValue)
        {
            //get results array from calculator - converted to strings to avoid JS scientific notation in view results
            var resultsArray = _fibonacciCalculator.Calculate(urlInputValue).Select(x => x.ToString("R0"));

            //handle diff formats

            //send back
            return this.Request.CreateResponse(
                HttpStatusCode.OK,
                new FibApiResult { Success = true, Results = resultsArray });
        }
    }
}