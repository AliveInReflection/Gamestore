﻿using AutoMapper;
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
using GameStore.BLL.DTO;

namespace GameStore.Tests.BLLTests
{
    [TestClass]
    public class GenreServiceTests
    {
        private List<Genre> genres;
        private List<Game> games;
        private Mock<IUnitOfWork> mock;
        private string testGameKey;
        private string notExistedGameKey;

        private GenreDTO genreToAdd;
        private GenreDTO genreToUpdate;

        private GenreService service;



        private void InitializeCollections()
        {
            genres = new List<Genre>
            {
                new Genre() {GenreId = 1, GenreName = "RTS", Games = new List<Game>()},
                new Genre() {GenreId = 2, GenreName = "Action", Games = new List<Game>{new Game()}}
            };
            games = new List<Game>
            {
                new Game()
                {
                    GameId = 1,
                    GameKey = "SCII",
                    GameName = "StarCraftII",
                    Description = "DescriptionSCII",
                    Genres = new List<Genre> {genres[0]},
                    PlatformTypes = new List<PlatformType>()
                },
                new Game()
                {
                    GameId = 2,
                    GameKey = "CSGO",
                    GameName = "Counter strike: global offencive",
                    Description = "DescriptionCSGO",
                    Genres = new List<Genre> {genres[1]},
                    PlatformTypes = new List<PlatformType>()
                }
            };
        }

        private void InitializeMocks()
        {
            mock = new Mock<IUnitOfWork>();


            mock.Setup(x => x.Genres.GetAll()).Returns(genres);
            mock.Setup(x => x.Genres.Get(It.IsAny<Expression<Func<Genre, bool>>>())).Returns((Expression<Func<Genre, bool>> predicate) => genres.Where(predicate.Compile()).First());
            mock.Setup(x => x.Genres.GetMany(It.IsAny<Expression<Func<Genre, bool>>>())).Returns((Expression<Func<Genre, bool>> predicate) => genres.Where(predicate.Compile()));
            mock.Setup(x => x.Genres.Create(It.IsAny<Genre>())).Callback((Genre genre) => genres.Add(genre));
            mock.Setup(x => x.Genres.Update(It.IsAny<Genre>())).Callback((Genre genre) =>
            {
                var entry = genres.First(m => m.GenreId.Equals(genre.GenreId));
                entry.GenreName = genre.GenreName;
            });
            mock.Setup(x => x.Genres.Delete(It.IsAny<int>()))
                .Callback((int id) => genres.Remove(genres.First(m => m.GenreId.Equals(id))));

            mock.Setup(x => x.Games.Get(It.IsAny<Expression<Func<Game, bool>>>())).Returns((Expression<Func<Game, bool>> predicate) => games.Where(predicate.Compile()).First());
        }

        private void InitializeTestEntities()
        {
            testGameKey = "SCII";
            notExistedGameKey = "Test";

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

            service = new GenreService(mock.Object);
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
        [ExpectedException(typeof(InvalidOperationException))]
        public void Get_Genres_By_Game_Key_Expected_Exception()
        {
            service.Get(notExistedGameKey);
        }
        

        [TestMethod]
        public void Get_Genres_By_Game_Key_Is_Not_Null()
        {
            var result = service.Get(testGameKey);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Get_All_Genres_Is_Not_Null()
        {
            var result = service.GetAll();

            Assert.IsNotNull(result);
        }


        [TestMethod]
        [ExpectedException(typeof (ValidationException))]
        public void Add_Genre_With_Null_Refference_Expected_Exception()
        {
            service.Create(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Add_Genre_With_Existed_Name_Expected_Exception()
        {
            genreToAdd.GenreName = genres[0].GenreName;
            service.Create(genreToAdd);
        }

        [TestMethod]
        public void Add_Genre()
        {
            var expectedCount = genres.Count + 1;

            service.Create(genreToAdd);

            Assert.AreEqual(expectedCount, genres.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Update_Genre_With_Null_Refference_Expected_Exception()
        {
            service.Update(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Update_Genre_With_Existed_Name_Expected_Exception()
        {
            genreToUpdate.GenreName = genres[0].GenreName;
            genreToUpdate.GenreId = 2;
            service.Update(genreToUpdate);
        }

        [TestMethod]
        public void Update_Genre()
        {
            service.Update(genreToUpdate);

            Assert.AreEqual(genreToUpdate.GenreName, genres[0].GenreName);
        }

        [TestMethod]
        public void Delete_Genre()
        {
            var expectedCount = genres.Count - 1;

            service.Delete(1);

            Assert.AreEqual(expectedCount, genres.Count);
        }


    }
}
