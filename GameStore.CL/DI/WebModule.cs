using GameStore.BLL.Services;
using GameStore.Infrastructure.AuthInterfaces;
using GameStore.Infrastructure.BLInterfaces;
using GameStore.Logger.Concrete;
using GameStore.Logger.Interfaces;
using Ninject.Modules;
using GameStore.Auth;

namespace GameStore.CL.DI
{
    public class WebModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IGameStoreLogger>().To<NLogAdapter>();
            Bind<IGameService>().To<GameService>();
            Bind<ICommentService>().To<CommentService>();
            Bind<IGenreService>().To<GenreService>();
            Bind<IPlatformTypeService>().To<PlatformTypeService>();
            Bind<IPublisherService>().To<PublisherService>();
            Bind<IOrderService>().To<OrderService>();
            Bind<IUserService>().To<UserService>();
            Bind<IAuthenticationService>().To<AuthenticationService>();
        }
    }
}
