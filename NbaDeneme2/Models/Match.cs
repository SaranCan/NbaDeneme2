using System;
using System.Collections.Generic;

namespace NbaDeneme2.Models;

public partial class Match
{
    public int MatchId { get; set; }

    public int HomeTeamId { get; set; }

    public int AwayTeamId { get; set; }

    public DateOnly MatchDate { get; set; }

    public int ArenaId { get; set; }

    public int SeasonId { get; set; }

    public int? HomeScore { get; set; }

    public int? AwayScore { get; set; }

    public DateTime LastUpdate { get; set; }

    public virtual Arena Arena { get; set; } = null!;

    public virtual Team AwayTeam { get; set; } = null!;

    public virtual Team HomeTeam { get; set; } = null!;

    public virtual ICollection<PlayerGameStat> PlayerGameStats { get; set; } = new List<PlayerGameStat>();

    public virtual Season Season { get; set; } = null!;
}
