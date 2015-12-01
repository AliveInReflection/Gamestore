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


        public GameController(IGameService gameService, IGenreService genreService, IPlatformTypeService platformTypeService, IPublisherService publisherService, IGameStoreLogger logger)
            :base(logger)
        {
            this.gameService = gameService;
            this.genreService = genreService;
            this.platformTypeService = platformTypeService;
            this.publisherService = publisherService;
        }


        [Claims(GameStoreClaim.Games, Permissions.Retreive)]
        public ActionResult Index(ContentTransformationViewModel transformer)
        {
            var model = new FilteredGamesViewModel();
            UpdateTransformerViewModel(transformer);           

            var paginatedGames = gameService.Get(Mapper.Map<GameFilteringMode>(transformer),
                                                 GetSortingMode(transformer),
                                                 Mapper.Map<PaginationMode>(transformer));

            model.Games = Mapper.Map<IEnumerable<GameDTO>, IEnumerable<DisplayGameViewModel>>(paginatedGames.Games);

            transformer.CurrentPage = paginatedGames.CurrentPage;
            transformer.PageCount = paginatedGames.PageCount;
            model.Transformer = transformer;

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
               Logger.Warn(e);
               SetErrorMessage(ValidationRes.ValidationError);
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
                Logger.Warn(e);
                SetErrorMessage(ValidationRes.ValidationError);
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
                Logger.Warn(e);
                SetErrorMessage(ValidationRes.ValidationError);
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
                Logger.Warn(e);
                SetErrorMessage(ValidationRes.GameNotFound);
            }
            return RedirectToAction("Index");
        }
        

        #region Private helpers


        private void UpdateTransformerViewModel(ContentTransformationViewModel transformer)
        {
            transformer.GenreItems = Mapper.Map<IEnumerable<GenreDTO>, List<CheckBoxViewModel>>(genreService.GetAll());

            transformer.PlatformTypeItems = Mapper.Map<IEnumerable<PlatformTypeDTO>, List<CheckBoxViewModel>>(platformTypeService.GetAll());

            transformer.PublisherItems = Mapper.Map<IEnumerable<PublisherDTO>, List<CheckBoxViewModel>>(publisherService.GetAll());

            transformer.ItemsPerPageList = Mapper.Map<IEnumerable<string>, List<SelectListItem>>(PagingManager.Items.Keys);

            transformer.SortByItems = Mapper.Map<IEnumerable<string>, List<SelectListItem>>(GameSortingModeManager.Items.Keys);
            transformer.SortByItems.First().Selected = true;


            if (transformer.PublishingDates == null)
            {
                transformer.PublishingDates = Mapper.Map<IEnumerable<string>, List<RadiobuttonViewModel>>(GamePublishingDateFilteringManager.Items.Keys);
            }

            transformer.CurrentPage = transformer.CurrentPage == 0 ? 1 : transformer.CurrentPage;
            transformer.ItemsPerPage = transformer.ItemsPerPage ?? 10.ToString();

        }

        private GamesSortingMode GetSortingMode(ContentTransformationViewModel transformer)
        {
            if (transformer.SortBy != null)
            {
                return GameSortingModeManager.Items[transformer.SortBy];
            }
            
            return GamesSortingMode.None;
        }

        #endregion

    }
}
