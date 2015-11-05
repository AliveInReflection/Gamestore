using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.DAL.Northwind.Interfaces;

namespace GameStore.DAL.Northwind.Concrete
{
    class NorthwindUnitOfWork : INorthwindUnitOfWork
    {
        private NorthwindContext context;
        private INorthwindRepository<Domain.Entities.Game> gameRepository;
        private INorthwindRepository<Domain.Entities.Genre> genreRepository;
        private INorthwindRepository<Domain.Entities.Publisher> publisherRepository;
        private INorthwindRepository<Domain.Entities.Order> orderRepository;

        public NorthwindUnitOfWork(NorthwindContext context)
        {
            this.context = context;
        }


        public INorthwindRepository<Domain.Entities.Game> Games
        {
            get { return gameRepository ?? (gameRepository = new NorthwindGameRepository(context)); }
        }

        public INorthwindRepository<Domain.Entities.Genre> Genres
        {
            get { return genreRepository ?? (genreRepository = new NorthwindGenreRepository(context)); }
        }

        public INorthwindRepository<Domain.Entities.Publisher> Publishers
        {
            get { return publisherRepository ?? (publisherRepository = new NorthwindPublisherRepository(context)); }
        }

        public INorthwindRepository<Domain.Entities.Order> Orders
        {
            get { return orderRepository ?? (orderRepository = new NorthwindOrderRepository(context)); }
        }
    }
}
