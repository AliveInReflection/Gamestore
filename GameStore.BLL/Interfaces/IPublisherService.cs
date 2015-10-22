using GameStore.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.BLL.Interfaces
{
    public interface IPublisherService
    {
        IEnumerable<PublisherDTO> GetAll();
        PublisherDTO Get(string companyName);
        void Create(PublisherDTO publisher);
    }
}
