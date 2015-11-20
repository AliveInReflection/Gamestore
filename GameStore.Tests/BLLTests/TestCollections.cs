using System;
using System.Collections.Generic;
using System.Web.Security;
using GameStore.Domain.Entities;
using GameStore.Infrastructure.Enums;

namespace GameStore.Tests.BLLTests
{
    public class TestCollections
    {
        private List<Genre> genres;
        private List<PlatformType> platformTypes;
        private List<Game> games;
        private List<Comment> comments;
        private List<Publisher> publishers;
        private List<User> users;
        private List<Role> roles; 
        private List<Order> orders;
        private List<OrderDetails> orderDetailses;


        public TestCollections()
        {
            Initialize();
        }

        public List<Genre> Genres { get { return genres; } }
        public List<PlatformType> PlatformTypes { get { return platformTypes; } }
        public List<Game> Games { get { return games; } }
        public List<Comment> Comments { get { return comments; } }
        public List<Publisher> Publishers { get { return publishers; } }
        public List<User> Users { get { return users; } }
        public List<Role> Roles { get { return roles; } }
        public List<Order> Orders { get { return orders; } }
        public List<OrderDetails> OrderDetailses { get { return orderDetailses; } }



        private void Initialize()
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

            roles = new List<Role>
            {
                new Role()
                {
                    RoleName = "Guest",
                    Claims = new List<RoleClaim>{new RoleClaim() {ClaimType = GameStoreClaim.Comments, ClaimValue = Permissions.Retreive}}
                },
                new Role()
                {
                    RoleName = "User",
                    Claims = new List<RoleClaim>{new RoleClaim() {ClaimType = GameStoreClaim.Comments, ClaimValue = Permissions.Retreive}}
                },
                new Role()
                {
                    RoleName = "Administrator",
                    Claims = new List<RoleClaim>{new RoleClaim() {ClaimType = GameStoreClaim.Comments, ClaimValue = Permissions.Retreive}}
                },
                new Role()
                {
                    RoleName = "Manager",
                    Claims = new List<RoleClaim>{new RoleClaim() {ClaimType = GameStoreClaim.Comments, ClaimValue = Permissions.Retreive}}
                }
            };

            users = new List<User>
            {
                new User() { UserName = "Administrator", DateOfBirth = new DateTime(1990, 1, 12), Country = Countries.Ukraine, Password = "qwerty", Roles = new[] { roles[2] } },
                new User() { UserName = "Manager", DateOfBirth = new DateTime(1991, 3, 23), Country = Countries.Ukraine, Password = "qwerty", Roles = new[] { roles[3] } },
                new User() { UserName = "Ghost", DateOfBirth = new DateTime(1993,12,17), Country = Countries.Ukraine, Password = "qwerty", Roles = new[] { roles[1] } },
                new User() { UserName = "Shooter", DateOfBirth = new DateTime(1992, 8, 11), Country = Countries.Ukraine, Password = "qwerty", Roles = new[] { roles[1] } },
                new User() { UserName = "Sarah Kerrigan", DateOfBirth = new DateTime(1991, 4, 1), Country = Countries.Ukraine, Password = "qwerty", Roles = new[] { roles[1] } }
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

            genres[0].Games = new[] { games[0] };
            genres[1].Games = new[] { games[1] };
            genres[2].Games = new[] { games[2] };

            platformTypes[0].Games = new[] { games[0] };
            platformTypes[1].Games = new[] { games[1] };
            platformTypes[2].Games = new[] { games[2] };


            comments = new List<Comment>
            {
                new Comment()
                {
                    CommentId = 1,
                    Content = "Is it miltiplayer only?",
                    Game = games[1],
                    GameId = games[1].GameId,
                    User = users[0],
                },
                new Comment()
                {
                    CommentId = 2,
                    Content = "No. It has offline mode to play with bots.",
                    Game = games[1],
                    GameId = games[1].GameId,
                    User = users[1],
                },
                new Comment() {
                    CommentId = 3, 
                    Content = "Nice game", 
                    Game = games[0],
                    GameId = games[0].GameId,
                    User = users[2],
}

            };

            comments[1].ParentComment = comments[0];

            games[0].Comments = new List<Comment> { comments[2] };
            games[1].Comments = new List<Comment> { comments[0], comments[1] };

            genres[0].Games = new List<Game> { games[0] };
            genres[1].Games = new List<Game> { games[1] };

            platformTypes[0].Games = new List<Game> { games[0], games[1] };
            platformTypes[1].Games = new List<Game> { games[1] };


            orders = new List<Order>
            {
                new Order()
                {
                    OrderId = 1,
                    Customer = users[0],
                    OrderState = OrderState.NotIssued,
                    Date = DateTime.UtcNow,
                    OrderDetailses = new List<OrderDetails>
                    {
                        new OrderDetails() {OrderDetailsId = 1, Product = games[0], ProductId = games[0].GameId, Quantity = 2, Discount = 0},
                        new OrderDetails() {OrderDetailsId = 2, Product = games[1], ProductId = games[1].GameId, Quantity = 2, Discount = 0}
                    }
                }
            };

            orderDetailses = new List<OrderDetails>(orders[0].OrderDetailses);
            orderDetailses[0].Order = orders[0];
            orderDetailses[1].Order = orders[0];

        }
    }
}
