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
        void Create(PublisherDTO publisher);
        void Update(PublisherDTO publisher);
        void Delete(int publisherId);

        IEnumerable<PublisherDTO> GetAll();
        PublisherDTO Get(string companyName);
        PublisherDTO Get(int publisherId);
    }
}
