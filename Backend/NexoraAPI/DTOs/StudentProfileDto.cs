namespace NexoraAPI.DTOs
{
    public class StudentProfileDto
    {
        public int StudentId { get; set; }

        public string StudentName { get; set; } = string.Empty;

        public double AverageScore { get; set; }

        public string PerformanceLevel { get; set; } = string.Empty;

        public int TotalClicks { get; set; }

        public string EngagementLevel { get; set; } = string.Empty;

        public List<string> Skills { get; set; } = new();

        public List<string> WeakSubjects { get; set; } = new();
    }
}