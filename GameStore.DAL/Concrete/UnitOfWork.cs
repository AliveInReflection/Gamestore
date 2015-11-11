using System;
using Gamestore.DAL.Context;
using GameStore.DAL.Concrete.Repositories;
using GameStore.DAL.GameStore.Concrete;
using GameStore.DAL.GameStore.Interfaces;
using GameStore.DAL.Interfaces;
using GameStore.DAL.Northwind;
using GameStore.DAL.Northwind.Concrete;
using GameStore.DAL.Northwind.Interfaces;
using GameStore.Domain.Entities;
using Order = GameStore.Domain.Entities.Order;

namespace GameStore.DAL.Concrete
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly GameStoreContext gameStoreContext;
        private readonly NorthwindContext northwindContext;

        private IGameStoreUnitOfWork gameStore;
        private INorthwindUnitOfWork northwind;

        private IRepository<Game> games;
        private IRepository<Comment> comments;
        private IRepository<Genre> genres;
        private IRepository<PlatformType> platformTypes;
        private IRepository<Publisher> publishers;
        private IRepository<OrderDetails> orderDetailses;
        private IRepository<Order> orders;
        private IRepository<User> users;


        public UnitOfWork(string gameStoreConnectionString)
        {
            gameStoreContext = new GameStoreContext(gameStoreConnectionString);
            northwindContext = new NorthwindContext();

            gameStore = new GameStoreUnitOfWork(gameStoreContext);
            northwind = new NorthwindUnitOfWork(northwindContext);
        }


        public IRepository<Game> Games
        {
            get { return games ?? (games = new GameRepository(gameStore, northwind)); }
        }

        public IRepository<Comment> Comments
        {
            get { return comments ?? (comments = new CommentRepository(gameStore, northwind)); }
        }

        public IRepository<Genre> Genres
        {
            get { return genres ?? (genres = new GenreRepository(gameStore, northwind)); }
        }

        public IRepository<PlatformType> PlatformTypes
        {
            get { return platformTypes ?? (platformTypes = new PlatformTypeRepository(gameStoreContext)); }
        }

        public IRepository<Publisher> Publishers
        {
            get { return publishers ?? (publishers = new PublisherRepository(gameStore, northwind)); }
        }

        public IRepository<OrderDetails> OrderDetailses
        {
            get { return orderDetailses ?? (orderDetailses = new OrderDetailsRepository(gameStore, northwind)); }
        }

        public IRepository<Order> Orders
        {
            get { return orders ?? (orders = new OrderRepository(gameStore, northwind)); }
        }

        public IRepository<User> Users
        {
            get { return users ?? (users = new UserRepository(gameStoreContext)); }
        }

        public void Save()
        {
            gameStoreContext.SaveChanges();
        }

        public void Dispose()
        {
            //the context itself checks if Dispose has already been called so that nothing happens on a second call and no exception will be thrown
            //the context class has its own finalizer that will ensure that the database connection is released on garbage collection if you didn"t call Dispose explicitly
            gameStoreContext.Dispose();
            northwindContext.Dispose();
        }
    }
}
