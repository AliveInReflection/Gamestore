using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using AutoMapper;
using GameStore.BLL.Infrastructure;
using GameStore.Infrastructure.BLInterfaces;
using GameStore.Infrastructure.Enums;
using GameStore.Logger.Interfaces;
using GameStore.WebUI.Filters;
using GameStore.WebUI.Models.Order;

namespace GameStore.WebUI.ApiControllers
{
    public class OrdersController : BaseApiController
    {
        private IOrderService orderService;
        private IGameService gameService;

        public OrdersController(IGameStoreLogger logger, IOrderService orderService, IGameService gameService) : base(logger)
        {
            this.orderService = orderService;
            this.gameService = gameService;
        }

        // GET api/<controller>
        [ClaimsApi(GameStoreClaim.Orders, Permissions.Create)]
        public HttpResponseMessage Get()
        {   
            int userId = int.Parse(CurrentUser.FindFirst(ClaimTypes.SerialNumber).Value);
            var order = orderService.GetCurrent(userId);
            
            var orderVM = Mapper.Map<DisplayOrderViewModel>(order);
            orderVM.Amount = orderService.CalculateAmount(order.OrderId);
            
            return Request.CreateResponse(HttpStatusCode.OK, orderVM);
        }

        // POST api/<controller>
        [ClaimsApi(GameStoreClaim.Orders, Permissions.Create)]
        public HttpResponseMessage Post(string gameKey, short quantity = 1)
        {
            try
            {
                int userId = int.Parse(CurrentUser.FindFirst(ClaimTypes.SerialNumber).Value);
                orderService.AddItem(userId, gameKey, quantity);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (ValidationException e)
            {
                Logger.Warn(e);
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/<controller>/5
        [ClaimsApi(GameStoreClaim.Orders, Permissions.Create)]
        public HttpResponseMessage Delete(string gameKey, short quantity = 1)
        {
            try
            {
                var game = gameService.Get(gameKey);
                int userId = int.Parse(CurrentUser.FindFirst(ClaimTypes.SerialNumber).Value);
                orderService.RemoveItem(userId, game.GameId, quantity);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (ValidationException e)
            {
                Logger.Warn(e);
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}