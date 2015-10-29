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
        IEnumerable<GenreDTO> GetAll();
        IEnumerable<GenreDTO> Get(string gameKey);
    }
}
