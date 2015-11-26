using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.BLL.ContentPaginators;
using GameStore.BLL.CreditCardService;
using GameStore.DAL.Concrete;
using GameStore.DAL.Interfaces;
using GameStore.Domain.Entities;
using GameStore.Infrastructure.BLInterfaces;
using Ninject.Modules;

namespace GameStore.CL.DI
{
    public class BLLModule : NinjectModule
    {
        private string connectionString;
        public BLLModule(string connection)
        {
            connectionString = connection;
        }
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>().WithConstructorArgument("GameStoreContext");
            Bind<IContentPaginator<Game>>().To<GamePaginator>();
        }
    }
}
