using System;
using Gamestore.DAL.Context;
using GameStore.DAL.GameStore.Interfaces;
using GameStore.Domain.Entities;

namespace GameStore.DAL.GameStore.Concrete
{
    public class GameStoreUnitOfWork : IGameStoreUnitOfWork
    {
        private GameStoreContext context;

        private IGameStoreRepository<Game> games;
        private IGameStoreRepository<Comment> comments;
        private IGameStoreRepository<Genre> genres;
        private IGameStoreRepository<PlatformType> platformTypes;
        private IGameStoreRepository<Publisher> publishers;
        private IGameStoreRepository<Order> orders;
        private IGameStoreRepository<OrderDetails> orderDetailses;
        private IGameStoreRepository<User> users;

        public GameStoreUnitOfWork(GameStoreContext context)
        {
            this.context = context;
        }

        public IGameStoreRepository<Game> Games
        {
            get { return games ?? (games = new GameStoreGameRepository(context)); }
        }

        public IGameStoreRepository<Comment> Comments
        {
            get { return comments ?? (comments = new GameStoreCommentRepository(context)); }
        }

        public IGameStoreRepository<Genre> Genres
        {
            get { return genres ?? (genres = new GameStoreGenreRepository(context)); }
        }

        public IGameStoreRepository<PlatformType> PlatformTypes
        {
            get { return platformTypes ?? (platformTypes = new GameStorePlatformTypeRepository(context)); }
        }

        public IGameStoreRepository<Publisher> Publishers
        {
            get { return publishers ?? (publishers = new GameStorePublisherRepository(context)); }
        }

        public IGameStoreRepository<Order> Orders
        {
            get { return orders ?? (orders = new GameStoreOrderRepository(context)); }
        }

        public IGameStoreRepository<OrderDetails> OrderDetailses
        {
            get { return orderDetailses ?? (orderDetailses = new GameStoreOrderDetailsRepository(context)); }
        }

        public IGameStoreRepository<User> Users
        {
            get { return users ?? (users = new GameStoreUserRepository(context)); }
        }
    }
}
