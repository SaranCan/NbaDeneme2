using System;
using System.Collections.Generic;

namespace NbaDeneme2.Models;

public partial class Arena
{
    public int ArenaId { get; set; }

    public string ArenaName { get; set; } = null!;

    public int? Capacity { get; set; }

    public DateTime LastUpdate { get; set; }

    public virtual ICollection<Match> Matches { get; set; } = new List<Match>();

    public virtual ICollection<Team> Teams { get; set; } = new List<Team>();
}
