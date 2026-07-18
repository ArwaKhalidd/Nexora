using Microsoft.EntityFrameworkCore;
using NexoraAPI.DTOs;
using NexoraAPI.Models;

namespace NexoraAPI.Services
{
    public class StudentProfileService
    {
        private readonly AppDbContext _context;

        public StudentProfileService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetStudentBasicInfo(int studentId)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Id == studentId);
        }

        public async Task<double> GetAverageScore(int studentId)
        {
            var user = await GetStudentBasicInfo(studentId);

            if (user == null)
                return 0;

            var resolvedStudentId = user.StudentId ?? user.Id;

            return await _context.StudentAssessments
                .Where(sa => sa.IdStudent == resolvedStudentId && sa.Score.HasValue)
                .AverageAsync(sa => (double?)sa.Score) ?? 0;
        }

        public async Task<List<string>> GetStudentSkills(int studentId)
        {
            return await _context.StudentSkills
                .Where(s => s.UserId == studentId)
                .Select(s => s.SkillName)
                .ToListAsync();
        }

        public async Task<List<string>> GetWeakSubjects(int studentId)
        {
            var user = await GetStudentBasicInfo(studentId);

            if (user == null)
                return new List<string>();

            var resolvedStudentId = user.StudentId ?? user.Id;

            return await _context.StudentAssessments
                .Where(sa => sa.IdStudent == resolvedStudentId && sa.Score < 60)
                .Join(
                    _context.Assessments,
                    sa => sa.IdAssessment,
                    a => a.IdAssessment,
                    (sa, a) => a.CodeModule
                )
                .Where(codeModule => codeModule != null)
                .Select(codeModule => codeModule!)
                .Distinct()
                .ToListAsync();
        }

        public async Task<StudentProfileDto?> GetStudentProfileAsync(int studentId)
        {
            var user = await GetStudentBasicInfo(studentId);

            if (user == null)
                return null;

            return new StudentProfileDto
            {
                StudentId = studentId,
                StudentName = $"{user.FirstName} {user.LastName}",
                AverageScore = await GetAverageScore(studentId),
                Skills = await GetStudentSkills(studentId),
                WeakSubjects = await GetWeakSubjects(studentId)
            };
        }

        public string GetPerformanceLevel(double averageScore, bool hasAssessments)
        {
            if (!hasAssessments)
                return "New Student";

            if (averageScore >= 85)
                return "Excellent";

            if (averageScore >= 70)
                return "Good";

            if (averageScore >= 50)
                return "Average";

            return "Needs Support";
        }

        public async Task<RecommendationDashboardResponse?> GetDashboardDataAsync(int studentId)
        {
            var profile = await GetStudentProfileAsync(studentId);

            if (profile == null)
                return null;

            var user = await GetStudentBasicInfo(studentId);

            bool hasAssessments = false;

            if (user != null)
            {
                var resolvedStudentId = user.StudentId ?? user.Id;
                hasAssessments = await _context.StudentAssessments
                    .AnyAsync(sa => sa.IdStudent == resolvedStudentId);
            }

            var response = new RecommendationDashboardResponse
            {
                StudentName = profile.StudentName,
                AverageScore = Math.Round(profile.AverageScore, 2),
                Status = GetPerformanceLevel(profile.AverageScore, hasAssessments)
            };

            if (!hasAssessments)
            {
                response.SystemMessage =
                    "Welcome aboard! Here are some courses to kickstart your journey matching your selected interests.";
            }
            else if (profile.AverageScore < 50)
            {
                response.SystemMessage =
                    "We noticed some subjects are challenging for you. Let's review these foundational courses to boost your score.";
            }
            else
            {
                response.SystemMessage =
                    "Great progress! Keep up the good work with these advanced recommendations.";
            }

            response.WeakSubjectsCount = profile.WeakSubjects.Count;

            response.SkillsCount = profile.Skills.Count;

            return response;
        }
    }
}