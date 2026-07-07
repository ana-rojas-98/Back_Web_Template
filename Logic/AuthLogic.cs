using StrategicviewBack.Models;
using StrategicviewBack.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using static IterumBackend.Token;
using System.Security.Claims;
using Newtonsoft.Json;

namespace StrategicviewBack.Logic
{

    public class AuthLogic{


        HelperToken helper;

        private readonly ApplicationDbContext _context;

        public AuthLogic(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            this.helper = new HelperToken(configuration);
        }


        public async Task<ApiResponse<AuthResponse>> Authenticate(LoginRequest login)
        {

            try
            {

             var user = await _context.Users.Where(user =>
                user.Email == login.Email &&
                user.Password == login.Password
             ).Include(x => x.RoleNavigation).FirstOrDefaultAsync();

        
            if( user == null)
            {
              return new ApiResponse<AuthResponse>(true,true, "Invalid credentials");
            }
            else
            {

                if(user.RoleNavigation == null)
                {
                    return new ApiResponse<AuthResponse>(true,true, "The user does not have an assigned role");
                }

             var userCompanies = await _context.CompanyUsers.
                                 Where(x => x.UserId == user.UserId).Select(u => new UserCompanyDto
                                 {   
                                  UserId = u.UserId, 
                                  CompanyId = u.CompanyId
                                 })
            
                                 .ToListAsync(); 

             if(userCompanies == null)
              {
                    return new ApiResponse<AuthResponse>(true,true, "The user does not have an assigned company");
              }

              // Load child permissions assigned to the user's role.
              var roleId = user.RoleNavigation.RoleId; 
              var childPermissions = await _context.RolePermissions
              .Where(rp => rp.RoleId == roleId).Join(_context.Permissions,rp => rp.PermissionId,p => p.PermissionId,(rp, p) => 
              new PermissionDto
               {
                  Id = p.PermissionId,
                  Name = p.PermissionName,
                  Route = p.ApplicationUrl,
                  IsParent = p.IsParent,
                  ParentPermissionId = p.ParentPermissionId,
                  Icon = p.Icon
               }).Where(p => p.IsParent == false)
               .ToListAsync();

               var parentPermissions = await _context.Permissions
               .Where(p => childPermissions.Select(h => h.ParentPermissionId).Contains(p.PermissionId))
               .Select(p => new PermissionDto
               {
                Id = p.PermissionId,
                Name = p.PermissionName,
                Route = p.ApplicationUrl,
                IsParent = p.IsParent,
                ParentPermissionId = p.ParentPermissionId,
                Icon = p.Icon
               }).ToListAsync();


               foreach (var parentPermission in parentPermissions)
               {
                parentPermission.Children = childPermissions
                .Where(h => h.ParentPermissionId == parentPermission.Id)
                .ToList();
               }


             string? companyId = null; 

             if(userCompanies.Count()==1)
             {
                companyId =  userCompanies.FirstOrDefault()?.CompanyId.ToString() ?? string.Empty;
             }
            
            
            var userCompaniesJson = JsonConvert.SerializeObject(userCompanies);


            var permissionsJson = JsonConvert.SerializeObject(childPermissions);

            
            var claims = new List<Claim>
            {
                new Claim("user_id", user.UserId.ToString()),
                new Claim("company_id", companyId ?? string.Empty),
                new Claim("user_companies", userCompaniesJson),
                new Claim("email", user.Email ?? string.Empty),
                new Claim("full_name", user.FirstName +" "+ user.LastName),
                new Claim("role", user.RoleNavigation.RoleName ?? string.Empty),
                new Claim("permissions_json", permissionsJson),
            };

            JwtSecurityToken token = new(
             issuer: helper.Issuer
             , audience: helper.Audience
             , claims: claims
             , expires: DateTime.UtcNow.AddMinutes(43800)
             , notBefore: DateTime.UtcNow
             , signingCredentials:
            new SigningCredentials(this.helper.GetKeyToken(), SecurityAlgorithms.HmacSha256)
            );

            var response = new JwtSecurityTokenHandler().WriteToken(token);

            AuthResponse authResponse = new AuthResponse(); 
            authResponse.Token = response; 
            authResponse.Email = user.Email; 
            authResponse.FullName = user.FirstName +" "+ user.LastName; 
            authResponse.Role = user.RoleNavigation.RoleName; 
            authResponse.Permissions = parentPermissions; 
            

            return new ApiResponse<AuthResponse>(true,false, "Login successful",authResponse);

            }
            }
            catch (Exception ex)
            {
                return new ApiResponse<AuthResponse>(false,true, $"Error: {ex.Message}");
            }
        }

        
        public async Task<ApiResponse<List<CompanyDto>>> GetUserCompanies(int userId)
        {

        try
          {
            var userCompanies = await (from eu in _context.CompanyUsers
                               join e in _context.Companies on eu.CompanyId equals e.CompanyId
                               where eu.UserId == userId
                               select new CompanyDto
                               {
                                   Company = e.CompanyName,
                                   CompanyId = e.CompanyId
                               }).ToListAsync();

            if (userCompanies.Any())
               {
                return new ApiResponse<List<CompanyDto>>(true,false,"OK", userCompanies);
               }
            else
               {
                return new ApiResponse<List<CompanyDto>>(true, true, "No companies were found for this user.");
               }

            }
            catch (Exception ex)
            {
                return new ApiResponse<List<CompanyDto>>(false,true, $"Error: {ex.Message}");
            }

        }

