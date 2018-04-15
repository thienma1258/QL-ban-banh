using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc3;
using DAL.Context;
using DAL.Interface;
using DAL.Implement;
using project.Controllers;

namespace DoAnCK
{
    public static class Bootstrapper
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();            
            container
             .RegisterType<BakeryContext>(
             new ContainerControlledLifetimeManager());
            //   container.Resolve<BakeryContext>();
            container.RegisterType<IBakeryReposibitory, BakeryReposibitory>();
<<<<<<< HEAD
            container.RegisterType<ICategoryResponsibility,CategoryResponsibility>();
=======
            container.RegisterType<IBillReposibility, BillReposibility>();
            container.RegisterType<IBillDetailsReposibility, BillDetailsReposibility>();

>>>>>>> e4be12b6a5710fe48d1b526b3ec33517586839d0

            return container;
        }
    }
}