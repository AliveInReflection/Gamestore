using GameStore.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Protocols.WSTrust;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameStore.BLL.DTO;
using GameStore.BLL.Services;
using GameStore.BLL.Infrastructure;

namespace GameStore.WebUI.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        private IGameService gameService;

        public HomeController()
        {
            gameService = new GameService();
        }

        public ActionResult Index()
        {
            
            try
            {
                gameService.AddGame(new GameDTO()
                {
                    GameKey = "ASd",
                    GameName = "asdasdasdasd",
                    Description = "asdasdasdasknakdsfamdfkqwehbffasfbqk"

                });
            }
            catch (ValidationException e)
            {

            }

            gameService.EditGame(new GameDTO()
            {
                GameId = 4,
                GameKey = "gagaASd",
                GameName = "asdasdasdasd",
                Description = "asdasdasdasknakdsfamdfkqwehbffasfbqk"

            });

            gameService.DeleteGame(4);

            var game1 = gameService.GetGameByKey("SCII");

            var games = gameService.GetAllGames();

            gameService.AddComment(new CommentDTO()
            {
                SendersName = "Test",
                Content = "This is test comment",
                GameId = 3
            });

            var comments = gameService.GetCommentsByGameKey("CS:GO");

            var games1 = gameService.GetGamesByGenre(2);

            var games2 = gameService.GetGamesByPlatformTypes(new int[] {1, 2});



            return View();
        }

    }
}
