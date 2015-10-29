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
    }
}
