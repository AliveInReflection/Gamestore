using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GameStore.DAL.Concrete.RepositoryDecorators;
using GameStore.DAL.GameStore.Interfaces;
using GameStore.DAL.Interfaces;
using GameStore.DAL.Northwind.Interfaces;
using GameStore.Domain.Entities;

namespace GameStore.DAL.Concrete.Repositories
{
    public class CommentRepository : BaseRepository<Comment>
    {
        private IRepository<Game> gameRepository;

        public CommentRepository(IGameStoreUnitOfWork gameStore, INorthwindUnitOfWork northwind, IRepository<Game> gameRepository)
            : base(gameStore, northwind)
        {
            this.gameRepository = gameRepository;
        }

        public override void Create(Comment entity)
        {
            var user = gameStore.Users.Get(m => m.UserName.Equals(entity.User.UserName));
            if (user != null)
            {
                entity.User = user;
            }
            else
            {
                entity.User.Password = entity.User.UserName;
            }

            var game = gameStore.Games.Get(m => m.GameKey.Equals(entity.Game.GameKey));
            if (game == null)
            {
                game = northwind.Games.GetAll(new int[] { }).First(m => m.GameKey.Equals(entity.Game.GameKey));             
                gameRepository.Create(game);
            }
            entity.Game = game;

            var parentComment = gameStore.Comments.Get(m => m.CommentId.Equals(entity.ParentComment.CommentId));
            entity.ParentComment = parentComment;

            gameStore.Comments.Create(entity);
            
        }

        public override void Update(Comment entity)
        {
            gameStore.Comments.Update(entity);
        }

        public override void Delete(int id)
        {
            gameStore.Comments.Delete(id);
        }

        public override Comment Get(Expression<Func<Comment, bool>> predicate)
        {
            return gameStore.Comments.GetMany(predicate).First(m => !m.IsDeleted);
        }

        public override IEnumerable<Comment> GetAll()
        {
            return gameStore.Comments.GetMany(m => !m.IsDeleted).ToList();
        }

        public override IEnumerable<Comment> GetMany(Expression<Func<Comment, bool>> predicate)
        {
            return gameStore.Comments.GetMany(predicate).Where(m => !m.IsDeleted).ToList();
        }

        public override int Count()
        {
            return gameStore.Comments.GetMany(m => !m.IsDeleted).Count();
        }

        public override bool IsExists(Expression<Func<Comment, bool>> predicate)
        {
            return gameStore.Comments.GetMany(predicate).Any(m => !m.IsDeleted);
        }
    }
}
