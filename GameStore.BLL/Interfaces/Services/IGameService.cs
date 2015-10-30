using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.BLL.DTO;

namespace GameStore.BLL.Interfaces
{
    public interface IGameService
    {
        void Create(GameDTO game, IEnumerable<int> genreIds, IEnumerable<int> platformTypeIds);
        void Update(GameDTO game, IEnumerable<int> genreIds, IEnumerable<int> platformTypeIds);
        void Delete(int gameId);
        
        GameDTO Get(string gameKey);
        IEnumerable<GameDTO> Get(int genreId);
        IEnumerable<GameDTO> Get(IEnumerable<int> platfotmTypeIds);
        PaginatedGames Get(GameFilteringMode filteringMode);
        IEnumerable<GameDTO> GetAll();
        int GetCount();
    }
}
