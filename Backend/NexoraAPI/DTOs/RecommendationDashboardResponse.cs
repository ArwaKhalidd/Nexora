namespace NexoraAPI.DTOs
{
    public class RecommendationDashboardResponse
    {
        public string StudentName { get; set; } = string.Empty;

        public double AverageScore { get; set; }

        public string Status { get; set; } = string.Empty;

        public string SystemMessage { get; set; } = string.Empty;

        public int WeakSubjectsCount { get; set; }

        public int SkillsCount { get; set; }
    }
}