using System;
using System.Collections.Generic;

namespace NbaDeneme2.Models;

public partial class VwTeamPayroll
{
    public long? Rank { get; set; }

    public string TeamName { get; set; } = null!;

    public string Conference { get; set; } = null!;

    public decimal? TotalPayroll { get; set; }

    public int? NumPlayers { get; set; }
}
