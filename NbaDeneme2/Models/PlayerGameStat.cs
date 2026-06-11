using System;
using System.Collections.Generic;

namespace NbaDeneme2.Models;

public partial class PlayerGameStat
{
    public int StatsId { get; set; }

    public int PersonId { get; set; }

    public int MatchId { get; set; }

    public int Points { get; set; }

    public int Asists { get; set; }

    public int Rebounds { get; set; }

    public int Steals { get; set; }

    public int Blocks { get; set; }

    public int? MinutesPlayed { get; set; }

    public DateTime LastUpdate { get; set; }

    public virtual Match Match { get; set; } = null!;

    public virtual Player Person { get; set; } = null!;
}
