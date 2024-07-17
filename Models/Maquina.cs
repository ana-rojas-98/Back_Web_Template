using System;
using System.Collections.Generic;

namespace StrategicviewBack.Models;

public partial class Maquina
{
    public int Id { get; set; }

    public int? IdMachine { get; set; }

    public string? Reference { get; set; }

    public string? Plant { get; set; }
}
