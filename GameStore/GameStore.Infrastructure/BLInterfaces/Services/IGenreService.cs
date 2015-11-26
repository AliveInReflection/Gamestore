using System.Collections.Generic;
using GameStore.Infrastructure.DTO;

namespace GameStore.Infrastructure.BLInterfaces
{
    public interface IGenreService
    {
        /// <summary>Add new entity to database</summary>
        /// <param name="genre">Entity, received from UI</param>
        /// <exception>ValidationException</exception>
        void Create(GenreDTO genre);

        /// <summary>Update entity in database</summary>
        /// <param name="genre">Entity, received from UI</param>
        /// <exception>ValidationException</exception>
        void Update(GenreDTO genre);

        /// <summary>Delete entity from database</summary>
        /// <param name="genreId">Entity id received from UI</param>
        /// <exception>ValidationException</exception>
        void Delete(int genreId);

        /// <summary>Returns all genres from database</summary>
        /// <returns>List of genres</returns>
        IEnumerable<GenreDTO> GetAll();

        /// <summary>Returns list of genres from database for game with specified game key</summary>
        /// <param name="gameKey">Game key</param>
        /// <returns>List of genres</returns>
        /// <exception>ValidationException</exception>
        IEnumerable<GenreDTO> Get(string gameKey);

        /// <summary>Returns genre from database with specified id</summary>
        /// <param name="genreId">Genre id</param>
        /// <returns>Genre</returns>
        /// <exception>ValidationException</exception>
        GenreDTO Get(int genreId);
    }
}
