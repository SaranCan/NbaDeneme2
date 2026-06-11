using System;
using System.Collections.Generic;

namespace NbaDeneme2.Models;

public partial class VwCoachingStaff
{
    public string TeamName { get; set; } = null!;

    public string Conference { get; set; } = null!;

    public string Division { get; set; } = null!;

    public string CoachName { get; set; } = null!;

    public string Role { get; set; } = null!;

    public decimal? CoachSalary { get; set; }
}
