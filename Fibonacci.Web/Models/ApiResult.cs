using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Fibonacci.Web.Models
{
    [DataContract]
    public class FibApiResult
    {
        [DataMember(Name = "success")]
        public bool Success { get; set; }

        [DataMember(Name = "results")]
        public IEnumerable<string> Results { get; set; }
    }
}