namespace NexoraAPI.DTOs
{
    public class RecommendationResponseDto
    {
        public int StudentId { get; set; }

        public string StudentName { get; set; } = string.Empty;

        public int RecommendationCount { get; set; }

        public DateTime GeneratedAt { get; set; }

        public List<RecommendationDto> Recommendations { get; set; } = new();
    }
}