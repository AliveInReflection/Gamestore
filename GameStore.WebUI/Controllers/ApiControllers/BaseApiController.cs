using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using GameStore.Logger.Interfaces;

namespace GameStore.WebUI.ApiControllers
{
    public class BaseApiController : ApiController
    {
        protected IGameStoreLogger Logger { get; private set; }

        public BaseApiController(IGameStoreLogger logger)
        {
            Logger = logger;
        }

        protected ClaimsPrincipal CurrentUser
        {
            get
            {
                return HttpContext.Current.User as ClaimsPrincipal;
            }
        }
    }
}
