using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Web.Mvc;
using AutoMapper;
using GameStore.BLL.Infrastructure;
using GameStore.Infrastructure.BLInterfaces;
using GameStore.Infrastructure.DTO;
using GameStore.Infrastructure.Enums;
using GameStore.Logger.Interfaces;
using GameStore.WebUI.App_LocalResources.Localization;
using GameStore.WebUI.Filters;
using GameStore.WebUI.Infrastructure;
using GameStore.WebUI.Models;

namespace GameStore.WebUI.Controllers
{
    public class GameController : BaseController
    {
        private IGameService gameService;
        private IGenreService genreService;
        private IPlatformTypeService platformTypeService;
        private IPublisherService publisherService;
        private IGameStoreLogger logger;


        public GameController(IGameService gameService, IGenreService genreService, IPlatformTypeService platformTypeService, IPublisherService publisherService, IGameStoreLogger logger)
        {
            this.gameService = gameService;
            this.genreService = genreService;
            this.platformTypeService = platformTypeService;
            this.publisherService = publisherService;
            this.logger = logger;
        }


        [Claims(GameStoreClaim.Games, Permissions.Retreive)]
        public ActionResult Index(FilteringViewModel filter)
        {
            var model = new FilteredGamesViewModel();
            UpdateFilterViewModel(filter);

            var paginatedGames = gameService.Get(BuildFilteringMode(filter));

            model.Games = Mapper.Map<IEnumerable<GameDTO>, IEnumerable<DisplayGameViewModel>>(paginatedGames.Games);

            filter.CurrentPage = paginatedGames.CurrentPage;
            filter.PageCount = paginatedGames.PageCount;
            model.Filter = filter;

            return View(model);
        }

        [Claims(GameStoreClaim.Games, Permissions.Retreive)]
        public ActionResult Details(string gameKey)
        {
            var game = gameService.Get(gameKey);

            var gameMV = Mapper.Map<GameDTO, DisplayGameViewModel>(game);

            return View(gameMV);
        }

        [ActionName("New")]
        [Claims(GameStoreClaim.Games, Permissions.Create)]
        public ActionResult Create()
        {
            var viewModel = new CreateGameViewModel();
            viewModel.Genres = Mapper.Map<IEnumerable<GenreDTO>, IEnumerable<SelectListItem>>(genreService.GetAll());
            viewModel.PlatformTypes = Mapper.Map<IEnumerable<PlatformTypeDTO>, IEnumerable<SelectListItem>>(platformTypeService.GetAll());
            viewModel.Publishers = Mapper.Map<IEnumerable<PublisherDTO>, IEnumerable<SelectListItem>>(publisherService.GetAll());
            return View(viewModel);
        }

        [HttpPost]
        [ActionName("New")]
        [Claims(GameStoreClaim.Games, Permissions.Create)]
        public ActionResult Create(CreateGameViewModel game)
        {
            if (!ModelState.IsValid)
            {
                game.Genres = Mapper.Map<IEnumerable<GenreDTO>, IEnumerable<SelectListItem>>(genreService.GetAll());
                game.PlatformTypes = Mapper.Map<IEnumerable<PlatformTypeDTO>, IEnumerable<SelectListItem>>(platformTypeService.GetAll());
                game.Publishers = Mapper.Map<IEnumerable<PublisherDTO>, IEnumerable<SelectListItem>>(publisherService.GetAll());

                return View(game);
            }

            try
            {
                var gameToSave = Mapper.Map<CreateGameViewModel, GameDTO>(game);
                gameService.Create(gameToSave);
            }
            catch (ValidationException e)
            {
               logger.Warn(e);
               TempData["ErrorMessage"] = ValidationRes.ValidationError;
            }
            
            return RedirectToAction("Index");
        }

        [Claims(GameStoreClaim.Games, Permissions.Update)]
        public ActionResult Update(string gameKey)
        {
            var viewModel = Mapper.Map<GameDTO, UpdateGameViewModel>(gameService.Get(gameKey));
            viewModel.Genres = Mapper.Map<IEnumerable<GenreDTO>, IEnumerable<SelectListItem>>(genreService.GetAll());
            viewModel.PlatformTypes = Mapper.Map<IEnumerable<PlatformTypeDTO>, IEnumerable<SelectListItem>>(platformTypeService.GetAll());
            viewModel.Publishers = Mapper.Map<IEnumerable<PublisherDTO>, IEnumerable<SelectListItem>>(publisherService.GetAll());
            return View(viewModel);
        }

