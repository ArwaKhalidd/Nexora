using System.ComponentModel.DataAnnotations;

namespace NexoraAPI.DTOs.Courses;

/// <summary>
/// Data sent by a Tutor when creating a new course.
/// TutorId is NOT included here — it is automatically set from the authenticated user's token.
/// </summary>
public class CreateCourseDto
{
    [Required]
    [MaxLength(45)]
    public string CodeModule { get; set; } = string.Empty;

    [Required]
    [MaxLength(45)]
    public string CodePresentation { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(2000)]
    public string Description { get; set; } = string.Empty;

    [Range(1, 10000)]
    public int Hours { get; set; }

    /// <summary>
    /// Optional list of skill names to tag this course with on creation.
    /// e.g. ["Python", "Machine Learning"]
    /// </summary>
    public List<string> Skills { get; set; } = new();
}
