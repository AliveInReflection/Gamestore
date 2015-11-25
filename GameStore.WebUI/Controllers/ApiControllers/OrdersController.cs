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
            return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<DisplayOrderViewModel>(order));
        }

        // POST api/<controller>
        [ClaimsApi(GameStoreClaim.Orders, Permissions.Create)]
        public HttpResponseMessage Post(int id)
        {
            try
            {
                var game = gameService.Get(id);
                int userId = int.Parse(CurrentUser.FindFirst(ClaimTypes.SerialNumber).Value);
                orderService.AddItem(userId, game.GameKey, 1);
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
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                int userId = int.Parse(CurrentUser.FindFirst(ClaimTypes.SerialNumber).Value);
                orderService.RemoveItem(userId, id, 1);
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