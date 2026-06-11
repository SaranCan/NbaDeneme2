using System;
using System.Collections.Generic;

namespace NbaDeneme2.Models;

public partial class VwTriggerVerification
{
    public string TeamName { get; set; } = null!;

    public int? ManualWins { get; set; }

    public int TriggerWins { get; set; }

    public int? ManualLoses { get; set; }

    public int TriggerLoses { get; set; }

    public string TriggerStatus { get; set; } = null!;
}
