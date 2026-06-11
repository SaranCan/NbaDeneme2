using System;
using System.Collections.Generic;

namespace NbaDeneme2.Models;

public partial class Season
{
    public int SeasonId { get; set; }

    public int YearStart { get; set; }

    public int YearEnd { get; set; }

    public int? ChampionTeamId { get; set; }

    public DateTime LastUpdate { get; set; }

    public virtual Team? ChampionTeam { get; set; }

    public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();

    public virtual ICollection<Match> Matches { get; set; } = new List<Match>();

    public virtual ICollection<TeamStat> TeamStats { get; set; } = new List<TeamStat>();
}
