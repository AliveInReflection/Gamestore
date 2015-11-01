using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GameStore.BLL.Concrete;
using GameStore.BLL.Concrete.ContentFilters;
using GameStore.BLL.Concrete.ContentPaginators;
using GameStore.BLL.Concrete.ContentSorters;
using GameStore.BLL.ContentFilters;
using GameStore.BLL.DTO;
using GameStore.BLL.Infrastructure;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Interfaces.ContentFilters;
using GameStore.DAL.Interfaces;
using GameStore.Domain.Entities;
using GameStore.DAL.Concrete;

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

        public void Create(GameDTO game, IEnumerable<int> genreIds, IEnumerable<int> platformTypeIds)
        {
            if (game == null)
            {
                throw new ValidationException("No content received");
            }

            try
            {
                //throws exception when can not find
                var entry = database.Games.GetSingle(m => m.GameKey.Equals(game.GameKey));
                throw new ValidationException("Another game has the same game key");
            }
            catch (InvalidOperationException)
            {
                Mapper.CreateMap<GameDTO, Game>();
                var gameToSave = Mapper.Map<GameDTO, Game>(game);

                gameToSave.Genres = genreIds.Select(id =>
                {
                    var genreEntry = database.Genres.GetSingle(m => m.GenreId.Equals(id));

                    return genreEntry;
                }).ToList();

                gameToSave.PlatformTypes = platformTypeIds.Select(id =>
                {
                    var ptEntry = database.PlatformTypes.GetSingle(m => m.PlatformTypeId.Equals(id));
                    
                    return ptEntry;
                }).ToList();

                gameToSave.Publisher = database.Publishers.GetSingle(m => m.PublisherId.Equals(game.PublisherId));

                database.Games.Create(gameToSave);
                database.Save();
            }
        }

        public void Update(GameDTO game, IEnumerable<int> genreIds, IEnumerable<int> platformTypeIds)
        {
            if (game == null)
            {
                throw new ValidationException("No content received");
            }


            var entry = database.Games.GetSingle(m => m.GameKey.Equals(game.GameKey));
            // if entry and game are different games with the same key
            if (entry.GameId != game.GameId)
            {
                throw new ValidationException("Another game has the same game key");
            }

            entry = database.Games.GetSingle(m => m.GameId.Equals(game.GameId));

            var gameToSave = Mapper.Map(game, entry);

            // Updating game genres
            var existedGenreIds = gameToSave.Genres.Select(m => m.GenreId);

            var genreIdsToAdd = genreIds.Except(existedGenreIds);
            foreach (var id in genreIdsToAdd)
            {
                var genreEntry = database.Genres.GetSingle(m => m.GenreId.Equals(id));
                gameToSave.Genres.Add(genreEntry);
            }

            var genreIdsToRemove = existedGenreIds.Except(genreIds);
            foreach (var id in genreIdsToRemove)
            {
                var genreEntry = database.Genres.GetSingle(m => m.GenreId.Equals(id));
                gameToSave.Genres.Remove(genreEntry);
            }

            // Updating game platform types
            var existedPtIds = gameToSave.PlatformTypes.Select(m => m.PlatformTypeId);

            var ptIdsToAdd = platformTypeIds.Except(existedPtIds);
            foreach (var id in ptIdsToAdd)
            {
                var ptEntry = database.PlatformTypes.GetSingle(m => m.PlatformTypeId.Equals(id));
                gameToSave.PlatformTypes.Add(ptEntry);
            }

            var ptIdsToRemove = existedPtIds.Except(platformTypeIds);
            foreach (var id in ptIdsToRemove)
            {
                var ptEntry = database.PlatformTypes.GetSingle(m => m.PlatformTypeId.Equals(id));
                gameToSave.PlatformTypes.Remove(ptEntry);
            }

            gameToSave.Publisher = database.Publishers.GetSingle(m => m.PublisherId.Equals(game.PublisherId));


            database.Games.Update(gameToSave);
            database.Save();
        }

        public void Delete(int gameId)
        {
            var entry = database.Games.GetSingle(m => m.GameId.Equals(gameId));

            var commentIds = entry.Comments.Select(m => m.CommentId);
            foreach (var commentId in commentIds)
            {
                database.Comments.Delete(commentId);
            }
            database.Games.Delete(gameId);
            database.Save();
        }

        public GameDTO Get(string gameKey)
        {
            var entry = database.Games.GetSingle(m => m.GameKey.Equals(gameKey));

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
            if (!filter.IsEmpty())
            {
                entries = database.Games.GetMany(filter.GetExpression());
            }
            else
            {
                entries = database.Games.GetAll();
            }

            var sorter = GetSorter(filteringMode);
            if (sorter != null)
            {
                entries = sorter.Sort(entries);
            }

            var paginatedGames = new PaginatedGames();
            paginatedGames.CurrentPage = filteringMode.CurrentPage;
            paginatedGames.PageCount = (int)Math.Ceiling((double)entries.Count()/filteringMode.ItemsPerPage);

            entries = paginator.GetItems(entries, filteringMode.CurrentPage, filteringMode.ItemsPerPage);
            var games = Mapper.Map<IEnumerable<Game>, IEnumerable<GameDTO>>(entries);

            paginatedGames.Games = games;

            return paginatedGames;
        }

        public IEnumerable<GameDTO> Get(int genreId)
        {
            var genre = database.Genres.GetSingle(m => m.GenreId.Equals(genreId));

            var gameEntries = genre.Games.ToList();
            var games = Mapper.Map<IEnumerable<Game>, IEnumerable<GameDTO>>(gameEntries);
            return games;
        }

        public IEnumerable<GameDTO> Get(IEnumerable<int> platformTypeIds)
        {
            HashSet<Game> gameEntries = new HashSet<Game>();
            foreach (var typeId in platformTypeIds)
            {
                var platformType = database.PlatformTypes.GetSingle(m => m.PlatformTypeId.Equals(typeId));

                var gamesTMP = platformType.Games.ToList();
                foreach (var game in gamesTMP)
                {
                    gameEntries.Add(game);
                }
            }
            var games = Mapper.Map<IEnumerable<Game>, IEnumerable<GameDTO>>(gameEntries);
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
