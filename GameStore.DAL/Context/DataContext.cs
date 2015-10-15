using System;
using System.Collections;
using System.Collections.Generic;
using GameStore.Domain.Entities;
using System.Data.Entity;

namespace Gamestore.DAL.Context
{
    public class DataContext : DbContext
    {
        static  DataContext()
        {
            System.Data.Entity.Database.SetInitializer<DataContext>(new GameStoreDbInitializer());
        }

        public DataContext(string connectionString)
            :base(connectionString)
        {
            
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<PlatformType> PlatformTypes { get; set; }
    }


    public class GameStoreDbInitializer : DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext db)
        {
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
                GameName = "StarCraft II", GameKey = "SCII", Description = "StarCraft II is a sequel to the real-time strategy game StarCraft, announced on May 19, 2007, at the Blizzard World Wide Invitational in Seoul, South Korea.[9][10] It is set to be released as a trilogy.",
                Genres = genres, PlatformTypes = types
            };
            db.Games.Add(sc2);

            //-----------------------------------

            types.Add(console);

            genres = new List<Genre>();
            genres.Add(races);

            db.Games.Add(new Game()
            {
                GameName = "Need for speed: Most wanted", GameKey = "NFS:MW", Description = "Need for Speed: Most Wanted (commonly abbreviated to as NFS: MW or just Most Wanted) is a racing video game developed by EA Black Box and published by Electronic Arts. It is the ninth installment in the Need for Speed series.",
                Genres = genres, PlatformTypes = types
            });

            //-----------------------------------

            genres = new List<Genre>();
            genres.Add(action);

            var csgo = new Game()
            {
                GameName = "Counter-Strike: Global Offensive ", GameKey = "CS:GO", Description = "Counter-Strike: Global Offensive (CS:GO) is a first-person shooter video game which is a part of the Counter-Strike series. It was announced to the public on August 12, 2011, and is developed by Valve Corporation and their partner, Hidden Path Entertainment. The game was later released on August 21, 2012 for the Playstation 3, Xbox 360, Microsoft Windows, and OS X and later Linux as a downloadable title."                ,
                Genres = genres, PlatformTypes = types
            };

            db.Games.Add(csgo);


            var ghostComment = new Comment() {SendersName = "Ghost", Content = "Is it miltiplayer only?", Game = csgo};
            db.Comments.Add(ghostComment);
            db.Comments.Add(new Comment() { SendersName = "Shooter", Content = "No. It has offline mode to play with bots.", Game = csgo, ParentComment = ghostComment});
            db.Comments.Add(new Comment() { SendersName = "Sarah Kerrigan", Content = "Nice game", Game = sc2 });

        }
    }
}
