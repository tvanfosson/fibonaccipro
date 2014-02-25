using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;

using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;

using Fibonacci.Lib.Calculators;

namespace Fibonacci.Web.App_Start
{
    public static class AutofacConfig
    {
        public static void RegisterDependencies()
        {
            //autofac 
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterApiControllers(typeof(MvcApplication).Assembly);

            //my types
            builder.RegisterType<ArrayCalculator>().As<IFibonacciCalculator>();

            //build container
            var container = builder.Build();

            // Set the dependency resolver for Web API.
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}