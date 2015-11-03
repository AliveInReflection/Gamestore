using GameStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.DAL.Interfaces;
using Gamestore.DAL.Context;

namespace GameStore.DAL.Concrete.Repositories
{
    public class GameRepository : IRepository<Game>
    {
        private GameStoreContext context;

        public GameRepository(GameStoreContext context)
        {
            this.context = context;
        }

        public void Create(Game entity)
        {
            context.Entry(entity).State = EntityState.Added;
        }

        public void Update(Game entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var entry = context.Games.First(m => m.GameId.Equals(id));
            entry.IsDeleted = true;
        }

        public Game Get(System.Linq.Expressions.Expression<Func<Game, bool>> predicate)
        {
            return context.Games.Where(predicate).First(m => !m.IsDeleted);
        }

        public IEnumerable<Game> GetAll()
        {
            return context.Games.Where(m => !m.IsDeleted).ToList();
        }

        public IEnumerable<Game> GetMany(System.Linq.Expressions.Expression<Func<Game, bool>> predicate)
        {
            return context.Games.Where(predicate).Where(m => !m.IsDeleted).ToList();
        }

        public int Count()
        {
            return context.Games.Count(m => !m.IsDeleted);
        }

        public bool IsExists(System.Linq.Expressions.Expression<Func<Game, bool>> predicate)
        {
            return context.Games.Where(predicate).Any(m => !m.IsDeleted);
        }
    }
}