        public Task<ApiResponse<AuthResponse>>  GetTokenCompany(AuthenticatedUserRequest userRequest, int companyId)
        {

             try
            {

             var idUser = userRequest.UserId;
             var companies = userRequest.UserCompaniesJson; 

             if (!string.IsNullOrEmpty(companies))
             {
                var userCompanies = JsonConvert.DeserializeObject<List<UserCompanyDto>>(companies) ?? new List<UserCompanyDto>();

                var requestedCompany = userCompanies.FirstOrDefault(e => e.CompanyId == companyId);

                if (requestedCompany != null)
                {

                    var claims = new List<Claim>
                    {
                        new Claim("user_id", idUser ?? string.Empty),
                        new Claim("company_id", companyId.ToString() ?? string.Empty),
                        new Claim("user_companies", companies),
                        new Claim("email", userRequest.Email ?? string.Empty),
                        new Claim("full_name", userRequest.FullName ?? string.Empty),
                        new Claim("role", userRequest.Role ?? string.Empty),
                        new Claim("permissions_json", userRequest.PermissionsJson ?? string.Empty),

                    };

                    JwtSecurityToken token = new(
                        issuer: helper.Issuer
                        , audience: helper.Audience
                        , claims: claims
                        ,expires: DateTime.UtcNow.AddMinutes(43800)
                        , notBefore: DateTime.UtcNow
                        , signingCredentials:
                        new SigningCredentials(this.helper.GetKeyToken(), SecurityAlgorithms.HmacSha256)
                        );

                    var response = new JwtSecurityTokenHandler().WriteToken(token);
                    AuthResponse authResponse = new AuthResponse(); 
                    authResponse.Token = response; 
                    authResponse.Email = userRequest.Email; 
                    authResponse.FullName = userRequest.FullName; 
                    authResponse.Role = userRequest.Role; 

                    return Task.FromResult(new ApiResponse<AuthResponse>(true,false, "OK",authResponse));
           
                }
                else{

                    return Task.FromResult(new ApiResponse<AuthResponse>(true,true, "The user does not have permission to access this company"));

                }


            }
            else
            {
               return Task.FromResult(new ApiResponse<AuthResponse>(true,true, "No available companies were found for this user"));
            }
            }

            catch (Exception ex)
            {
                return Task.FromResult(new ApiResponse<AuthResponse>(false,true, $"Error: {ex.Message}"));
            }

  

        }
    }


}
