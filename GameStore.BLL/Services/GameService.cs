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
using GameStore.Logger.Interfaces;
using Ninject;

namespace GameStore.BLL.Services
{
    public class GameService : IGameService
    {
        private readonly IUnitOfWork database;
        private readonly IContentPaginator<Game> paginator;

        [Inject]
        private IGameStoreLogger Logger { get; set; }


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
            
            var filter = BuildPipeline(filteringMode);
            
            entries = !filter.IsEmpty() ? database.Games.GetMany(filter.GetExpression()) : database.Games.GetAll();

            var sorter = GetSorter(filteringMode);
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


        private IFilteringPipeline<Game> BuildPipeline(GameFilteringMode filteringMode)
        {
            var pipeline = new GameFilterPipeline();

            if (filteringMode.GenreIds.Any())
            {
                pipeline.Add(new GameFilterByGenre(filteringMode.GenreIds));
            }

            if (filteringMode.PlatformTypeIds.Any())
            {
                pipeline.Add(new GameFilterByPlatformType(filteringMode.PlatformTypeIds));
            }

            if (filteringMode.PublisherIds.Any())
            {
                pipeline.Add(new GameFilterByPublisher(filteringMode.PublisherIds));
            }

            if (filteringMode.MaxPrice > 0)
            {
                pipeline.Add(new GamePriceFilter(filteringMode.MinPrice, filteringMode.MaxPrice));
            }
            
            if (!String.IsNullOrEmpty(filteringMode.PartOfName))
            {
                pipeline.Add(new GameFilterByName(filteringMode.PartOfName));
            }

            if (filteringMode.PublishingDate.Days != 0)
            {
                pipeline.Add(new GameFilterByPublitingDate(filteringMode.PublishingDate));
            }


            return pipeline;
        }

        private IContentSorter<Game> GetSorter(GameFilteringMode filteringMode)
        {
            switch (filteringMode.SortingMode)
            {
                case GamesSortingMode.MostPopular: 
                    return new GameSorterByViews();
                case GamesSortingMode.MostCommented: 
                    return new GameSorterByComments();
                case GamesSortingMode.AdditionDate:
                    return new GameSorterByDate();
                case GamesSortingMode.PriceAscending: 
                    return new GameSorterByPrice(SortingDirection.Ascending);
                case GamesSortingMode.PriceDescending:
                    return new GameSorterByPrice(SortingDirection.Descending);
                default :
                    return null;
            }            
        }

    }
}
