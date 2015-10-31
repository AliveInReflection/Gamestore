using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.BLL.DTO;
using GameStore.BLL.Interfaces;
using GameStore.DAL.Interfaces;
using AutoMapper;
using GameStore.BLL.Infrastructure;
using GameStore.Domain.Entities;

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
            var game = database.Games.GetSingle(m => m.GameKey.Equals(gameKey));
            
            var genres = game.Genres;
            return Mapper.Map<IEnumerable<Genre>, IEnumerable<GenreDTO>>(genres);
        }

        public GenreDTO Get(int genreId)
        {
            var entry = database.Genres.GetSingle(m => m.GenreId.Equals(genreId));
            return Mapper.Map<Genre, GenreDTO>(entry);
        }

        public void Create(GenreDTO genre)
        {
            if (genre == null)
            {
                throw new ValidationException("No content received");
            }
            try
            {
                var entry = database.Genres.GetSingle(m => m.GenreName.Equals(genre.GenreName));
                throw new ValidationException("Another genre with the same name exists");
            }
            catch (InvalidOperationException)
            {
                var genreToSave = Mapper.Map<GenreDTO, Genre>(genre);
                database.Genres.Create(genreToSave);
                database.Save();
            }
        }

        public void Update(GenreDTO genre)
        {
            if (genre == null)
            {
                throw new ValidationException("No content received");
            }

            var entry = database.Genres.GetSingle(m => m.GenreName.Equals(genre.GenreName));
            if (entry.GenreId != genre.GenreId)
            {
                throw new ValidationException("Another genre with the same name exists");
            }

            var genreToSave = Mapper.Map(genre, entry);
            database.Genres.Update(genreToSave);
            database.Save();
        }

        public void Delete(int genreId)
        {
            var entry = database.Genres.GetSingle(m => m.GenreId.Equals(genreId));
            
            if(entry.Games.Any())
            {
                throw new ValidationException("There are some games that marked up by this genre in the store");
            }

            database.Genres.Delete(genreId);
            database.Save();
        }
    }
}
