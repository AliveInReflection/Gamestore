using System.Globalization;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using GameStore.Logger.Interfaces;
using System.Web.Routing;

namespace GameStore.WebUI.ApiControllers
{
    public class BaseApiController : ApiController
    {
        protected IGameStoreLogger Logger { get; private set; }
        public string Language { get; protected set; }

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

        protected override void Initialize(HttpControllerContext context)
        {
            if (context.RouteData.Values["lang"] != null && context.RouteData.Values["lang"] as string != "null")
            {
                Language = context.RouteData.Values["lang"] as string;
                var ci = new CultureInfo(Language);
                Thread.CurrentThread.CurrentUICulture = ci;
                Thread.CurrentThread.CurrentCulture = ci;
            }
            base.Initialize(context);
        }
    }
}
