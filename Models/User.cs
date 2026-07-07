using System;
using System.Collections.Generic;

namespace StrategicviewBack.Models;

public partial class User
{
    public int UserId { get; set; }

    public int? RoleId { get; set; }

    public int? ProjectId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? IdentificationType { get; set; }

    public int? IdentificationNumber { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public DateOnly? BirthDate { get; set; }

    public DateOnly? CompanyJoinedAt { get; set; }

    public virtual Role? RoleNavigation { get; set; }

    public virtual ICollection<CompanyUser> CompanyUsers { get; set; } = new List<CompanyUser>();
}
