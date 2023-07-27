using Microsoft.AspNetCore.Http;

namespace SharedHelpers.HelperServices
{
    public static class AccessRequestService
    {
        // Write function to get data from request header
        public static int GetCompanyId(this HttpContext httpContext)
        {
            return int.Parse(httpContext.User.FindFirst("companyId")!.Value);
        }

        // Write function to get user id from request header
        public static string GetUserId(this HttpContext httpContext)
        {
            return httpContext.User.FindFirst("userId")!.Value;
        }
    }
}
