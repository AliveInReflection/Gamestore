using GameStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gamestore.DAL.Context;
using GameStore.DAL.Infrastructure;
using GameStore.DAL.Interfaces;
using GameStore.DAL.Northwind.Interfaces;

namespace GameStore.DAL.Concrete.Repositories
{
    class GenreRepository : IRepository<Genre>
    {
        private GameStoreContext context;
        private INorthwindUnitOfWork northwind;

        public GenreRepository(GameStoreContext context, INorthwindUnitOfWork northwind)
        {
            this.context = context;
            this.northwind = northwind;
        }

        public void Create(Genre entity)
        {
            context.Entry(entity).State = EntityState.Added;
        }

        public void Update(Genre entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var entry = context.Genres.First(m => m.GenreId.Equals(id));
            entry.IsDeleted = true;
        }

        public Genre Get(System.Linq.Expressions.Expression<Func<Genre, bool>> predicate)
        {
            return context.Genres.Where(predicate).First(m => !m.IsDeleted);
        }

        public IEnumerable<Genre> GetAll()
        {
            var genresToExclude = context.Genres.Where(m => KeyManager.GetDatabase(m.GenreId) == DatabaseType.Northwind).Select(m => m.GenreId);
            var genres = context.Genres.Where(m => !m.IsDeleted).ToList();

            genres.AddRange(northwind.Genres.GetAll(genresToExclude));

            return genres;
        }

        public IEnumerable<Genre> GetMany(System.Linq.Expressions.Expression<Func<Genre, bool>> predicate)
        {
            return context.Genres.Where(predicate).Where(m => !m.IsDeleted).ToList();
        }

        public int Count()
        {
            return context.Genres.Count(m => !m.IsDeleted);
        }

        public bool IsExists(System.Linq.Expressions.Expression<Func<Genre, bool>> predicate)
        {
            return context.Genres.Where(predicate).Any(m => !m.IsDeleted);
        }
    }
}
