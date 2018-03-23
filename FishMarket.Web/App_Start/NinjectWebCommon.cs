[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(FishMarket.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(FishMarket.Web.App_Start.NinjectWebCommon), "Stop")]

namespace FishMarket.Web.App_Start
{
    using System;
    using System.Web;
    using System.Web.Http;
    using FishMarket.Repository.Interface;
    using FishMarket.Repository.Repository;
    using FishMarket.Web.Infrastructure.Abstract;
    using FishMarket.Web.Infrastructure.Concrete;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

            RegisterServices(kernel);

            // Install our Ninject-based IDependencyResolver into the Web API config
            GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);

            return kernel;
        }

        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IAuthProvider>().To<FormsAuthProvider>();
            kernel.Bind<IUser>().To<UserRepository>();
            kernel.Bind<IFish>().To<FishRepository>();
        }
    }
}
