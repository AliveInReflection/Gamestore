using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GameStore.DAL.Concrete.RepositoryDecorators;
using GameStore.DAL.GameStore.Interfaces;
using GameStore.DAL.Infrastructure;
using GameStore.DAL.Northwind.Interfaces;
using GameStore.Domain.Entities;

namespace GameStore.DAL.Concrete.Repositories
{
    class GenreRepository : BaseRepository<Genre>
    {
        public GenreRepository(IGameStoreUnitOfWork gameStore, INorthwindUnitOfWork northwind)
            : base(gameStore, northwind)
        {
            
        }

        public override void Create(Genre entity)
        {
            gameStore.Genres.Create(entity);
        }

        public override void Update(Genre entity)
        {
            var database = KeyManager.GetDatabase(entity.GenreId);

            if (database == DatabaseType.Northwind &&
                gameStore.Genres.Get(m => m.GenreId.Equals(entity.GenreId)) == null)
            {
                Create(entity);
                return;
            }

            gameStore.Genres.Update(entity);
        }

        public override void Delete(int id)
        {
            var database = KeyManager.GetDatabase(id);

            if (database == DatabaseType.Northwind &&
                gameStore.Genres.Get(m => m.GenreId.Equals(id)) == null)
            {
                var genre = northwind.Genres.Get(KeyManager.Decode(id));
                genre.IsDeleted = true;
                Create(genre);
                return;
            }

            gameStore.Genres.Delete(id);
        }

        public override Genre Get(Expression<Func<Genre, bool>> predicate)
        {
            var genre = gameStore.Genres.GetMany(predicate).FirstOrDefault(m => !m.IsDeleted);
            if (genre == null)
            {
                genre = northwind.Genres.GetAll(GetGenreIdsToExclude()).Where(predicate.Compile()).First();
            }
            
            return genre;
        }

        public override IEnumerable<Genre> GetAll()
        {
            var genres = gameStore.Genres.GetMany(m => !m.IsDeleted).ToList();

            genres.AddRange(northwind.Genres.GetAll(GetGenreIdsToExclude()));

            return genres;
        }

        public override IEnumerable<Genre> GetMany(Expression<Func<Genre, bool>> predicate)
        {
            var genres = gameStore.Genres.GetMany(predicate).Where(m => !m.IsDeleted).ToList();

            genres.AddRange(northwind.Genres.GetAll(GetGenreIdsToExclude()).Where(predicate.Compile()));

            return genres;
        }

        public override int Count()
        {
            var genreIdsToExclude = GetGenreIdsToExclude();

            var gameStoreCount = gameStore.Genres.GetMany(m => !m.IsDeleted).Count();
            var northwindCount = northwind.Genres.Count(genreIdsToExclude);

            return gameStoreCount + northwindCount;
        }

        public override bool IsExists(Expression<Func<Genre, bool>> predicate)
        {
            var gameStoreIsExists = gameStore.Genres.GetMany(m => !m.IsDeleted).Any(predicate);

            return gameStoreIsExists ? gameStoreIsExists : northwind.Genres.GetAll(GetGenreIdsToExclude()).Any(predicate.Compile());
        }

        private IEnumerable<int> GetGenreIdsToExclude()
        {
            return gameStore.Genres.GetAll().Select(m => m.GenreId).Where(m => KeyManager.GetDatabase(m) == DatabaseType.Northwind).Select(m => KeyManager.Decode(m));
        }
    }
}
