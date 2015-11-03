using GameStore.BLL.Infrastructure;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using GameStore.BLL.DTO;
using GameStore.WebUI.Models;
using AutoMapper;
using GameStore.WebUI.Infrastructure;

namespace GameStore.WebUI.Controllers
{
    public class GameController : Controller
    {
        private IGameService gameService;
        private IGenreService genreService;
        private IPlatformTypeService platformTypeService;
        private IPublisherService publisherService;


        public GameController(IGameService gameService, IGenreService genreService, IPlatformTypeService platformTypeService, IPublisherService publisherService)
        {
            this.gameService = gameService;
            this.genreService = genreService;
            this.platformTypeService = platformTypeService;
            this.publisherService = publisherService;
        }

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

        public ActionResult Details(string gameKey)
        {
            var game = gameService.Get(gameKey);

            var gameMV = BuildDisplayGameViewModel(game);

            return View(gameMV);
        }

        [ActionName("New")]
        public ActionResult Create()
        {
            var genres = genreService.GetAll();
            ViewBag.Genres = ConvertGenresToSelectListItems(genres);

            var pts = platformTypeService.GetAll();
            ViewBag.PlatformTypes = ConvertPlatformTypesToSelectListItems(pts);

            var publishers = convertPublishersToSelectListItems(publisherService.GetAll());
            ViewBag.Publishers = publishers;

            return View(new CreateGameViewModel());

        }

        [HttpPost]
        public ActionResult Create(CreateGameViewModel game)
        {
            if (!ModelState.IsValid)
            {
                var genres = genreService.GetAll();
                ViewBag.Genres = ConvertGenresToSelectListItems(genres);

                var pts = platformTypeService.GetAll();
                ViewBag.PlatformTypes = ConvertPlatformTypesToSelectListItems(pts);

                var publishers = convertPublishersToSelectListItems(publisherService.GetAll());
                ViewBag.Publishers = publishers;

                return View(game);
            }
            var gameToSave = Mapper.Map<CreateGameViewModel, GameDTO>(game);
            gameService.Create(gameToSave, game.GenreIds, game.PlatformTypeIds);
            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult Update(GameDTO game)
        {
            return View();

        }

        [HttpPost]
        [ActionName("Remove")]
        public ActionResult Delete(int gameId)
        {
            try
            {
                gameService.Delete(gameId);
            }
            catch (Exception)
            {
                
            }
            return RedirectToAction("List", "Game");
        }


        [OutputCache(Duration = 60)]
        public ActionResult GetCount()
        {
            return PartialView(gameService.GetCount());
        }


        public ActionResult Download(string gamekey)
        {
            try
            {
                //control of game existance in db
                var game = gameService.Get(gamekey);
                var rootPath = Server.MapPath("~/App_Data/Binary/");
                byte[] fileBytes = System.IO.File.ReadAllBytes(rootPath + "game.data");
                string fileName = "game.data";
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            }
            catch (ValidationException e)
            {
                return Json("Validation error");
            }
        }
        

        #region Private helpers

        private IEnumerable<SelectListItem> ConvertGenresToSelectListItems(IEnumerable<GenreDTO> genres)
        {
            return genres.Select(m => new SelectListItem()
            {
                Value = m.GenreId.ToString(),
                Text = m.GenreName
            });
        }
        private IEnumerable<SelectListItem> ConvertPlatformTypesToSelectListItems(IEnumerable<PlatformTypeDTO> pts)
        {
            return pts.Select(m => new SelectListItem()
            {
                Value = m.PlatformTypeId.ToString(),
                Text = m.PlatformTypeName
            });
        }
        private IEnumerable<SelectListItem> convertPublishersToSelectListItems(IEnumerable<PublisherDTO> publishers)
        {
            return publishers.Select(m => new SelectListItem()
            {
                Value = m.PublisherId.ToString(),
                Text = m.CompanyName
            });
        }
        private DisplayGameViewModel BuildDisplayGameViewModel(GameDTO game)
        {
            var gameVM = Mapper.Map<GameDTO, DisplayGameViewModel>(game);
            return gameVM;
        }

        
        private void UpdateFilterViewModel(FilteringViewModel filter)
        {
            if (filter.Genres == null)
            {
                filter.Genres = genreService.GetAll().Select(m => new CheckBoxViewModel()
                {
                    Id = m.GenreId,
                    Text = m.GenreName
                }).ToList();
            }

            if (filter.PlatformTypes == null)
            {
                filter.PlatformTypes = platformTypeService.GetAll().Select(m => new CheckBoxViewModel()
                {
                    Id = m.PlatformTypeId,
                    Text = m.PlatformTypeName
                }).ToList();
            }


            if (filter.Publishers == null)
            {
                filter.Publishers = publisherService.GetAll().Select(m => new CheckBoxViewModel()
                {
                    Id = m.PublisherId,
                    Text = m.CompanyName
                }).ToList();
            }

            filter.ItemsPerPageList = PagingManager.GetKeys().Select(m => new SelectListItem()
            {
                Text = m,
                Value = m,
                Selected = m == filter.SortBy
            }).ToList();

            filter.SortByItems = GameSortingModeManager.GetKeys().Select(m => new SelectListItem()
            {
                Text = m,
                Value = m,
                Selected = m == filter.SortBy
            }).ToList();

            if (filter.PublishingDates == null)
            {
                filter.PublishingDates = GamePublishingDateFilteringManager.GetKeys().Select(m => new RadiobuttonViewModel()
                {
                    SelectedValue = m
                }).ToList();
            }

            filter.CurrentPage = filter.CurrentPage == 0 ? 1 : filter.CurrentPage;
            filter.ItemsPerPage = filter.ItemsPerPage == null ? 10.ToString() : filter.ItemsPerPage;


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
