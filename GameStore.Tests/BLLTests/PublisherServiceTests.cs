using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Infrastructure;
using GameStore.BLL.Services;
using GameStore.DAL.Interfaces;
using GameStore.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Tests.BLLTests
{
    [TestClass]
    public class PublisherServiceTests
    {
        private List<Publisher> publishers;
        private Mock<IUnitOfWork> mock;
        private PublisherDTO publisherToAdd;
        private string existedCompanyName;



        private void InitializeCollections()
        {
            publishers = new List<Publisher>
            {
                new Publisher()
                {
                    PublisherId = 1,
                    CompanyName = "Blizzard",
                    Description = "The best company in the world",
                    HomePage = "battle.net"
                },
                new Publisher()
                {
                    PublisherId = 2,
                    CompanyName = "Electronic Arts",
                    Description = "Only fast",
                    HomePage = "www.needforspeed.com"
                }
            };
        }

        private void InitializeMocks()
        {
            mock = new Mock<IUnitOfWork>();


            mock.Setup(x => x.Publishers.GetAll()).Returns(publishers);
            mock.Setup(x => x.Publishers.GetSingle(It.IsAny<Expression<Func<Publisher, bool>>>())).Returns((Expression<Func<Publisher, bool>> predicate) => publishers.Where(predicate.Compile()).First());
            mock.Setup(x => x.Publishers.GetMany(It.IsAny<Expression<Func<Publisher, bool>>>())).Returns((Expression<Func<Publisher, bool>> predicate) => publishers.Where(predicate.Compile()));
            mock.Setup(x => x.Publishers.Create(It.IsAny<Publisher>())).Callback((Publisher publisher) => publishers.Add(publisher));
        }

        private void InitializeTestEntities()
        {
            publisherToAdd = new PublisherDTO()
                {
                    PublisherId = 3,
                    CompanyName = "Valve",
                    Description = "Conquire the world",
                    HomePage = "www.valve.com"
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

            var service = new PublisherService(mock.Object);

            service.Create(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Add_Publisher_With_Existed_Company_Name_Reference_Expected_Exception()
        {
            publisherToAdd.CompanyName = existedCompanyName;

            var service = new PublisherService(mock.Object);

            service.Create(publisherToAdd);
        }

        [TestMethod]
        public void Add_Publisher()
        {
            var service = new PublisherService(mock.Object);
            var expectedCount = publishers.Count + 1;

            service.Create(publisherToAdd);

            Assert.AreEqual(expectedCount, publishers.Count);
        }

        [TestMethod]
        public void Get_Publisher_By_Company_Name_Is_Not_Null()
        {
            var service = new PublisherService(mock.Object);

            var publisher = service.Get(existedCompanyName);

            Assert.IsNotNull(publisher);
        }

        [TestMethod]
        public void Get_All_Publishers_Is_Not_Null()
        {
            var service = new PublisherService(mock.Object);

            var publisherEntries = service.GetAll();

            Assert.IsNotNull(publisherEntries);
        }


    }
}
