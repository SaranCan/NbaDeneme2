using System;
using System.Collections.Generic;

namespace NbaDeneme2.Models;

public partial class VwWestStanding
{
    public long? ConferenceRank { get; set; }

    public string TeamName { get; set; } = null!;

    public string Division { get; set; } = null!;

    public int Wins { get; set; }

    public int Loses { get; set; }

    public int Differential { get; set; }
}
