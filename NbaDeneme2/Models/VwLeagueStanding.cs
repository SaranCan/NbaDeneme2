using System;
using System.Collections.Generic;

namespace NbaDeneme2.Models;

public partial class VwLeagueStanding
{
    public long? Rank { get; set; }

    public string TeamName { get; set; } = null!;

    public string Conference { get; set; } = null!;

    public string Division { get; set; } = null!;

    public int Wins { get; set; }

    public int Loses { get; set; }

    public int Differential { get; set; }
}
