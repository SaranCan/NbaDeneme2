using System;
using System.Collections.Generic;

namespace NbaDeneme2.Models;

public partial class VwMatchResult
{
    public int MatchId { get; set; }

    public DateOnly MatchDate { get; set; }

    public string HomeTeam { get; set; } = null!;

    public int? HomeScore { get; set; }

    public int? AwayScore { get; set; }

    public string AwayTeam { get; set; } = null!;

    public string Winner { get; set; } = null!;

    public int? Margin { get; set; }
}
