using System;
using System.Collections.Generic;

namespace NbaDeneme2.Models;

public partial class TeamStat
{
    public int TeamStatId { get; set; }

    public int TeamId { get; set; }

    public int SeasonId { get; set; }

    public int Wins { get; set; }

    public int Loses { get; set; }

    public int Differential { get; set; }

    public DateTime LastUpdate { get; set; }

    public virtual Season Season { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;
}
