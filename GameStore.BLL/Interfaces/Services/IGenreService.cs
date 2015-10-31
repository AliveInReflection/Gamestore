using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.BLL.DTO;

namespace GameStore.BLL.Interfaces
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
