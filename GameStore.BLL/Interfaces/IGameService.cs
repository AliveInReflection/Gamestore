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
        void AddGame(GameDTO game);
        void EditGame(GameDTO game);
        void DeleteGame(int id);
        GameDTO GetGameByKey(string key);
        IEnumerable<GameDTO> GetAllGames();

        void AddComment(CommentDTO comment);
        IEnumerable<CommentDTO> GetCommentsByGameKey(string key);

        IEnumerable<GameDTO> GetGamesByGenre(int genreId);
        IEnumerable<GameDTO> GetGamesByPlatformTypes(IEnumerable<int> typeIds);
    }
}
