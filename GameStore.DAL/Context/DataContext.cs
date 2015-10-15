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
            var desctop = new PlatformType() {PlatformTypeName = "Desktop"};
            var console = new PlatformType() {PlatformTypeName = "Console"};
            db.PlatformTypes.Add(desctop);
            db.PlatformTypes.Add(new PlatformType() {PlatformTypeName = "Browser"});
            db.PlatformTypes.Add(new PlatformType() {PlatformTypeName = "Mobile"});
            db.PlatformTypes.Add(console);


            var strategy = new Genre() {GenreName = "Strategy"};
            var rpg = new Genre() {GenreName = "RPG"};
            var fps = new Genre() {GenreName = "TPS", ParentGenreId = 5};
            var races = new Genre() {GenreName = "Races"};
            
            
            db.Genres.Add(strategy);
            db.Genres.Add(rpg);
            db.Genres.Add(new Genre() { GenreName = "Sports" });
            db.Genres.Add(races);
            db.Genres.Add(new Genre() { GenreName = "Action" });
            db.Genres.Add(new Genre() { GenreName = "Adventure" });
            db.Genres.Add(new Genre() { GenreName = "Puzzle&Skill" });

            db.Genres.Add(new Genre() { GenreName = "RTS", ParentGenreId = 1 });
            db.Genres.Add(new Genre() { GenreName = "BTS", ParentGenreId = 1 });

            db.Genres.Add(new Genre() { GenreName = "Rally", ParentGenreId = 4 });
            db.Genres.Add(new Genre() { GenreName = "Arcade", ParentGenreId = 4 });
            db.Genres.Add(new Genre() { GenreName = "Formula", ParentGenreId = 4 });
            db.Genres.Add(new Genre() { GenreName = "Off-road", ParentGenreId = 4 });

            db.Genres.Add(new Genre() { GenreName = "FPS", ParentGenreId = 5 });
            db.Genres.Add(fps);


            var genres = new List<Genre>();
            genres.Add(strategy);
            genres.Add(rpg);
            var types = new List<PlatformType>();
            types.Add(desctop);
            db.Games.Add(new Game()
            {
                GameName = "StarCraft II", GameKey = "SCII", Description = "StarCraft II is a sequel to the real-time strategy game StarCraft, announced on May 19, 2007, at the Blizzard World Wide Invitational in Seoul, South Korea.[9][10] It is set to be released as a trilogy.",
                Genres = genres, PlatformTypes = types
            });

            genres.Clear();
            genres.Add(races);
            types.Add(console);
            db.Games.Add(new Game()
            {
                GameName = "Need for speed: Most wanted", GameKey = "NFS:MW", Description = "Need for Speed: Most Wanted (commonly abbreviated to as NFS: MW or just Most Wanted) is a racing video game developed by EA Black Box and published by Electronic Arts. It is the ninth installment in the Need for Speed series.",
                Genres = genres, PlatformTypes = types
            });

            genres.Clear();
            genres.Add(races);

            db.Games.Add(new Game()
            {
                GameName = "Counter-Strike: Global Offensive ", GameKey = "CS:GO", Description = "Counter-Strike: Global Offensive (CS:GO) is a first-person shooter video game which is a part of the Counter-Strike series. It was announced to the public on August 12, 2011, and is developed by Valve Corporation and their partner, Hidden Path Entertainment. The game was later released on August 21, 2012 for the Playstation 3, Xbox 360, Microsoft Windows, and OS X and later Linux as a downloadable title.",
                Genres = genres, PlatformTypes = types
            });


            db.Comments.Add(new Comment() {SendersName = "Ghost", Content = "Is it miltiplayer only?", GameId = 3});
            db.Comments.Add(new Comment() { SendersName = "Shooter", Content = "No. It has offline mode to play with bots.", GameId = 3, ParentId = 1});
            db.Comments.Add(new Comment() { SendersName = "Sarah Kerrigan", Content = "Nice game", GameId = 1 });

        }
    }
}
