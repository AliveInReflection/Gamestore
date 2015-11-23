using System.Collections.Generic;
using GameStore.Infrastructure.DTO;


namespace GameStore.Infrastructure.BLInterfaces
{
    public interface IGameService
    {
        /// <summary>Add new entity to database</summary>
        /// <param name="game">Entity, received from UI</param>
        /// <exception>ValidationException</exception>
        void Create(GameDTO game);

        /// <summary>Update entity in database</summary>
        /// <param name="game">Entity, received from UI</param>
        /// <exception>ValidationException</exception>
        void Update(GameDTO game);

        /// <summary>Delete entity from database</summary>
        /// <param name="gameId">Entity id received from UI</param>
        /// <exception>ValidationException</exception>
        void Delete(int gameId);


        /// <summary>Returns game from database with specified game key</summary>
        /// <param name="gameKey">Game key</param>
        /// <returns>Game</returns>
        /// <exception>ValidationException</exception>
        GameDTO Get(string gameKey);

        /// <summary>Returns game from database with specified game id</summary>
        /// <param name="gameId">Game id</param>
        /// <returns>Game</returns>
        /// <exception>ValidationException</exception>
        GameDTO Get(int gameId);

        /// <summary>Returns list of games from database which masked by genre with specified genre id</summary>
        /// <param name="genreId">Genre id</param>
        /// <returns>List of games</returns>
        /// <exception>ValidationException</exception>
        IEnumerable<GameDTO> GetByGenre(int genreId);

        /// <summary>Returns list of games from database which masked by platform types which ids listed in param</summary>
        /// <param name="platfotmTypeIds">List of platform type ids</param>
        /// <returns>List of games</returns>
        /// <exception>ValidationException</exception>
        IEnumerable<GameDTO> Get(IEnumerable<int> platfotmTypeIds);

        /// <summary>Returns conteiner with games, filtered and sorted for specified page</summary>
        /// <param name="filteringMode">Conteiner with parameters for filtering, sorting and paginating</param>
        /// <returns>Conteiner with games</returns>
        /// <exception>ValidationException</exception>
        PaginatedGames Get(GameFilteringMode filteringMode);

        /// <summary>Returns all games from database</summary>
        /// <returns>List of games</returns>
        IEnumerable<GameDTO> GetAll();

        /// <summary>Returns count of games in database</summary>
        /// <returns>Count of games</returns>
        int GetCount();
    }
}
