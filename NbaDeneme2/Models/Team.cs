using System;
using System.Collections.Generic;

namespace NbaDeneme2.Models;

public partial class Team
{
    public int TeamId { get; set; }

    public string TeamName { get; set; } = null!;

    public string City { get; set; } = null!;

    public int ArenaId { get; set; }

    public int? FoundedYear { get; set; }

    public string Conference { get; set; } = null!;

    public string Division { get; set; } = null!;

    public DateTime LastUpdate { get; set; }

    public virtual Arena Arena { get; set; } = null!;

    public virtual ICollection<Coach> Coaches { get; set; } = new List<Coach>();

    public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();

    public virtual ICollection<Match> MatchAwayTeams { get; set; } = new List<Match>();

    public virtual ICollection<Match> MatchHomeTeams { get; set; } = new List<Match>();

    public virtual ICollection<Player> Players { get; set; } = new List<Player>();

    public virtual ICollection<Season> Seasons { get; set; } = new List<Season>();

    public virtual ICollection<TeamStat> TeamStats { get; set; } = new List<TeamStat>();
}
