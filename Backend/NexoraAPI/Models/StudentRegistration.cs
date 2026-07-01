using System;
using System.Collections.Generic;

namespace NexoraAPI.Models;

public partial class StudentRegistration
{
    public string? CodeModule { get; set; }

    public string? CodePresentation { get; set; }

    public int? IdStudent { get; set; }

    public int? DateRegistration { get; set; }

    public int? DateUnregistration { get; set; }

    public virtual StudentInfo? StudentInfo { get; set; }
}
