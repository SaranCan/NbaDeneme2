using System;
using System.Collections.Generic;

namespace NbaDeneme2.Models;

public partial class Contract
{
    public int ContractId { get; set; }

    public int PersonId { get; set; }

    public int TeamId { get; set; }

    public int SeasonId { get; set; }

    public decimal Salary { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public DateTime LastUpdate { get; set; }

    public virtual Player Person { get; set; } = null!;

    public virtual Season Season { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;
}
