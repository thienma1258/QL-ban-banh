using DAL.Context;
using DoAnCK;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Unity.Mvc3;

namespace project
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
          
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            UnityContainer container =   Bootstrapper.BuildUnityContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            var context = container.Resolve<BakeryContext>();
            new DAL.Startup(context).initialisedatabase();

        }
    }
}
