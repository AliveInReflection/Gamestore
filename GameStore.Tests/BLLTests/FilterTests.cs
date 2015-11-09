using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GameStore.BLL.Concrete.ContentFilters;
using GameStore.BLL.Concrete.ContentSorters;
using GameStore.BLL.ContentFilters;
using GameStore.BLL.ContentPaginators;
using GameStore.BLL.Services;
using GameStore.CL.AutomapperProfiles;
using GameStore.Infrastructure.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameStore.Tests.BLLTests
{
    [TestClass]
    public class FilterTests
    {
        private TestCollections collections;

        private IEnumerable<int> testPlatformTypeIds;
        private IEnumerable<int> testGenreIds;
        private IEnumerable<int> testPublisherTypeIds;

        private decimal testMinPrice = 10;
        private decimal testMaxPrice = 30;
        private TimeSpan testPublicationDate = new TimeSpan(800, 0, 0, 0);
        private string testPartOfName = "Sta";

        private int testItemsPerPage = 2;
        private int testPageNumber = 2;

        private GameService service;

        #region initialize


        private void InitializeTestEntities()
        {
            testGenreIds = new[] {collections.Genres[0].GenreId, collections.Genres[1].GenreId};
            testPlatformTypeIds = new[] {collections.PlatformTypes[0].PlatformTypeId, collections.PlatformTypes[1].PlatformTypeId};
            testPublisherTypeIds = new[] {collections.Publishers[0].PublisherId, collections.Publishers[1].PublisherId};
        }

        [TestInitialize]
        public void TestInitialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new AutomapperBLLProfile());
            });
            collections = new TestCollections();
            InitializeTestEntities();
        }
        #endregion

        [TestMethod]
        public void Filter_Games_By_Genres()
        {
            var filter = new GameFilterByGenre(testGenreIds);
            var expression = filter.GetExpression();
            var expectedCount = 2;

            var result = collections.Games.Where(expression.Compile());

            Assert.AreEqual(expectedCount, result.Count());
        }

        [TestMethod]
        public void Filter_Games_By_Platform_Types()
        {
            var filter = new GameFilterByPlatformType(testPlatformTypeIds);
            var expression = filter.GetExpression();
            var expectedCount = 2;

            var result = collections.Games.Where(expression.Compile());

            Assert.AreEqual(expectedCount, result.Count());
        }

        [TestMethod]
        public void Filter_Games_By_Publishers()
        {
            var filter = new GameFilterByPublisher(testPublisherTypeIds);
            var expression = filter.GetExpression();
            var expectedCount = 2;

            var result = collections.Games.Where(expression.Compile());

            Assert.AreEqual(expectedCount, result.Count());
        }

        [TestMethod]
        public void Filter_Games_By_Price()
        {
            var filter = new GamePriceFilter(testMinPrice, testMaxPrice);
            var expression = filter.GetExpression();
            var expectedCount = 1;

            var result = collections.Games.Where(expression.Compile());

            Assert.AreEqual(expectedCount, result.Count());
        }

        [TestMethod]
        public void Filter_Games_By_Publication_Date()
        {
            var filter = new GameFilterByPublitingDate(testPublicationDate);
            var expression = filter.GetExpression();
            var expectedCount = 2;

            var result = collections.Games.Where(expression.Compile());

            Assert.AreEqual(expectedCount, result.Count());
        }

        [TestMethod]
        public void Filter_Games_By_Part_Of_Name()
        {
            var filter = new GameFilterByName(testPartOfName);
            var expression = filter.GetExpression();
            var expectedCount = 1;

            var result = collections.Games.Where(expression.Compile());

            Assert.AreEqual(expectedCount, result.Count());
        }

        [TestMethod]
        public void Game_Pagination()
        {
            var paginator = new GamePaginator();
            var expectedCount = 1;

            var result = paginator.GetItems(collections.Games, testPageNumber, testItemsPerPage);

            Assert.AreEqual(expectedCount, result.Count());
        }

        [TestMethod]
        public void Game_Sorting_By_Comments()
        {
            var sorter = new GameSorterByComments();
            var expectedFirstGame = collections.Games[2];

            var result = sorter.Sort(collections.Games);

            Assert.AreEqual(expectedFirstGame, result.First());
        }

        [TestMethod]
        public void Game_Sorting_By_Views()
        {
            var sorter = new GameSorterByViews();
            var expectedFirstGame = collections.Games[2];

            var result = sorter.Sort(collections.Games);

            Assert.AreEqual(expectedFirstGame, result.First());
        }

        [TestMethod]
        public void Game_Sorting_By_Date()
        {
            var sorter = new GameSorterByDate();
            var expectedFirstGame = collections.Games[0];

            var result = sorter.Sort(collections.Games);

            Assert.AreEqual(expectedFirstGame, result.First());
        }

        [TestMethod]
        public void Game_Sorting_By_Price_Asc()
        {
            var sorter = new GameSorterByPrice(SortingDirection.Ascending);
            var expectedFirstGame = collections.Games[2];

            var result = sorter.Sort(collections.Games);

            Assert.AreEqual(expectedFirstGame, result.First());
        }

        [TestMethod]
        public void Game_Sorting_By_Price_Desc()
        {
            var sorter = new GameSorterByPrice(SortingDirection.Descending);
            var expectedFirstGame = collections.Games[0];

            var result = sorter.Sort(collections.Games);

            Assert.AreEqual(expectedFirstGame, result.First());
        }



    }


}
