using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using Castle.Windsor;
using Castle.Windsor.Installer;
using RiskApplication.Utility.AutoMapper;
using RiskApplication.Utility.Windsor;
using RiskApplication.Utility.Windsor.Extensions;

namespace RiskApplication
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static IWindsorContainer Container { get; set; }

        protected void Application_Start()
        {
            BootstrapContainer();

            AreaRegistration.RegisterAllAreas();

            AutoMaps.Configure(new List<Profile>
								   {
									   new WebMappingProfile() 
                                   });

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            
            // View Engines
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }



        private static void BootstrapContainer()
        {
            Container = new WindsorContainer();
            Container.Kernel.ComponentModelCreated += WindsorExtension.Kernel_ComponentModelCreated;

            Container.Install(FromAssembly.This());
            

            var controllerFactory = new WindsorControllerFactory(Container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Risk", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }
    }
}
