using System;
using System.Collections.Generic;

namespace NbaDeneme2.Models;

public partial class Player
{
    public int PersonId { get; set; }

    public int TeamId { get; set; }

    public string? Position { get; set; }

    public double? Height { get; set; }

    public double? Weight { get; set; }

    public int? JerseyNumber { get; set; }

    public DateTime LastUpdate { get; set; }

    public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();

    public virtual Human Person { get; set; } = null!;

    public virtual ICollection<PlayerGameStat> PlayerGameStats { get; set; } = new List<PlayerGameStat>();

    public virtual Team Team { get; set; } = null!;
}
