using System.ComponentModel.DataAnnotations;

namespace NexoraAPI.DTOs.Courses;

public class UpdateCourseDto
{
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(2000)]
    public string Description { get; set; } = string.Empty;

    [Range(1, 10000)]
    public int Hours { get; set; }

    public List<string> Skills { get; set; } = new();
}
