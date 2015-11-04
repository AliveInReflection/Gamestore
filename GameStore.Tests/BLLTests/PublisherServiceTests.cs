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
        private List<Publisher> publishers;
        private Mock<IUnitOfWork> mock;
        private PublisherDTO publisherToAdd;
        private PublisherDTO publisherToUpdate;
        private int publisherToDeleteId;
        private string existedCompanyName;

        private PublisherService service;



        private void InitializeCollections()
        {
            publishers = new List<Publisher>
            {
                new Publisher()
                {
                    PublisherId = 1,
                    CompanyName = "Blizzard",
                    Description = "The best company in the world",
                    HomePage = "battle.net",
                    Games = new List<Game>()
                },
                new Publisher()
                {
                    PublisherId = 2,
                    CompanyName = "Electronic Arts",
                    Description = "Only fast",
                    HomePage = "www.needforspeed.com",
                    Games = new List<Game>{new Game()}
                }
            };
        }

        private void InitializeMocks()
        {
            mock = new Mock<IUnitOfWork>();


            mock.Setup(x => x.Publishers.GetAll()).Returns(publishers);
            mock.Setup(x => x.Publishers.Get(It.IsAny<Expression<Func<Publisher, bool>>>())).Returns((Expression<Func<Publisher, bool>> predicate) => publishers.Where(predicate.Compile()).First());
            mock.Setup(x => x.Publishers.GetMany(It.IsAny<Expression<Func<Publisher, bool>>>())).Returns((Expression<Func<Publisher, bool>> predicate) => publishers.Where(predicate.Compile()));
            mock.Setup(x => x.Publishers.Create(It.IsAny<Publisher>())).Callback((Publisher publisher) => publishers.Add(publisher));
            mock.Setup(x => x.Publishers.Update(It.IsAny<Publisher>())).Callback((Publisher publisher) =>
            {
                var entry = publishers.First(m => m.PublisherId.Equals(publisher.PublisherId));
                entry.CompanyName = publisher.CompanyName;
                entry.HomePage = publisher.HomePage;
                entry.Description = entry.Description;
            });
            mock.Setup(x => x.Publishers.Delete(It.IsAny<int>()))
                .Callback((int id) => publishers.Remove(publishers.First(m => m.PublisherId.Equals(id))));
        }

        private void InitializeTestEntities()
        {
            service = new PublisherService(mock.Object);

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

            existedCompanyName = "Blizzard";
        }

        [TestInitialize]
        public void TestInitialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new AutomapperBLLProfile());
            });
            InitializeCollections();
            InitializeMocks();
            InitializeTestEntities();
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Add_Publisher_With_Null_Reference_Expected_Exception()
        {
            service.Create(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Add_Publisher_With_Existed_Company_Name_Reference_Expected_Exception()
        {
            publisherToAdd.CompanyName = existedCompanyName;

            service.Create(publisherToAdd);
        }

        [TestMethod]
        public void Add_Publisher()
        {
            var expectedCount = publishers.Count + 1;

            service.Create(publisherToAdd);

            Assert.AreEqual(expectedCount, publishers.Count);
        }

        [TestMethod]
        public void Get_Publisher_By_Company_Name_Is_Not_Null()
        {
            var publisher = service.Get(existedCompanyName);

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

            var entry = publishers.First(m => m.PublisherId.Equals(publisherToUpdate.PublisherId));

            Assert.AreEqual(publisherToUpdate.CompanyName, entry.CompanyName);
            Assert.AreEqual(publisherToUpdate.Description, entry.Description);
            Assert.AreEqual(publisherToUpdate.HomePage, entry.HomePage);
        }

        [TestMethod]
        public void Remove_Publisher()
        {
            var expectedCount = publishers.Count - 1;
            
            service.Delete(1);

            Assert.AreEqual(expectedCount, publishers.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Remove_Publisher_With_Published_Games_Expected_Exception()
        {
            service.Delete(2);
        }


    }
}
