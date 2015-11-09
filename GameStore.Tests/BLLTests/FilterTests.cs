using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.BLL.Concrete.ContentFilters;
using GameStore.BLL.Concrete.ContentSorters;
using GameStore.BLL.ContentFilters;
using GameStore.BLL.ContentPaginators;
using GameStore.BLL.Services;
using GameStore.CL.AutomapperProfiles;
using GameStore.DAL.Interfaces;
using GameStore.Domain.Entities;
using GameStore.Infrastructure.DTO;
using GameStore.Infrastructure.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GameStore.Tests.BLLTests
{
    [TestClass]
    public class FilterTests
    {
        private List<Genre> genres;
        private List<PlatformType> platformTypes;
        private List<Game> games;
        private List<Comment> comments;
        private List<Publisher> publishers;

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
        private void InitializeCollections()
        {
            genres = new List<Genre>
            {
                new Genre() {GenreId = 1, GenreName = "RTS",},
                new Genre() {GenreId = 2, GenreName = "Action"},
                new Genre() { GenreId = 10, GenreName = "Races" },
            };

            platformTypes = new List<PlatformType>
            {
                new PlatformType() {PlatformTypeId = 1, PlatformTypeName = "Desktop"},
                new PlatformType() {PlatformTypeId = 2, PlatformTypeName = "Console"},
                new PlatformType() {PlatformTypeId = 3, PlatformTypeName = "Browser"},
            };

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
                },
                new Publisher()
                { 
                    PublisherId = 7,
                    CompanyName = "Valve",
                    Description = "Conquire the world",
                    HomePage = "www.valve.com"}
            };


            games = new List<Game>
            {
                new Game()
                {
                    GameId = 1,
                GameName = "StarCraft II",
                GameKey = "SCII",
                Description = "StarCraft II is a sequel to the real-time strategy game StarCraft, announced on May 19, 2007, at the Blizzard World Wide Invitational in Seoul, South Korea.[9][10] It is set to be released as a trilogy.",
                PlatformTypes = new []{platformTypes[0]},
                Genres =new [] {genres[0]},
                Publisher = publishers[0],
                UnitsInStock = 450,
                Discontinued = false,
                Price = 49.99m,
                PublisherId = 1,
                PublicationDate = DateTime.UtcNow - new TimeSpan(365,0,0,0),
                ReceiptDate = new DateTime(2014, 12, 10),
                Comments = new[] {new Comment()},
                Views = new[] {new View()}
                },
                new Game()
                {
                    GameId = 4,
                GameName = "Need for speed: Most wanted",
                GameKey = "NFSMW",
                Description = "Need for Speed: Most Wanted (commonly abbreviated to as NFS: MW or just Most Wanted) is a racing video game developed by EA Black Box and published by Electronic Arts. It is the ninth installment in the Need for Speed series.",
                PlatformTypes = new []{platformTypes[1]},
                Genres =new [] {genres[1]},
                Publisher = publishers[1],
                UnitsInStock = 780,
                Discontinued = false,
                Price = 25,
                PublisherId = 4,
                PublicationDate = DateTime.UtcNow - new TimeSpan(365*2,0,0,0),
                ReceiptDate = new DateTime(2014, 12, 10),
                Comments = new[] {new Comment(), new Comment()},
                Views = new[] {new View(), new View()}
                },
                new Game()
            {
                GameId = 7,
                GameName = "Counter-Strike: Global Offensive ",
                GameKey = "CSGO",
                Description = "Counter-Strike: Global Offensive (CS:GO) is a first-person shooter video game which is a part of the Counter-Strike series. It was announced to the public on August 12, 2011, and is developed by Valve Corporation and their partner, Hidden Path Entertainment. The game was later released on August 21, 2012 for the Playstation 3, Xbox 360, Microsoft Windows, and OS X and later Linux as a downloadable title.",
                PlatformTypes = new[] {platformTypes[2]},
                Genres =new [] {genres[2]},
                Publisher = publishers[2],
                UnitsInStock = 818,
                Discontinued = false,
                Price = 9.99m,
                PublisherId = 7,
                PublicationDate = DateTime.UtcNow - new TimeSpan(365*3,0,0,0),
                ReceiptDate = new DateTime(2014, 12, 10),
                Comments = new[] {new Comment(), new Comment(), new Comment()},
                Views = new[] {new View(), new View(), new View()}
            }
            };

            comments = new List<Comment>
            {
                new Comment()
                {
                    CommentId = 1,
                    Content = "Is it miltiplayer only?",
                    Game = games[1],
                    GameId = games[1].GameId
                },
                new Comment()
                {
                    CommentId = 2,
                    Content = "No. It has offline mode to play with bots.",
                    Game = games[1],
                    GameId = games[1].GameId
                },
                new Comment() {CommentId = 3, Content = "Nice game", Game = games[0],
                    GameId = games[0].GameId}
            };
            comments[1].ParentComment = comments[0];

            games[0].Comments = new List<Comment> { comments[2] };
            games[1].Comments = new List<Comment> { comments[0], comments[1] };

            genres[0].Games = new List<Game> { games[0] };
            genres[1].Games = new List<Game> { games[1] };

            platformTypes[0].Games = new List<Game> { games[0], games[1] };
            platformTypes[1].Games = new List<Game> { games[1] };

        }

        private void InitializeMocks()
        {
            
        }

        private void InitializeTestEntities()
        {
            testGenreIds = new[] {genres[0].GenreId, genres[1].GenreId};
            testPlatformTypeIds = new[] {platformTypes[0].PlatformTypeId, platformTypes[1].PlatformTypeId};
            testPublisherTypeIds = new[] {publishers[0].PublisherId, publishers[1].PublisherId};
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
        #endregion

        [TestMethod]
        public void Filter_Games_By_Genres()
        {
            var filter = new GameFilterByGenre(testGenreIds);
            var expression = filter.GetExpression();
            var expectedCount = 2;

            var result = games.Where(expression.Compile());

            Assert.AreEqual(expectedCount, result.Count());
        }

        [TestMethod]
        public void Filter_Games_By_Platform_Types()
        {
            var filter = new GameFilterByPlatformType(testPlatformTypeIds);
            var expression = filter.GetExpression();
            var expectedCount = 2;

            var result = games.Where(expression.Compile());

            Assert.AreEqual(expectedCount, result.Count());
        }

        [TestMethod]
        public void Filter_Games_By_Publishers()
        {
            var filter = new GameFilterByPublisher(testPublisherTypeIds);
            var expression = filter.GetExpression();
            var expectedCount = 2;

            var result = games.Where(expression.Compile());

            Assert.AreEqual(expectedCount, result.Count());
        }

        [TestMethod]
        public void Filter_Games_By_Price()
        {
            var filter = new GamePriceFilter(testMinPrice, testMaxPrice);
            var expression = filter.GetExpression();
            var expectedCount = 1;

            var result = games.Where(expression.Compile());

            Assert.AreEqual(expectedCount, result.Count());
        }

        [TestMethod]
        public void Filter_Games_By_Publication_Date()
        {
            var filter = new GameFilterByPublitingDate(testPublicationDate);
            var expression = filter.GetExpression();
            var expectedCount = 2;

            var result = games.Where(expression.Compile());

            Assert.AreEqual(expectedCount, result.Count());
        }

        [TestMethod]
        public void Filter_Games_By_Part_Of_Name()
        {
            var filter = new GameFilterByName(testPartOfName);
            var expression = filter.GetExpression();
            var expectedCount = 1;

            var result = games.Where(expression.Compile());

            Assert.AreEqual(expectedCount, result.Count());
        }

        [TestMethod]
        public void Game_Pagination()
        {
            var paginator = new GamePaginator();
            var expectedCount = 1;

            var result = paginator.GetItems(games, testPageNumber, testItemsPerPage);

            Assert.AreEqual(expectedCount, result.Count());
        }

        [TestMethod]
        public void Game_Sorting_By_Comments()
        {
            var sorter = new GameSorterByComments();
            var expectedFirstGame = games[2];

            var result = sorter.Sort(games);

            Assert.AreEqual(expectedFirstGame, result.First());
        }

        [TestMethod]
        public void Game_Sorting_By_Views()
        {
            var sorter = new GameSorterByViews();
            var expectedFirstGame = games[2];

            var result = sorter.Sort(games);

            Assert.AreEqual(expectedFirstGame, result.First());
        }

        [TestMethod]
        public void Game_Sorting_By_Date()
        {
            var sorter = new GameSorterByDate();
            var expectedFirstGame = games[0];

            var result = sorter.Sort(games);

            Assert.AreEqual(expectedFirstGame, result.First());
        }

        [TestMethod]
        public void Game_Sorting_By_Price_Asc()
        {
            var sorter = new GameSorterByPrice(SortingDirection.Ascending);
            var expectedFirstGame = games[2];

            var result = sorter.Sort(games);

            Assert.AreEqual(expectedFirstGame, result.First());
        }

        [TestMethod]
        public void Game_Sorting_By_Price_Desc()
        {
            var sorter = new GameSorterByPrice(SortingDirection.Descending);
            var expectedFirstGame = games[0];

            var result = sorter.Sort(games);

            Assert.AreEqual(expectedFirstGame, result.First());
        }



    }


}
