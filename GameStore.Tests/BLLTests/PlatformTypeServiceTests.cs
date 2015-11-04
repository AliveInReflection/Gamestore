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
    public class PlatformTypeServiceTests
    {
        private List<PlatformType> platformTypes;
        private List<Game> games;
        private Mock<IUnitOfWork> mock;
        private string testGameKey;
        private string notExistedGameKey;

        private PlatformTypeDTO platformTypeToAdd;
        private PlatformTypeDTO platformTypeToUpdate;

        private PlatformTypeService service;



        private void InitializeCollections()
        {
            platformTypes = new List<PlatformType>
            {
                new PlatformType() { PlatformTypeId = 1, PlatformTypeName = "Desktop", Games = new List<Game>()},
                new PlatformType() { PlatformTypeId = 2, PlatformTypeName = "Console", Games = new List<Game>()}
            };

            games = new List<Game>
            {
                new Game()
                {
                    GameId = 1,
                    GameKey = "SCII",
                    GameName = "StarCraftII",
                    Description = "DescriptionSCII",
                    Genres = new List<Genre>(),
                    PlatformTypes = new List<PlatformType>{platformTypes[0]}
                },
                new Game()
                {
                    GameId = 2,
                    GameKey = "CSGO",
                    GameName = "Counter strike: global offencive",
                    Description = "DescriptionCSGO",
                    Genres = new List<Genre>(),
                    PlatformTypes = new List<PlatformType>{platformTypes[0]}
                }
            };
        }

        private void InitializeMocks()
        {
            mock = new Mock<IUnitOfWork>();


            mock.Setup(x => x.PlatformTypes.GetAll()).Returns(platformTypes);
            mock.Setup(x => x.PlatformTypes.Get(It.IsAny<Expression<Func<PlatformType, bool>>>())).Returns((Expression<Func<PlatformType, bool>> predicate) => platformTypes.Where(predicate.Compile()).First());
            mock.Setup(x => x.PlatformTypes.GetMany(It.IsAny<Expression<Func<PlatformType, bool>>>())).Returns((Expression<Func<PlatformType, bool>> predicate) => platformTypes.Where(predicate.Compile()));
            mock.Setup(x => x.PlatformTypes.Create(It.IsAny<PlatformType>())).Callback((PlatformType platformType) => platformTypes.Add(platformType));
            mock.Setup(x => x.PlatformTypes.Update(It.IsAny<PlatformType>())).Callback((PlatformType platformType) =>
            {
                var entry = platformTypes.First(m => m.PlatformTypeId.Equals(platformType.PlatformTypeId));
                entry.PlatformTypeName = platformType.PlatformTypeName;
            });
            mock.Setup(x => x.PlatformTypes.Delete(It.IsAny<int>()))
                .Callback((int id) => platformTypes.Remove(platformTypes.First(m => m.PlatformTypeId.Equals(id))));
            
            mock.Setup(x => x.Games.Get(It.IsAny<Expression<Func<Game, bool>>>())).Returns((Expression<Func<Game, bool>> predicate) => games.Where(predicate.Compile()).First());
        }

        private void InitializeTestEntities()
        {
            testGameKey = "SCII";
            notExistedGameKey = "Test";

            platformTypeToAdd = new PlatformTypeDTO()
            {
                PlatformTypeId = 3,
                PlatformTypeName = "TestName"
            };

            platformTypeToUpdate = new PlatformTypeDTO()
            {
                PlatformTypeId = 1,
                PlatformTypeName = "TestName"
            };

            service = new PlatformTypeService(mock.Object);
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
        public void Get_Platform_Types_By_Game_Key_Is_Not_Null()
        {
            var result = service.Get(testGameKey);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Get_All_Platform_Types_Is_Not_Null()
        {
            var result = service.GetAll();

            Assert.IsNotNull(result);
        }


        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Add_Platform_Type_With_Null_Refference_Expected_Exception()
        {
            service.Create(null);
        }

        [TestMethod]
        public void Add_Platform_Type()
        {
            var expectedCount = platformTypes.Count + 1;

            service.Create(platformTypeToAdd);

            Assert.AreEqual(expectedCount, platformTypes.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Update_Platform_Type_With_Null_Refference_Expected_Exception()
        {
            service.Update(null);
        }


        [TestMethod]
        public void Update_Platform_Type()
        {
            service.Update(platformTypeToUpdate);

            Assert.AreEqual(platformTypeToUpdate.PlatformTypeName, platformTypes[0].PlatformTypeName);
        }

        [TestMethod]
        public void Delete_Platform_Type()
        {
            var expectedCount = platformTypes.Count - 1;

            service.Delete(1);

            Assert.AreEqual(expectedCount, platformTypes.Count);
        }
    }




    
}
