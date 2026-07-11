namespace NexoraAPI.DTOs
{
    public class RecommendationDto
    {
        public string CourseCode { get; set; } = string.Empty;

        public string CourseName { get; set; } = string.Empty;

        public string Reason { get; set; } = string.Empty;

        public string ResourceUrl { get; set; } = string.Empty;

        public int Priority { get; set; }

        public string Type { get; set; } = string.Empty;
    }
}