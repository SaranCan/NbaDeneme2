using System;
using System.Collections.Generic;

namespace NbaDeneme2.Models;

public partial class Human
{
    public int PersonId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateOnly? BirthDate { get; set; }

    public DateTime LastUpdate { get; set; }

    public virtual Coach? Coach { get; set; }

    public virtual Player? Player { get; set; }
}
