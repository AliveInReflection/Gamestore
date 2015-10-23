using GameStore.BLL.Infrastructure;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using GameStore.BLL.DTO;
using GameStore.WebUI.Models;
using AutoMapper;

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
        
        public ActionResult List()
        {           
            var games = Mapper.Map<IEnumerable<GameDTO>, IEnumerable<DisplayGameViewModel>>(gameService.GetAll());
            return View(games);
        }

        [HttpGet]
        public ActionResult Details(string gameKey)
        {
            var game = gameService.Get(gameKey);
            var gameMV = BuildDisplayGameViewModel(game);
            return View(gameMV);
        }

        [HttpGet]
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
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Update(GameDTO game)
        {
            return View();

        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            return View();

        }

        [HttpGet]
        [ChildActionOnly]
        [OutputCache(Duration=60)]
        public ActionResult GetCount()
        {
            return PartialView(gameService.GetCount());
        }

        

        [HttpGet]
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

        #endregion

    }
}
