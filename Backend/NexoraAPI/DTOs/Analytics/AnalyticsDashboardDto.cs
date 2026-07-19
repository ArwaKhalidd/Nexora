using System.Collections.Generic;

namespace NexoraAPI.DTOs
{
    public class AnalyticsDashboardDto
    {
        public List<MonthlyProgressDto> MonthlyProgress { get; set; } = new();
        public List<SkillProgressDto> SkillsProgress { get; set; } = new();
    }
}
