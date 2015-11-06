using System;
using Gamestore.DAL.Context;
using GameStore.DAL.Concrete.Repositories;
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
        private readonly GameStoreContext context;
        private readonly INorthwindUnitOfWork northwind;

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
            context = new GameStoreContext(gameStoreConnectionString);
            northwind = new NorthwindUnitOfWork(new NorthwindContext());
        }


        public IRepository<Game> Games
        {
            get { return games ?? (games = new GameRepository(context, northwind)); }
        }

        public IRepository<Comment> Comments
        {
            get { return comments ?? (comments = new CommentRepository(context, northwind)); }
        }

        public IRepository<Genre> Genres
        {
            get { return genres ?? (genres = new GenreRepository(context, northwind)); }
        }

        public IRepository<PlatformType> PlatformTypes
        {
            get { return platformTypes ?? (platformTypes = new PlatformTypeRepository(context)); }
        }

        public IRepository<Publisher> Publishers
        {
            get { return publishers ?? (publishers = new PublisherRepository(context, northwind)); }
        }

        public IRepository<OrderDetails> OrderDetailses
        {
            get { return orderDetailses ?? (orderDetailses = new OrderDetailsRepository(context, northwind)); }
        }

        public IRepository<Order> Orders
        {
            get { return orders ?? (orders = new OrderRepository(context, northwind)); }
        }

        public IRepository<User> Users
        {
            get { return users ?? (users = new UserRepository(context)); }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            //the context itself checks if Dispose has already been called so that nothing happens on a second call and no exception will be thrown
            //the context class has its own finalizer that will ensure that the database connection is released on garbage collection if you didn"t call Dispose explicitly
            context.Dispose();
        }
    }
}
