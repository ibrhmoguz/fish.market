using FishMarket.Repository.Interface;
using FishMarket.Repository.Repository;
using FishMarket.Web.Infrastructure.Abstract;
using Ninject;
using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;

namespace FishMarket.Web.Infrastructure.Concrete
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public IDependencyScope BeginScope()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<IAuthProvider>().To<FormsAuthProvider>();
            kernel.Bind<IUser>().To<UserRepository>();
            kernel.Bind<IFish>().To<FishRepository>();
        }
    }
}