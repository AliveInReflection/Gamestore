using System;
using AutoMapper;
using GameStore.BLL.Services;
using GameStore.CL.AutomapperProfiles;
using GameStore.Infrastructure.DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameStore.Tests.BLLTests
{
    [TestClass]
    public class GenreServiceTests
    {
        private TestCollections collections;
        
        private UOWMock mock;
       

        private int existedGenreId = 1;

        private GenreDTO genreToAdd;
        private GenreDTO genreToUpdate;

        private GenreService service;

        
        private void InitializeTestEntities()
        {

            genreToAdd = new GenreDTO()
            {
                GenreId = 3,
                GenreName = "TestName"
            };

            genreToUpdate = new GenreDTO()
            {
                GenreId = 1,
                GenreName = "TestName"
            };

            service = new GenreService(mock.UnitOfWork);
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
        public void Get_Genres_By_Game_Key_Is_Not_Null()
        {
            var result = service.Get(collections.Games[0].GameKey);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Get_All_Genres_Is_Not_Null()
        {
            var result = service.GetAll();

            Assert.IsNotNull(result);
        }


        [TestMethod]
        [ExpectedException(typeof (NullReferenceException))]
        public void Add_Genre_With_Null_Refference_Expected_Exception()
        {
            service.Create(null);
        }


        [TestMethod]
        public void Add_Genre()
        {
            var expectedCount = collections.Genres.Count + 1;

            service.Create(genreToAdd);

            Assert.AreEqual(expectedCount, collections.Genres.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Update_Genre_With_Null_Refference_Expected_Exception()
        {
            service.Update(null);
        }


        [TestMethod]
        public void Update_Genre()
        {
            service.Update(genreToUpdate);

            Assert.AreEqual(genreToUpdate.GenreName, collections.Genres[0].GenreName);
        }

        [TestMethod]
        public void Delete_Genre()
        {
            var expectedCount = collections.Genres.Count - 1;

            service.Delete(1);

            Assert.AreEqual(expectedCount, collections.Genres.Count);
        }

        [TestMethod]
        public void Get_Genre_By_Id_Result_Is_Not_Null()
        {
            var result = service.Get(existedGenreId);

            Assert.IsNotNull(result);
        }


    }
}
