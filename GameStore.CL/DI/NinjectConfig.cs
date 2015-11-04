using GameStore.BLL.ContentPaginators;
using GameStore.BLL.Services;
using GameStore.DAL.Concrete;
using GameStore.DAL.Interfaces;
using GameStore.Domain.Entities;
using GameStore.Infrastructure.BLInterfaces;
using GameStore.Logger.Concrete;
using GameStore.Logger.Interfaces;
using Ninject;

namespace GameStore.CL.DI
{
    public static class NinjectConfig
    {
        public static void BindBLL(IKernel kernel)
        {
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().WithConstructorArgument("GameStoreContext");
            kernel.Bind<IContentPaginator<Game>>().To<GamePaginator>();
        }

        public static void BindWeb(IKernel kernel)
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
