using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GameStore.DAL.Concrete.RepositoryDecorators;
using GameStore.DAL.GameStore.Interfaces;
using GameStore.DAL.Infrastructure;
using GameStore.DAL.Interfaces;
using GameStore.DAL.Northwind.Interfaces;
using GameStore.Domain.Entities;

namespace GameStore.DAL.Concrete.Repositories
{
    public class OrderDetailsRepository : BaseRepository<OrderDetails>
    {
        private IRepository<Game> gameRepository;
        public OrderDetailsRepository(IGameStoreUnitOfWork gameStore, INorthwindUnitOfWork northwind, IRepository<Game> gameRepository)
            : base(gameStore, northwind)
        {
            this.gameRepository = gameRepository;
        }

        public override void Create(OrderDetails entity)
        {
            var game = gameStore.Games.Get(m => m.GameId.Equals(entity.Product.GameId));
            if (game == null)
            {
                game = northwind.Games.Get(KeyManager.Decode(entity.Product.GameId));
                gameRepository.Update(game);
                entity.Product = game;
            } 
            gameStore.OrderDetailses.Create(entity);
        }

        public override void Update(OrderDetails entity)
        {
            gameStore.OrderDetailses.Update(entity);
        }

        public override void Delete(int id)
        {
            gameStore.OrderDetailses.Delete(id);
        }

        public override OrderDetails Get(Expression<Func<OrderDetails, bool>> predicate)
        {
            var orderDetails = gameStore.OrderDetailses.GetMany(predicate).First(m => !m.IsDeleted);
            return orderDetails;
        }

        public override IEnumerable<OrderDetails> GetAll()
        {
            var orderDetailses = gameStore.OrderDetailses.GetMany(m => !m.IsDeleted).ToList();
            return orderDetailses;
        }

        public override IEnumerable<OrderDetails> GetMany(Expression<Func<OrderDetails, bool>> predicate)
        {
            var orderDetailses = gameStore.OrderDetailses.GetMany(predicate).Where(m => !m.IsDeleted).ToList();
            return orderDetailses;
        }

        public override int Count()
        {
            return gameStore.OrderDetailses.GetMany(m => !m.IsDeleted).Count();
        }

        public override bool IsExists(Expression<Func<OrderDetails, bool>> predicate)
        {
            return gameStore.OrderDetailses.GetMany(predicate).Any(m => !m.IsDeleted);
        }

    }
}
