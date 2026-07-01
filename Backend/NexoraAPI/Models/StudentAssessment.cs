using System;
using System.Collections.Generic;

namespace NexoraAPI.Models;

public partial class StudentAssessment
{
    public int? IdStudent { get; set; }

    public int? IdAssessment { get; set; }

    public int? DateSubmitted { get; set; }

    public byte? IsBanked { get; set; }

    public double? Score { get; set; }

    public virtual Assessment? IdAssessmentNavigation { get; set; }
}
