using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SharedHelpers.HelperServices
{
    public static class AccessTokenService
    {
        // This method is used to get the user id from the access token
        public static int GetUserId(this ClaimsPrincipal principal)
        {
            return principal.FindFirst("userId") is null ? 0 : int.Parse(principal.FindFirst("userId")!.Value);
        }
        // This method is used to get the company id from the access token
        public static int GetCompanyId(this ClaimsPrincipal principal)
        {
            return principal.FindFirst("companyId") is null ? 0 : int.Parse(principal.FindFirst("companyId")!.Value);
        }
    }
}
