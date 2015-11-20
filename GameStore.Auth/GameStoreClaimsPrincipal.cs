using System.Collections.Generic;
using System.Security.Claims;

namespace GameStore.Auth
{
    public class GameStoreClaimsPrincipal : ClaimsPrincipal
    {

        public GameStoreClaimsPrincipal(List<Claim> claims)
        {
            var identity = new ClaimsIdentity(claims, "Game store auth", ClaimTypes.Name, ClaimTypes.Role);

            AddIdentity(identity);
        }
    }
}
