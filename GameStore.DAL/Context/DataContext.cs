using System;
using System.Collections;
using System.Collections.Generic;
using GameStore.Domain.Entities;
using System.Data.Entity;
using System.Data.Metadata.Edm;

namespace Gamestore.DAL.Context
{
    public class DataContext : DbContext
    {
        static  DataContext()
        {
            System.Data.Entity.Database.SetInitializer<DataContext>(new GameStoreDbInitializer());
        }

        public DataContext()
        {
            
        }

        public DataContext(string connectionString)
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
    }


    public class GameStoreDbInitializer : DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext db)
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



            var strategy = new Genre() {GenreName = "Strategy"};
            var rpg = new Genre() {GenreName = "RPG"};
            var races = new Genre() {GenreName = "Races"};
            var action = new Genre() {GenreName = "Action"};
            var rts = new Genre() {GenreName = "RTS", ParentGenre = strategy};

            db.Genres.Add(strategy);
            db.Genres.Add(rpg);
            db.Genres.Add(new Genre() { GenreName = "Sports" });
            db.Genres.Add(races);
            db.Genres.Add(action);
            db.Genres.Add(new Genre() { GenreName = "Adventure" });
            db.Genres.Add(new Genre() { GenreName = "Puzzle&Skill" });

            db.Genres.Add(rts);
            db.Genres.Add(new Genre() { GenreName = "BTS", ParentGenre = strategy});

            db.Genres.Add(new Genre() { GenreName = "Rally", ParentGenre = races});
            db.Genres.Add(new Genre() { GenreName = "Arcade", ParentGenre = races});
            db.Genres.Add(new Genre() { GenreName = "Formula", ParentGenre = races});
            db.Genres.Add(new Genre() { GenreName = "Off-road", ParentGenre = races});

            db.Genres.Add(new Genre() { GenreName = "FPS", ParentGenre = action});
            db.Genres.Add(new Genre() {GenreName = "TPS", ParentGenre = action});

            //-----------------------------------

            var types = new List<PlatformType>();
            types.Add(desctop);

            var genres = new List<Genre>();
            genres.Add(strategy);
            genres.Add(rts);
            genres.Add(rpg);

            var sc2 = new Game()
            {
                GameName = "StarCraft II",
                GameKey = "SCII",
                Description = "StarCraft II is a sequel to the real-time strategy game StarCraft, announced on May 19, 2007, at the Blizzard World Wide Invitational in Seoul, South Korea.[9][10] It is set to be released as a trilogy.",
                Genres = genres,
                PlatformTypes = types,
                UnitsInStock = 12,
                Discontinued = false,
                Price = 49.99m,
                Publisher = new Publisher()
                {
                    PublisherId = 1,
                    CompanyName = "Blizzard",
                    Description = "The best company in the world",
                    HomePage = "battle.net"
                },
                PublitingDate = new DateTime(2012,9,17),
                AdditionDate = new DateTime(2014, 12, 10),
                Views = new List<View>
                {
                    new View(){UserId = 1},
                    new View(){UserId = 2},
                    new View(){UserId = 3}
                }
            };
            db.Games.Add(sc2);

            //-----------------------------------

            types.Add(console);

            genres = new List<Genre>();
            genres.Add(races);

            db.Games.Add(new Game()
            {
                GameName = "Need for speed: Most wanted",
                GameKey = "NFS:MW",
                Description = "Need for Speed: Most Wanted (commonly abbreviated to as NFS: MW or just Most Wanted) is a racing video game developed by EA Black Box and published by Electronic Arts. It is the ninth installment in the Need for Speed series.",
                Genres = genres,
                PlatformTypes = types,
                UnitsInStock = 45,
                Discontinued = false,
                Price = 25,
                Publisher = new Publisher()
                {
                    PublisherId = 2,
                    CompanyName = "Electronic Arts",
                    Description = "Only fast",
                    HomePage = "www.needforspeed.com"
                },
                PublitingDate = new DateTime(2008,10,13),
                AdditionDate = new DateTime(2014, 12, 10),
                Views = new List<View>
                {
                    new View(){UserId = 1},
                    new View(){UserId = 2}
                }
            });

            //-----------------------------------

            genres = new List<Genre>();
            genres.Add(action);

            var csgo = new Game()
            {
                GameName = "Counter-Strike: Global Offensive ",
                GameKey = "CSGO",
                Description = "Counter-Strike: Global Offensive (CS:GO) is a first-person shooter video game which is a part of the Counter-Strike series. It was announced to the public on August 12, 2011, and is developed by Valve Corporation and their partner, Hidden Path Entertainment. The game was later released on August 21, 2012 for the Playstation 3, Xbox 360, Microsoft Windows, and OS X and later Linux as a downloadable title.",
                Genres = genres,
                PlatformTypes = types,
                UnitsInStock = 102,
                Discontinued = false,
                Price = 9.99m,
                Publisher = new Publisher()
                {
                    PublisherId = 3,
                    CompanyName = "Valve",
                    Description = "Conquire the world",
                    HomePage = "www.valve.com"
                },
                PublitingDate = new DateTime(2013,12,1),
                AdditionDate = new DateTime(2014, 12, 10),
                Views = new List<View>
                {
                    new View(){UserId = 1},
                    new View(){UserId = 2},
                    new View(){UserId = 3},
                    new View(){UserId = 4}
                }
            };

            db.Games.Add(csgo);


            var ghostComment = new Comment() {User = new User(){UserId = 1, UserName = "Ghost"}, Content = "Is it miltiplayer only?", Game = csgo};
            db.Comments.Add(ghostComment);
            db.Comments.Add(new Comment() { User = new User(){UserId = 2, UserName ="Shooter"}, Content = "No. It has offline mode to play with bots.", Game = csgo, ParentComment = ghostComment});
            db.Comments.Add(new Comment() { User = new User(){UserId = 3, UserName ="Sarah Kerrigan"}, Content = "Nice game", Game = sc2 });


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
        }
    }
}
