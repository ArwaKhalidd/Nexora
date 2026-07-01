using System;
using System.Collections.Generic;

namespace NexoraAPI.Models;

public partial class StudentVle
{
    public string? CodeModule { get; set; }

    public string? CodePresentation { get; set; }

    public int? IdStudent { get; set; }

    public int? IdSite { get; set; }

    public int? Date { get; set; }

    public int? SumClick { get; set; }

    public virtual Vle? IdSiteNavigation { get; set; }

    public virtual StudentInfo? StudentInfo { get; set; }
}
