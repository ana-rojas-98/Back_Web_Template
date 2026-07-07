using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using StrategicviewBack.Models.DTO; 

namespace StrategicviewBack.Utilities
{
    public static class UserUtility
    {
        public static AuthenticatedUserRequest GetAuthenticatedUserRequestFromClaims(HttpContext httpContext)
        {
            var userId = httpContext.User.FindFirst("user_id")?.Value;
            var companyIdClaim = httpContext.User.FindFirst("company_id")?.Value;
            var companyId = string.IsNullOrEmpty(companyIdClaim) ? 0 : Convert.ToInt16(companyIdClaim);
            var userCompaniesJson = httpContext.User.FindFirst("user_companies")?.Value;
            var email = httpContext.User.FindFirst(ClaimTypes.Email)?.Value
                ?? httpContext.User.FindFirst("email")?.Value;
            var fullName = httpContext.User.FindFirst("full_name")?.Value;
            var role = httpContext.User.FindFirst("role")?.Value;
            var permissionsJson = httpContext.User.FindFirst("permissions_json")?.Value;

            return new AuthenticatedUserRequest
            {
                UserId = userId,
                CompanyId = companyId,
                UserCompaniesJson = userCompaniesJson,
                Email = email,
                FullName = fullName,
                Role = role,
                PermissionsJson = permissionsJson
            };
        }
    }
}
