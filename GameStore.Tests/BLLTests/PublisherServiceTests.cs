using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using GameStore.BLL.Infrastructure;
using GameStore.BLL.Services;
using GameStore.CL.AutomapperProfiles;
using GameStore.DAL.Interfaces;
using GameStore.Domain.Entities;
using GameStore.Infrastructure.DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GameStore.Tests.BLLTests
{
    [TestClass]
    public class PublisherServiceTests
    {
        private TestCollections collections;
        private UOWMock mock;
        private PublisherDTO publisherToAdd;
        private PublisherDTO publisherToUpdate;

        private PublisherService service;

        private void InitializeTestEntities()
        {
            service = new PublisherService(mock.UnitOfWork);

            publisherToAdd = new PublisherDTO()
                {
                    PublisherId = 3,
                    CompanyName = "Valve",
                    Description = "Conquire the world",
                    HomePage = "www.valve.com",
                    Games = new List<GameDTO>()
                };

            publisherToUpdate = new PublisherDTO()
                {
                    PublisherId = 2,
                    CompanyName = "Electronic Arts",
                    Description = "Only fast updated",
                    HomePage = "www.needforspeed.com 1",
                    Games = new List<GameDTO>()
                };
        }

        [TestInitialize]
        public void TestInitialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new AutomapperBLLProfile());
            });
            collections = new TestCollections();
            mock = new UOWMock(collections);
            InitializeTestEntities();
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Add_Publisher_With_Null_Reference_Expected_Exception()
        {
            service.Create(null);
        }


        [TestMethod]
        public void Add_Publisher()
        {
            var expectedCount = collections.Publishers.Count + 1;

            service.Create(publisherToAdd);

            Assert.AreEqual(expectedCount, collections.Publishers.Count);
        }

        [TestMethod]
        public void Get_Publisher_By_Company_Name_Is_Not_Null()
        {
            var publisher = service.Get(collections.Publishers[0].CompanyName);

            Assert.IsNotNull(publisher);
        }

        [TestMethod]
        public void Get_Publisher_By_Id_Is_Not_Null()
        {
            var publisher = service.Get(1);

            Assert.IsNotNull(publisher);
        }

        [TestMethod]
        public void Get_All_Publishers_Is_Not_Null()
        {
            var publisherEntries = service.GetAll();

            Assert.IsNotNull(publisherEntries);
        }

        [TestMethod]
        [ExpectedException(typeof (ValidationException))]
        public void Update_Publisher_With_Null_Refference_Expected_Exception()
        {
            service.Update(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Update_Publisher_With_Existed_Company_Name_Expected_Exception()
        {
            publisherToUpdate.PublisherId = 10;
            service.Update(publisherToUpdate);
        }

        [TestMethod]
        public void Update_Publisher()
        {
            service.Update(publisherToUpdate);

            var entry = collections.Publishers.First(m => m.PublisherId.Equals(publisherToUpdate.PublisherId));

            Assert.AreEqual(publisherToUpdate.CompanyName, entry.CompanyName);
            Assert.AreEqual(publisherToUpdate.Description, entry.Description);
            Assert.AreEqual(publisherToUpdate.HomePage, entry.HomePage);
        }

        [TestMethod]
        public void Remove_Publisher()
        {
            var expectedCount = collections.Publishers.Count - 1;
            
            service.Delete(1);

            Assert.AreEqual(expectedCount, collections.Publishers.Count);
        }


    }
}
