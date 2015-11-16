using System.Collections.Generic;
using System.Security.Claims;

namespace GameStore.Auth
{
    public class GameStoreClaimsPrincipal : ClaimsPrincipal
    {
        private List<Claim> claims;

        public GameStoreClaimsPrincipal(string userName, List<Claim> claims)
        {
            claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userName)
            };
            claims.AddRange(claims);

            var identity = new ClaimsIdentity(claims, "Game store auth", ClaimTypes.Name, ClaimTypes.Role);

            AddIdentity(identity);
        }
    }
}
