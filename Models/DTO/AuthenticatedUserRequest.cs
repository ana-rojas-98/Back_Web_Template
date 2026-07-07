namespace StrategicviewBack.Models.DTO
{
    public class AuthenticatedUserRequest
    {
        public string? UserId { get; set; }
        public int CompanyId { get; set; }
        public string? UserCompaniesJson { get; set; }
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public string? Role { get; set; }
        public string? PermissionsJson { get; set; }
    }
}
