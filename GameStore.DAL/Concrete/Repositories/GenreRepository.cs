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
            if (entity.GenreId == 0)
            {
                entity.GenreId = GetId();
            }
            context.Genres.Add(entity);
        }

        public void Update(Genre entity)
        {
            var database = KeyManager.GetDatabase(entity.GenreId);

            if (database == DatabaseType.Northwind)
            {
                Create(entity);
                return;
            }

            context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var database = KeyManager.GetDatabase(id);

            if (database == DatabaseType.Northwind)
            {
                var genre = northwind.Genres.Get(KeyManager.Decode(id));
                genre.IsDeleted = true;
                Create(genre);
                return;
            }

            var entry = context.Genres.First(m => m.GenreId.Equals(id));
            entry.IsDeleted = true;
        }

        public Genre Get(System.Linq.Expressions.Expression<Func<Genre, bool>> predicate)
        {
            var genreIdsToExclude = GetGenreIdsToExclude();
            
            var genre = context.Genres.Where(predicate).FirstOrDefault(m => !m.IsDeleted);
            if (genre == null)
            {
                genre = northwind.Genres.GetAll(genreIdsToExclude).Where(predicate.Compile()).First();
            }
            
            return genre;
        }

        public IEnumerable<Genre> GetAll()
        {
            var genreIdsToExclude = GetGenreIdsToExclude();
            var genres = context.Genres.Where(m => !m.IsDeleted).ToList();

            genres.AddRange(northwind.Genres.GetAll(genreIdsToExclude));

            return genres;
        }

        public IEnumerable<Genre> GetMany(System.Linq.Expressions.Expression<Func<Genre, bool>> predicate)
        {
            var genreIdsToExclude = GetGenreIdsToExclude();
            var genres = context.Genres.Where(predicate).Where(m => !m.IsDeleted).ToList();

            genres.AddRange(northwind.Genres.GetAll(genreIdsToExclude).Where(predicate.Compile()));

            return genres;
        }

        public int Count()
        {
            var genreIdsToExclude = GetGenreIdsToExclude();

            var gameStoreCount = context.Genres.Count(m => !m.IsDeleted);
            var northwindCount = northwind.Genres.GetAll(genreIdsToExclude).Count();

            return gameStoreCount + northwindCount;
        }

        public bool IsExists(System.Linq.Expressions.Expression<Func<Genre, bool>> predicate)
        {
            var genreIdsToExclude = GetGenreIdsToExclude();

            var gameStoreIsExists = context.Genres.Where(m => !m.IsDeleted).Any(predicate);
            var northwindIsExists = northwind.Genres.GetAll(genreIdsToExclude).Any(predicate.Compile());

            return gameStoreIsExists || northwindIsExists;
        }

        private int GetId()
        {
            return context.Genres.Select(m => m.GenreId).OrderBy(m => m).ToList().Last() + KeyManager.Coefficient;
        }

        private IEnumerable<int> GetGenreIdsToExclude()
        {
            return context.Genres.ToList().Where(m => KeyManager.GetDatabase(m.GenreId) == DatabaseType.Northwind).Select(m => KeyManager.Decode(m.GenreId));
        }
    }
}
