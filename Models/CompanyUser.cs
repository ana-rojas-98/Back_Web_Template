using System;
using System.Collections.Generic;

namespace StrategicviewBack.Models;

public partial class CompanyUser
{
    public int CompanyUserId { get; set; }

    public int? CompanyId { get; set; }

    public int? UserId { get; set; }

    public virtual Company? CompanyNavigation { get; set; }

    public virtual User? UserNavigation { get; set; }
}
