using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc3;
using DAL.Context;
using DAL.Interface;
using DAL.Implement;
using project.Controllers;
using System;
using DoAnCK.RS.Implement;
using DoAnCK.RS.Interface;


namespace DoAnCK
{
    public static class Bootstrapper
    {
        public static IUnityContainer container;
        public static void Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
           

        }
      

        public static UnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();            
            container
             .RegisterType<IDisposable, BakeryContext>(new ContainerControlledLifetimeManager()
         );
            //   container.Resolve<BakeryContext>();
            container.RegisterType<IBakeryReposibitory, BakeryReposibitory>();

            container.RegisterType<ICategoryResponsibility,CategoryResponsibility>();
            container.RegisterType<IBillReposibility, BillReposibility>();
            container.RegisterType<IBillDetailsReposibility, BillDetailsReposibility>();
            container.RegisterType<IBranchReposibitory, BranchResponsibility>();
            container.RegisterType<IIntroductionReposibitory, IntroductionResponsibility>();

            container.RegisterType<IBackup_RestoreRepository, Backup_RestoreRepository>();
            container.RegisterType<ILogRepository, LogRepository>();
            container.RegisterType<IImageRepository, Imagerepository>();
            container.RegisterType<IRateResposibitory, RateResposibitory>();


            container.RegisterType<INewsResponsibility,NewsResponsibility>();
            container.RegisterType<IRateResposibitory, RateResposibitory>();

            container.RegisterType<IRateDampMeanAl, RatingDampMeanAl>();

            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<IMatrixParse,MatrixParse >();
            container.RegisterType<IAppraiseAlgorthim, AppraiseAlgorthim>();
            container.RegisterType<IExcelrepository, ExcelRepository>();
            container.RegisterType<IPredictAL,PredictAL>();

            container.RegisterType<IReconmended_based_data, Reconmended_based_data>();

            var context = container.Resolve<BakeryContext>();
            new DAL.Startup(context).initialisedatabase();

            return container;
        }
    }
}