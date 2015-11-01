using GameStore.BLL.Interfaces;
using GameStore.BLL.Services;
using GameStore.Logger.Concrete;
using GameStore.Logger.Interfaces;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.WebUI.DI
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
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
            kernel.Bind<IGameStoreLogger>().To<NLogAdapter>();
            kernel.Bind<IGameService>().To<GameService>();
            kernel.Bind<ICommentService>().To<CommentService>();
            kernel.Bind<IGenreService>().To<GenreService>();
            kernel.Bind<IPlatformTypeService>().To<PlatformTypeService>();
            kernel.Bind<IPublisherService>().To<PublisherService>();
            kernel.Bind<IOrderService>().To<OrderService>();
            kernel.Bind<IUserService>().To<UserService>();
        }
    }
}