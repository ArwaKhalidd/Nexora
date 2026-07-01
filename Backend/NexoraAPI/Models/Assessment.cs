using System;
using System.Collections.Generic;

namespace NexoraAPI.Models;

public partial class Assessment
{
    public int IdAssessment { get; set; }

    public string? CodeModule { get; set; }

    public string? CodePresentation { get; set; }

    public string? AssessmentType { get; set; }

    public string? Date { get; set; }

    public virtual Course? Course { get; set; }
}
