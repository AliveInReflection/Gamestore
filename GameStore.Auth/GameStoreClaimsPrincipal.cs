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

        public GameStoreClaimsPrincipal(string userName, List<Claim> claims)
        {
            claims.Add(new Claim(ClaimTypes.Name, userName));

            var identity = new ClaimsIdentity(claims, "Game store auth", ClaimTypes.Name, ClaimTypes.Role);

            AddIdentity(identity);
        }
    }
}
