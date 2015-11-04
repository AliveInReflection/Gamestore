using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GameStore.BLL.Concrete;
using GameStore.BLL.Concrete.ContentFilters;
using GameStore.BLL.Concrete.ContentSorters;
using GameStore.BLL.ContentFilters;
using GameStore.BLL.Infrastructure;
using GameStore.DAL.Interfaces;
using GameStore.Domain.Entities;
using GameStore.Infrastructure.BLInterfaces;
using GameStore.Infrastructure.DTO;
using GameStore.Infrastructure.Enums;

namespace GameStore.BLL.Services
{
    public class GameService : IGameService
    {
        private readonly IUnitOfWork database;
        private readonly IContentPaginator<Game> paginator;


        public GameService(IUnitOfWork database, IContentPaginator<Game> paginator)
        {
            this.database = database;
            this.paginator = paginator;
        }

        public void Create(GameDTO game)
        {
            if (game == null)
            {
                throw new ValidationException("No content received");
            }

            try
            {
                var gameToSave = Mapper.Map<GameDTO, Game>(game);
                database.Games.Create(gameToSave);
                database.Save();
            }
            catch (InvalidOperationException)
            {
                throw new ValidationException(string.Format("Another game has the same game key: {0}", game.GameKey));
            }
          
        }

        public void Update(GameDTO game)
        {
            if (game == null)
            {
                throw new ValidationException("No content received");
            }

            try
            {
                var gameToSave = Mapper.Map<GameDTO, Game>(game);
                database.Games.Update(gameToSave);
                database.Save();
            }
            catch (InvalidOperationException)
            {                
                throw new ValidationException(String.Format("Another game with the same key ({0}) exists", game.GameKey));
            }
            
        }

        public void Delete(int gameId)
        {            
            database.Games.Delete(gameId);
            database.Save();
        }

        public GameDTO Get(string gameKey)
        {
            var entry = database.Games.Get(m => m.GameKey.Equals(gameKey));

            Mapper.CreateMap<Game, GameDTO>();
            var game = Mapper.Map<Game, GameDTO>(entry);
            return game;
        }

        public IEnumerable<GameDTO> GetAll()
        {
            var entries = database.Games.GetAll();
            var games = Mapper.Map<IEnumerable<Game>, IEnumerable<GameDTO>>(entries);
            return games;
        }

        public PaginatedGames Get(GameFilteringMode filteringMode)
        {
            IEnumerable<Game> entries;

            var filter = TransformationManager.GetGameFilteringPipeline(filteringMode);
            
            entries = !filter.IsEmpty() ? database.Games.GetMany(filter.GetExpression()) : database.Games.GetAll();

            var sorter = TransformationManager.GetGameSorter(filteringMode.SortingMode);
            if (sorter != null)
            {
                entries = sorter.Sort(entries);
            }

            var paginatedGames = new PaginatedGames
            {
                CurrentPage = filteringMode.CurrentPage,
                PageCount = (int) Math.Ceiling((double) entries.Count()/filteringMode.ItemsPerPage)
            };

            entries = paginator.GetItems(entries, filteringMode.CurrentPage, filteringMode.ItemsPerPage);
            var games = Mapper.Map<IEnumerable<Game>, IEnumerable<GameDTO>>(entries);

            paginatedGames.Games = games;

            return paginatedGames;
        }

        public IEnumerable<GameDTO> Get(int genreId)
        {
            var gameEntries = database.Games.GetMany(m => m.Genres.Any(g => g.GenreId.Equals(genreId)));
            var games = Mapper.Map<IEnumerable<Game>, IEnumerable<GameDTO>>(gameEntries);
            return games;
        }

        public IEnumerable<GameDTO> Get(IEnumerable<int> platformTypeIds)
        {
            var entries =
                database.Games.GetMany(m => m.PlatformTypes.Any(pt => platformTypeIds.Contains(pt.PlatformTypeId)));
            var games = Mapper.Map<IEnumerable<Game>, IEnumerable<GameDTO>>(entries);
            return games;
        }

        public int GetCount()
        {
            return database.Games.Count();
        }
     
    }
}
