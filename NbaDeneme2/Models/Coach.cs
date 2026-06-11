using System;
using System.Collections.Generic;

namespace NbaDeneme2.Models;

public partial class Coach
{
    public int PersonId { get; set; }

    public int TeamId { get; set; }

    public string Role { get; set; } = null!;

    public decimal? Salary { get; set; }

    public DateTime LastUpdate { get; set; }

    public virtual Human Person { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;
}
