using GameStore.DAL.Concrete;
using GameStore.DAL.Interfaces;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.BLL.Concrete.ContentPaginators;
using GameStore.BLL.Interfaces.ContentFilters;
using GameStore.Domain.Entities;
using GameStore.Logger.Concrete;
using GameStore.Logger.Interfaces;

namespace GameStore.BLL.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        private string connectionString;
        public ServiceModule(string connection)
        {
            connectionString = connection;
        }
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>().WithConstructorArgument(connectionString);
            Bind<IContentPaginator<Game>>().To<GamePaginator>();
            Bind<IGameStoreLogger>().To<NLogAdapter>();
        }
    }
}