        [HttpPost]
        [Claims(GameStoreClaim.Games, Permissions.Update)]
        public ActionResult Update(UpdateGameViewModel game)
        {
            if (!ModelState.IsValid)
            {
                game.Genres = Mapper.Map<IEnumerable<GenreDTO>, IEnumerable<SelectListItem>>(genreService.GetAll());
                game.PlatformTypes = Mapper.Map<IEnumerable<PlatformTypeDTO>, IEnumerable<SelectListItem>>(platformTypeService.GetAll());
                game.Publishers = Mapper.Map<IEnumerable<PublisherDTO>, IEnumerable<SelectListItem>>(publisherService.GetAll());
                return View(game);
            }
            try
            {
                var gameToUpdate = Mapper.Map<UpdateGameViewModel, GameDTO>(game);
                gameService.Update(gameToUpdate);
            }
            catch (ValidationException e)
            {
                logger.Warn(e);
                TempData["ErrorMessage"] = ValidationRes.ValidationError;
            }

            return RedirectToAction("Index");

        }

        [HttpPost]
        [ActionName("Remove")]
        [Claims(GameStoreClaim.Games, Permissions.Delete)]
        public ActionResult Delete(int gameId)
        {
            try
            {
                gameService.Delete(gameId);
            }
            catch (Exception e)
            {
                logger.Warn(e);
                TempData["ErrorMessage"] = ValidationRes.ValidationError;
            }
            return RedirectToAction("Index");
        }


        [OutputCache(Duration = 60)]
        [Claims(GameStoreClaim.Games, Permissions.Retreive)]
        public ActionResult GetCount()
        {
            return PartialView(gameService.GetCount());
        }

        [Claims(GameStoreClaim.Games, Permissions.Retreive)]
        public ActionResult Download(string gamekey)
        {
            try
            {
                //control of game existance in db
                var game = gameService.Get(gamekey);
                var rootPath = Server.MapPath("~/App_Data/Binary/");
                byte[] fileBytes = System.IO.File.ReadAllBytes(rootPath + "game.data");
                string fileName = "game.data";
                return File(fileBytes, MediaTypeNames.Application.Octet, fileName);
            }
            catch (InvalidOperationException e)
            {
                logger.Warn(e);
                TempData["ErrorMessage"] = ValidationRes.GameNotFound;
            }
            return RedirectToAction("Index");
        }
        

        #region Private helpers

        
        private void UpdateFilterViewModel(FilteringViewModel filter)
        {
            if (filter.Genres == null)
            {
                filter.Genres = Mapper.Map<IEnumerable<GenreDTO>, List<CheckBoxViewModel>>(genreService.GetAll());
            }

            if (filter.PlatformTypes == null)
            {
                filter.PlatformTypes = Mapper.Map<IEnumerable<PlatformTypeDTO>, List<CheckBoxViewModel>>(platformTypeService.GetAll());
            }

            if (filter.Publishers == null)
            {
                filter.Publishers = Mapper.Map<IEnumerable<PublisherDTO>, List<CheckBoxViewModel>>(publisherService.GetAll());
            }

            filter.ItemsPerPageList = Mapper.Map<IEnumerable<string>, List<SelectListItem>>(PagingManager.GetKeys());

            filter.SortByItems = Mapper.Map<IEnumerable<string>, List<SelectListItem>>(GameSortingModeManager.GetKeys());
            filter.SortByItems.First().Selected = true;

            if (filter.PublishingDates == null)
            {
                filter.PublishingDates = Mapper.Map<IEnumerable<string>, List<RadiobuttonViewModel>>(GamePublishingDateFilteringManager.GetKeys());
            }

            filter.CurrentPage = filter.CurrentPage == 0 ? 1 : filter.CurrentPage;
            filter.ItemsPerPage = filter.ItemsPerPage ?? 10.ToString();

        }

        private GameFilteringMode BuildFilteringMode(FilteringViewModel filter)
        {
            var filteringMode = new GameFilteringMode();

            if (filter.Genres != null)
            {
                filteringMode.GenreIds = filter.Genres.Where(m => m.IsChecked).Select(m => m.Id);
            }

            if (filter.PlatformTypes != null)
            {
                filteringMode.PlatformTypeIds = filter.PlatformTypes.Where(m => m.IsChecked).Select(m => m.Id);
            }

            if (filter.Publishers != null)
            {
                filteringMode.PublisherIds = filter.Publishers.Where(m => m.IsChecked).Select(m => m.Id);
            }

            if (filter.PublishingDate != null)
            {
                filteringMode.PublishingDate = GamePublishingDateFilteringManager.Get(filter.PublishingDate.SelectedValue);
            }

            if (filter.SortBy != null)
            {
                filteringMode.SortingMode = GameSortingModeManager.Get(filter.SortBy); 
            }

            if (filter.ItemsPerPage != null)
            {
                filteringMode.ItemsPerPage = PagingManager.Get(filter.ItemsPerPage);
            }
            
            filteringMode.MinPrice = filter.MinPrice;
            filteringMode.MaxPrice = filter.MaxPrice;                      
            filteringMode.PartOfName = filter.Name;

            filteringMode.CurrentPage = filter.CurrentPage;
            filteringMode.ItemsPerPage = PagingManager.Get(filter.ItemsPerPage);

            return filteringMode;
        }

        #endregion

    }
}
