using System;
using System.Collections.Generic;
using System.Linq;

namespace Fibonacci.Web.App_Start
{
    public static class FilterConfig
    {
        public static void RegisterGlobalFilters()
        {
            //Removed this to allow custom 404 and 500 error pages
            //filters.Add(new HandleErrorAttribute());
        }
    }
}