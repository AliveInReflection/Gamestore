using System;
using System.Collections.Generic;
using AutoMapper;
using GameStore.BLL.Infrastructure;
using GameStore.DAL.Interfaces;
using GameStore.Domain.Entities;
using GameStore.Infrastructure.BLInterfaces;
using GameStore.Infrastructure.DTO;

namespace GameStore.BLL.Services
{
    public class PublisherService : IPublisherService
    {
        private IUnitOfWork database;

        public PublisherService(IUnitOfWork database)
        {
            this.database = database;
        }

        public IEnumerable<PublisherDTO> GetAll()
        {
            var publishers = database.Publishers.GetAll();
            return Mapper.Map<IEnumerable<Publisher>, IEnumerable<PublisherDTO>>(publishers);
        }

        public PublisherDTO Get(string companyName)
        {
            var publisher = database.Publishers.Get(m => m.CompanyName.Equals(companyName));
            return Mapper.Map<Publisher, PublisherDTO>(publisher);
        }

        public PublisherDTO Get(int publisherId)
        {
            var entry = database.Publishers.Get(m => m.PublisherId.Equals(publisherId));
            return Mapper.Map<Publisher, PublisherDTO>(entry);
        }

        public void Create(PublisherDTO publisher)
        {
            if (publisher == null)
            {
                throw new ValidationException("No content received");
            }

            try
            {
                var publisherToSave = Mapper.Map<PublisherDTO, Publisher>(publisher);
                database.Publishers.Create(publisherToSave);
                database.Save();
            }
            catch (InvalidOperationException)
            {
                throw new ValidationException(String.Format("Another publisher with the same company name ({0}) exists", publisher.CompanyName));
            }
        }


        public void Update(PublisherDTO publisher)
        {
            if (publisher == null)
            {
                throw new ValidationException("No content received");
            }

            try
            {
                var publisherToSave = Mapper.Map<PublisherDTO, Publisher>(publisher);
                database.Publishers.Create(publisherToSave);
                database.Save();
            }
            catch (InvalidOperationException)
            {
                throw new ValidationException(String.Format("Another publisher with the same company name ({0}) exists", publisher.CompanyName));
            }
        }

        public void Delete(int publisherId)
        {
            database.Publishers.Delete(publisherId);
            database.Save();
        }
    }
}
