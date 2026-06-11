using System;
using System.Collections.Generic;

namespace NbaDeneme2.Models;

public partial class VwTopBlockerBpg
{
    public string PlayerName { get; set; } = null!;

    public string TeamName { get; set; } = null!;

    public decimal? Bpg { get; set; }
}
