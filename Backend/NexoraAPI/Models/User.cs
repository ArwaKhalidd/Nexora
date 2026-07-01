using System;
using System.Collections.Generic;

namespace NexoraAPI.Models;

public partial class User
{
    public int Id { get; set; }

    public int StudentId { get; set; }

    public string PasswordHash { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }
}
