using System;
using System.Collections.Generic;

namespace StrategicviewBack.Models;

public partial class Company
{
    public int CompanyId { get; set; }

    public string? Industry { get; set; }

    public string? CompanyName { get; set; }

    public string? Country { get; set; }

    public string? State { get; set; }

    public string? City { get; set; }

    public int? PostalCode { get; set; }

    public string? CompanyAddress { get; set; }

    public string? CompanyPhone { get; set; }

    public string? CompanyEmail { get; set; }

    public string? ManagerFirstName { get; set; }

    public string? ManagerLastName { get; set; }

    public string? ManagerPhone { get; set; }

    public string? ManagerEmail { get; set; }

    public string? ContactFirstName { get; set; }

    public string? ContactLastName { get; set; }

    public string? ContactPhone { get; set; }

    public string? ContactEmail { get; set; }

    public string? ContactPosition { get; set; }

    public DateOnly? CreatedAt { get; set; }

    public virtual ICollection<CompanyUser> CompanyUsers { get; set; } = new List<CompanyUser>();
}
