using System.Collections.Generic;
using GameStore.Infrastructure.DTO;


namespace GameStore.Infrastructure.BLInterfaces
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
