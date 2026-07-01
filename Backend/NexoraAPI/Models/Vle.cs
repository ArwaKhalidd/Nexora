using System;
using System.Collections.Generic;

namespace NexoraAPI.Models;

public partial class Vle
{
    public int IdSite { get; set; }

    public string? CodeModule { get; set; }

    public string? CodePresentation { get; set; }

    public string? ActivityType { get; set; }

    public string? WeekFrom { get; set; }

    public string? WeekTo { get; set; }

    public virtual Course? Course { get; set; }
}
