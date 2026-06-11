using System;
using System.Collections.Generic;

namespace NbaDeneme2.Models;

public partial class VwTopRebounderRpg
{
    public string PlayerName { get; set; } = null!;

    public string TeamName { get; set; } = null!;

    public decimal? Rpg { get; set; }
}
