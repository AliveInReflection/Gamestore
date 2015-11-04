using System.Collections.Generic;
using GameStore.Infrastructure.DTO;


namespace GameStore.Infrastructure.BLInterfaces
{
    public interface IGameService
    {
        void Create(GameDTO game);
        void Update(GameDTO game);
        void Delete(int gameId);
        
        GameDTO Get(string gameKey);
        IEnumerable<GameDTO> Get(int genreId);
        IEnumerable<GameDTO> Get(IEnumerable<int> platfotmTypeIds);
        PaginatedGames Get(GameFilteringMode filteringMode);
        IEnumerable<GameDTO> GetAll();
        int GetCount();
    }
}
