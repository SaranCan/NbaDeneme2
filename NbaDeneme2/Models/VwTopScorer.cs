using System;
using System.Collections.Generic;

namespace NbaDeneme2.Models;

public partial class VwTopScorer
{
    public string PlayerName { get; set; } = null!;

    public string TeamName { get; set; } = null!;

    public string? Position { get; set; }

    public int? GamesPlayed { get; set; }

    public decimal? Ppg { get; set; }

    public int? TotalPoints { get; set; }
}
