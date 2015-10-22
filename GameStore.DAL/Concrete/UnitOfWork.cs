using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gamestore.DAL.Context;
using GameStore.DAL.Interfaces;
using GameStore.Domain.Entities;

namespace GameStore.DAL.Concrete
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DataContext context;

        private IRepository<Game> games;
        private IRepository<Comment> comments;
        private IRepository<Genre> genres;
        private IRepository<PlatformType> platformTypes;
        private IRepository<Publisher> publishers;
        private IRepository<OrderDetails> orderDetailses;
        private IRepository<Order> orders; 


        public UnitOfWork(string connectionString)
        {
                context = new DataContext(connectionString);
        }


        public IRepository<Game> Games
        {
            get { return games ?? (games = new BaseRepository<Game>(context)); }
        }

        public IRepository<Comment> Comments
        {
            get { return comments ?? (comments = new BaseRepository<Comment>(context)); }
        }

        public IRepository<Genre> Genres
        {
            get { return genres ?? (genres = new BaseRepository<Genre>(context)); }
        }

        public IRepository<PlatformType> PlatformTypes
        {
            get { return platformTypes ?? (platformTypes = new BaseRepository<PlatformType>(context)); }
        }

        public IRepository<Publisher> Publishers
        {
            get { return publishers ?? (publishers = new BaseRepository<Publisher>(context)); }
        }

        public IRepository<OrderDetails> OrderDetailses
        {
            get { return orderDetailses ?? (orderDetailses = new BaseRepository<OrderDetails>(context)); }
        }

        public IRepository<Order> Orders
        {
            get { return orders ?? (orders = new BaseRepository<Order>(context)); }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            //the context itself checks if Dispose has already been called so that nothing happens on a second call and no exception will be thrown
            //the context class has its own finalizer that will ensure that the database connection is released on garbage collection if you didn't call Dispose explicitly
            context.Dispose();
        }
    }
}
