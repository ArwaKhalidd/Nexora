using System;
using System.Collections.Generic;

namespace NexoraAPI.Models;

public partial class StudentInfo
{
    public string CodeModule { get; set; } = null!;

    public string CodePresentation { get; set; } = null!;

    public int IdStudent { get; set; }

    public string? Gender { get; set; }

    public string? ImdBand { get; set; }

    public string? HighestEducation { get; set; }

    public string? AgeBand { get; set; }

    public int? NumOfPrevAttempts { get; set; }

    public int? StudiedCredits { get; set; }

    public string? Region { get; set; }

    public string? Disability { get; set; }

    public string? FinalResult { get; set; }

    public virtual Course Course { get; set; } = null!;
}
