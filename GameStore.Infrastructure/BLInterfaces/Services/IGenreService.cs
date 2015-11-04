using System.Collections.Generic;
using GameStore.Infrastructure.DTO;

namespace GameStore.Infrastructure.BLInterfaces
{
    public interface IGenreService
    {
        void Create(GenreDTO genre);
        void Update(GenreDTO genre);
        void Delete(int genreId);

        IEnumerable<GenreDTO> GetAll();
        IEnumerable<GenreDTO> Get(string gameKey);
        GenreDTO Get(int genreId);
    }
}
