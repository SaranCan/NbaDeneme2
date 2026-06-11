using System;
using System.Collections.Generic;

namespace NbaDeneme2.Models;

public partial class VwPlayerStat
{
    public string PlayerName { get; set; } = null!;

    public string TeamName { get; set; } = null!;

    public string? Position { get; set; }

    public int? JerseyNumber { get; set; }

    public int? GamesPlayed { get; set; }

    public decimal? Ppg { get; set; }

    public decimal? Rpg { get; set; }

    public decimal? Apg { get; set; }

    public decimal? Spg { get; set; }

    public decimal? Bpg { get; set; }

    public decimal? Mpg { get; set; }
}
