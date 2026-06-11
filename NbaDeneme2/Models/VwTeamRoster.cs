using System;
using System.Collections.Generic;

namespace NbaDeneme2.Models;

public partial class VwTeamRoster
{
    public int TeamId { get; set; }

    public string TeamName { get; set; } = null!;

    public string Conference { get; set; } = null!;

    public string PlayerName { get; set; } = null!;

    public string? Position { get; set; }

    public int? JerseyNumber { get; set; }

    public decimal Salary { get; set; }
}
