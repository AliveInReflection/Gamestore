using System;
using System.Collections.Generic;
using System.Linq;
using GameStore.DAL.Interfaces;
using AutoMapper;
using GameStore.BLL.Infrastructure;
using GameStore.Domain.Entities;
using GameStore.Infrastructure.BLInterfaces;
using GameStore.Infrastructure.DTO;

namespace GameStore.BLL.Services
{
    public class GenreService : IGenreService
    {
        private readonly IUnitOfWork database;

        public GenreService()
        {

        }

        public GenreService(IUnitOfWork database)
        {
            this.database = database;
        }

        public IEnumerable<GenreDTO> GetAll()
        {
            return Mapper.Map<IEnumerable<Genre>, IEnumerable<GenreDTO>>(database.Genres.GetAll());
        }

        public IEnumerable<GenreDTO> Get(string gameKey)
        {
            var genres = database.Genres.GetMany(m => m.Games.Any(g => g.GameKey.Equals(gameKey)));
            return Mapper.Map<IEnumerable<Genre>, IEnumerable<GenreDTO>>(genres);
        }

        public GenreDTO Get(int genreId)
        {
            var entry = database.Genres.Get(m => m.GenreId.Equals(genreId));
            return Mapper.Map<Genre, GenreDTO>(entry);
        }

        public void Create(GenreDTO genre)
        {
            if (genre == null)
            {
                throw new NullReferenceException("No content received");
            }

            if (database.Genres.IsExists(m => m.GenreName.Equals(genre.GenreName)))
            {
                throw new ValidationException(String.Format("Another genre with the same name ({0}) exists",genre.GenreName));
            }

            var genreToSave = Mapper.Map<GenreDTO, Genre>(genre);
            database.Genres.Create(genreToSave);
            database.Save();

        }

        public void Update(GenreDTO genre)
        {
            if (genre == null)
            {
                throw new NullReferenceException("No content received");
            }

            try
            {
                var genreToUpdate = Mapper.Map<GenreDTO, Genre>(genre);
                database.Genres.Update(genreToUpdate);
                database.Save();
            }
            catch (InvalidOperationException)
            {
                throw new ValidationException(String.Format("Another genre with the same name ({0}) exists", genre.GenreName));
            }
        }

        public void Delete(int genreId)
        {
            database.Genres.Delete(genreId);
            database.Save();
        }
    }
}
