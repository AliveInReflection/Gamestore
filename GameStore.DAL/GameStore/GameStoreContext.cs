﻿using System;
using System.Collections;
using System.Collections.Generic;
using GameStore.Domain.Entities;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using GameStore.Domain.Static;

namespace Gamestore.DAL.Context
{
    public class GameStoreContext : DbContext
    {
        static  GameStoreContext()
        {
            System.Data.Entity.Database.SetInitializer<GameStoreContext>(new GameStoreDbInitializer());
        }

        public GameStoreContext()
        {
            
        }

        public GameStoreContext(string connectionString)
            :base(connectionString)
        {
            
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<PlatformType> PlatformTypes { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetailses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<View> Views { get; set; }
        public DbSet<GameGenre> GameGenre { get; set; }
    }


    public class GameStoreDbInitializer : DropCreateDatabaseIfModelChanges<GameStoreContext>
    {
        protected override void Seed(GameStoreContext db)
        {
            Dictionary<String, String> uniqeFields = new Dictionary<string, string>
            {
                {"Games", "GameKey"},
                {"Genres", "GenreName"},
                {"PlatformTypes", "PlatformTypeName"}
            };
            foreach (var item in uniqeFields)
            {
                String query = String.Format("CREATE UNIQUE INDEX uniqeKey ON [{0}] ([{1}])", item.Key, item.Value);
                db.Database.ExecuteSqlCommand(query);
            }

            var desctop = new PlatformType() { PlatformTypeName = "Desktop" };
            var console = new PlatformType() { PlatformTypeName = "Console" };
            db.PlatformTypes.Add(desctop);
            db.PlatformTypes.Add(new PlatformType() { PlatformTypeName = "Browser" });
            db.PlatformTypes.Add(new PlatformType() { PlatformTypeName = "Mobile" });
            db.PlatformTypes.Add(console);


            db.Genres.Add(new Genre() {GenreId = 1, GenreName = "Strategy"});
            db.Genres.Add(new Genre() { GenreId = 4, GenreName = "RPG" });
            db.Genres.Add(new Genre() { GenreId = 7, GenreName = "Sports" });
            db.Genres.Add(new Genre() { GenreId = 10, GenreName = "Races" });
            db.Genres.Add(new Genre() { GenreId = 13, GenreName = "Action" });
            db.Genres.Add(new Genre() { GenreId = 16, GenreName = "Adventure" });
            db.Genres.Add(new Genre() { GenreId = 19, GenreName = "Puzzle&Skill" });

            db.Genres.Add(new Genre() { GenreId = 22, GenreName = "RTS", ParentGenreId = 1 });
            db.Genres.Add(new Genre() { GenreId = 25, GenreName = "BTS", ParentGenreId = 1 });

            db.Genres.Add(new Genre() { GenreId = 28, GenreName = "Rally", ParentGenreId = 10 });
            db.Genres.Add(new Genre() { GenreId = 31, GenreName = "Arcade", ParentGenreId = 10 });
            db.Genres.Add(new Genre() { GenreId = 34, GenreName = "Formula", ParentGenreId = 10 });
            db.Genres.Add(new Genre() { GenreId = 37, GenreName = "Off-road", ParentGenreId = 10 });

            db.Genres.Add(new Genre() { GenreId = 40, GenreName = "FPS", ParentGenreId = 13 });
            db.Genres.Add(new Genre() { GenreId = 43, GenreName = "TPS", ParentGenreId = 13 });

            //-----------------------------------

            db.Publishers.Add(new Publisher(){PublisherId = 1,CompanyName = "Blizzard",Description = "The best company in the world",HomePage = "battle.net"});
            db.Publishers.Add(new Publisher(){PublisherId = 4,CompanyName = "Electronic Arts",Description = "Only fast", HomePage = "www.needforspeed.com"});
            db.Publishers.Add(new Publisher(){ PublisherId = 7,CompanyName = "Valve",Description = "Conquire the world",HomePage = "www.valve.com"});


            //-----------------------------------
            var sc2 = new Game()
            {
                GameId = 1,
                GameName = "StarCraft II",
                GameKey = "SCII",
                Description = "StarCraft II is a sequel to the real-time strategy game StarCraft, announced on May 19, 2007, at the Blizzard World Wide Invitational in Seoul, South Korea.[9][10] It is set to be released as a trilogy.",
                PlatformTypes = new []{desctop},
                UnitsInStock = 450,
                Discontinued = false,
                Price = 49.99m,
                PublisherId = 1,
                PublicationDate = new DateTime(2012, 9, 17),
                ReceiptDate = new DateTime(2014, 12, 10)
            };
            db.Games.Add(sc2);

            //-----------------------------------

            db.Games.Add(new Game()
            {
                GameId = 4,
                GameName = "Need for speed: Most wanted",
                GameKey = "NFSMW",
                Description = "Need for Speed: Most Wanted (commonly abbreviated to as NFS: MW or just Most Wanted) is a racing video game developed by EA Black Box and published by Electronic Arts. It is the ninth installment in the Need for Speed series.",
                PlatformTypes = new []{console,desctop},
                UnitsInStock = 780,
                Discontinued = false,
                Price = 25,
                PublisherId = 4,
                PublicationDate = new DateTime(2008, 10, 13),
                ReceiptDate = new DateTime(2014, 12, 10)
            });

            //-----------------------------------

            var csgo = new Game()
            {
                GameId = 7,
                GameName = "Counter-Strike: Global Offensive ",
                GameKey = "CSGO",
                Description = "Counter-Strike: Global Offensive (CS:GO) is a first-person shooter video game which is a part of the Counter-Strike series. It was announced to the public on August 12, 2011, and is developed by Valve Corporation and their partner, Hidden Path Entertainment. The game was later released on August 21, 2012 for the Playstation 3, Xbox 360, Microsoft Windows, and OS X and later Linux as a downloadable title.",
                PlatformTypes = new[]{desctop},
                UnitsInStock = 818,
                Discontinued = false,
                Price = 9.99m,
                PublisherId = 7,
                PublicationDate = new DateTime(2013, 12, 1),
                ReceiptDate = new DateTime(2014, 12, 10),
            };

            db.Games.Add(csgo);


            var ghostComment = new Comment() {User = new User(){UserId = 1, UserName = "Ghost"}, Content = "Is it miltiplayer only?", GameId = 7};
            db.Comments.Add(ghostComment);
            db.Comments.Add(new Comment() { User = new User(){UserId = 2, UserName ="Shooter"}, Content = "No. It has offline mode to play with bots.", GameId = 7, ParentComment = ghostComment});
            db.Comments.Add(new Comment() { User = new User(){UserId = 3, UserName ="Sarah Kerrigan"}, Content = "Nice game", GameId = 1 });


            db.Orders.Add(new Order()
            {
                OrderId = 1,
                CustomerId = "1",
                OrderState = OrderState.NotIssued,
                Date = DateTime.UtcNow,
                OrderDetailses = new List<OrderDetails>
                {
                    new OrderDetails() {OrderDetailsId = 1, Product = sc2, Quantity = 2, Discount = 0.1f},
                    new OrderDetails() {OrderDetailsId = 2, Product = csgo, Quantity = 3, Discount = 0.15f}
                },
            });

            db.GameGenre.Add(new GameGenre() {GameId = 1, GenreId = 1});
            db.GameGenre.Add(new GameGenre() { GameId = 1, GenreId = 22 });
            db.GameGenre.Add(new GameGenre() { GameId = 1, GenreId = 4 });

            db.GameGenre.Add(new GameGenre() { GameId = 4, GenreId = 4 });
            
            db.GameGenre.Add(new GameGenre() { GameId = 7, GenreId = 13 });
            db.GameGenre.Add(new GameGenre() { GameId = 7, GenreId = 4 });

            db.Views.Add(new View() {GameId = 1, UserId = 1});
            db.Views.Add(new View() { GameId = 1, UserId = 2 });
            db.Views.Add(new View() { GameId = 1, UserId = 3 });
            db.Views.Add(new View() { GameId = 1, UserId = 2 });
            db.Views.Add(new View() { GameId = 4, UserId = 1 });
            db.Views.Add(new View() { GameId = 4, UserId = 2 });
            db.Views.Add(new View() { GameId = 4, UserId = 3 });
            db.Views.Add(new View() { GameId = 7, UserId = 1 });
            db.Views.Add(new View() { GameId = 7, UserId = 2 });

        }
    }
}