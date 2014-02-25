using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;

using DataAnnotationsExtensions;

namespace Fibonacci.Web.Models
{
    public class IndexViewModel
    {
        [Required]
        [Integer(ErrorMessage = "Input must be a positive integer.")]
        [Min(1, ErrorMessage = "Input must be a positive integer.")]
        public int InputValue { get; set; }

        public IEnumerable<BigInteger> Results { get; set; }
    }
}