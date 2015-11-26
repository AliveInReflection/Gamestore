using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Domain.Entities;

namespace GameStore.DAL.GameStore.Interfaces
{
    public interface IGameStoreUnitOfWork
    {
        IGameStoreRepository<Game> Games { get;}
        IGameStoreRepository<Comment> Comments { get; }
        IGameStoreRepository<Genre> Genres { get; }
        IGameStoreRepository<PlatformType> PlatformTypes { get; }
        IGameStoreRepository<Publisher> Publishers { get; }
        IGameStoreRepository<Order> Orders { get; }
        IGameStoreRepository<OrderDetails> OrderDetailses { get; }
        IGameStoreRepository<User> Users { get; }
    }
}
