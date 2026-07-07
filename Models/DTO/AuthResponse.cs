namespace StrategicviewBack.Models.DTO
{
    public class AuthResponse
    {
        public string? Email { get ; set; }

        public string? FullName { get; set; }

        public string? Role { get; set; }

        public string? Token { get; set; }

        public List<PermissionDto>? Permissions {get; set;}

    }
}
