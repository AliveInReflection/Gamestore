﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.BLL.DTO;
using GameStore.BLL.Interfaces;
using AutoMapper;
using GameStore.BLL.Infrastructure;
using GameStore.DAL.Interfaces;
using GameStore.Domain.Entities;

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

            if(database.Publishers.IsExists(m => m.CompanyName.Equals(publisher.CompanyName)))
            {
                throw new ValidationException("Another company exists with the same name");
            }

            var publisherToSave = Mapper.Map<PublisherDTO, Publisher>(publisher);
            database.Publishers.Create(publisherToSave);
            database.Save();
        }


        public void Update(PublisherDTO publisher)
        {
            if (publisher == null)
            {
                throw new ValidationException("No content received");
            }

            var entry = database.Publishers.Get(m => m.PublisherId.Equals(publisher.PublisherId));
            var publisherToSave = Mapper.Map(publisher, entry);

            database.Publishers.Update(publisherToSave);
            database.Save();
        }

        public void Delete(int publisherId)
        {
            if (!database.Publishers.IsExists(m => m.PublisherId.Equals(publisherId)))
            {
                throw new ValidationException("Publisher not found");
            }
            database.Publishers.Delete(publisherId);
            database.Save();
        }
    }
}
