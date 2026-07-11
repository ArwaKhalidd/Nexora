using NexoraAPI.DTOs;

namespace NexoraAPI.Services
{
    public class RecommendationEngineService
    {
        private readonly ResourceService _resourceService;

        public RecommendationEngineService(ResourceService resourceService)
        {
            _resourceService = resourceService;
        }

        public async Task<List<RecommendationDto>> GenerateRecommendations(StudentProfileDto profile)
        {
            var recommendations = new List<RecommendationDto>();

            // ==========================
            // Weak Subjects
            // ==========================
            foreach (var subject in profile.WeakSubjects.Distinct())
            {
                var resources = await _resourceService.GetResourcesBySubject(subject);

                foreach (var resource in resources)
                {
                    recommendations.Add(new RecommendationDto
                    {
                        CourseCode = subject,
                        CourseName = resource.Title,
                        ResourceUrl = resource.ResourceUrl,
                        Type = resource.Type,
                        Priority = 100,
                        Reason = $"Your performance in {subject} needs improvement."
                    });
                }
            }

            // ==========================
            // Skills
            // ==========================
            foreach (var skill in profile.Skills.Distinct())
            {
                var resources = await _resourceService.GetResourcesBySubject(skill);

                foreach (var resource in resources)
                {
                    recommendations.Add(new RecommendationDto
                    {
                        CourseCode = skill,
                        CourseName = resource.Title,
                        ResourceUrl = resource.ResourceUrl,
                        Type = resource.Type,
                        Priority = 70,
                        Reason = $"Recommended because you are interested in {skill}."
                    });
                }
            }

            // ==========================
            // Demo Mode
            // ==========================
            if (!recommendations.Any())
            {
                var resources = await _resourceService.GetAllRecommendations();

                recommendations = resources
                    .Take(5)
                    .Select(r => new RecommendationDto
                    {
                        CourseCode = r.SubjectName,
                        CourseName = r.Title,
                        ResourceUrl = r.ResourceUrl,
                        Type = r.Type,
                        Priority = 50,
                        Reason = "General recommendation for new students."
                    })
                    .ToList();
            }

            // ==========================
            // Remove duplicates
            // ==========================
            recommendations = recommendations
                .GroupBy(r => r.CourseName)
                .Select(g => g.OrderByDescending(x => x.Priority).First())
                .OrderByDescending(r => r.Priority)
                .Take(5)
                .ToList();

            return recommendations;
        }
    }
}