using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.BLL.DTO;
using GameStore.BLL.Interfaces;
using AutoMapper;
using GameStore.DAL.Interfaces;
using GameStore.Domain.Entities;

namespace GameStore.BLL.Services
{
    public class PublisherService : IPublisherService
    {
        private IUnitOfWork database;

        public PublisherService()
        {
            
        }

        public PublisherService(IUnitOfWork database)
        {
            this.database = database;
        }

        public IEnumerable<PublisherDTO> GetAll()
        {
            var publishers = database.Publishers.GetAll();
            return Mapper.Map<IEnumerable<Publisher>, IEnumerable<PublisherDTO>>(publishers);
        }
    }
}
