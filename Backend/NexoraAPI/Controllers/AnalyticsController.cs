using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NexoraAPI.DTOs;
using NexoraAPI.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace NexoraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // حماية الـ Endpoint عشان نعرف مين الطالب من الـ Token
    public class AnalyticsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AnalyticsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Analytics/dashboard-charts
        [HttpGet("dashboard-charts")]
        public async Task<ActionResult<AnalyticsDashboardDto>> GetDashboardChartsData()
        {
            try
            {
                // 1. جلب الـ User ID من الـ Token
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int currentUserId))
                {
                    return Unauthorized(new { success = false, message = "لم يتم التعرف على المستخدم، يرجى تسجيل الدخول مجدداً." });
                }

                // 2. جلب بيانات الـ User عشان نعرف الـ StudentId المرتبط بيه
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Id == currentUserId);

                if (user == null)
                {
                    return NotFound(new { success = false, message = "المستخدم غير موجود." });
                }

                // 3. جلب تقييمات الطالب مع تفاصيل الكورسات والمهارات
                var studentId = user.StudentId ?? user.Id;
                var studentAssessments = await _context.StudentAssessments
                    .Include(sa => sa.IdAssessmentNavigation)
                        .ThenInclude(a => a.Course)
                            .ThenInclude(c => c.CourseSkillTags)
                    .Where(a => a.IdStudent == studentId) 
                    .ToListAsync();

                // 4. حساب تقدم المهارات من درجات التقييمات
                var skillScores = new Dictionary<string, List<double>>();

                foreach (var sa in studentAssessments)
                {
                    if (sa.Score.HasValue && sa.IdAssessmentNavigation?.Course?.CourseSkillTags != null)
                    {
                        foreach (var tag in sa.IdAssessmentNavigation!.Course!.CourseSkillTags)
                        {
                            var skill = tag.SkillName;
                            if (string.IsNullOrWhiteSpace(skill)) continue;

                            if (!skillScores.ContainsKey(skill))
                            {
                                skillScores[skill] = new List<double>();
                            }
                            skillScores[skill].Add(sa.Score.Value);
                        }
                    }
                }

                var skillsProgress = skillScores.Select(kv => new SkillProgressDto
                {
                    SkillName = kv.Key,
                    Percentage = (int)kv.Value.Average()
                }).ToList();

                // بيانات افتراضية لو مفيش مهارات مسجلة (نفس الموجود بالواجهة)
                if (!skillsProgress.Any())
                {
                    skillsProgress = new List<SkillProgressDto>
                    {
                        new() { SkillName = "Python", Percentage = 85 },
                        new() { SkillName = "Css", Percentage = 50 },
                        new() { SkillName = "Html", Percentage = 70 },
                        new() { SkillName = ".Net", Percentage = 90 },
                        new() { SkillName = "Node", Percentage = 15 },
                        new() { SkillName = "MongoDB", Percentage = 45 },
                        new() { SkillName = "Django", Percentage = 70 },
                        new() { SkillName = "Fast api", Percentage = 80 }
                    };
                }

                // 5. تجهيز بيانات الـ Line Chart بناءً على التقييمات
                List<MonthlyProgressDto> monthlyProgress;

                if (studentAssessments.Any())
                {
                    // حساب مجموع الدرجات ومتوسطها لتوزيعها على الشهور (محاكاة)
                    double totalScore = studentAssessments.Sum(a => a.Score ?? 0);
                    int avgScore = (int)(totalScore / studentAssessments.Count);

                    monthlyProgress = new List<MonthlyProgressDto>
                    {
                        new() { Month = "Jan", Progress = (int)(avgScore * 0.4), StudyHours = (int)(totalScore * 0.1) },
                        new() { Month = "Feb", Progress = (int)(avgScore * 0.6), StudyHours = (int)(totalScore * 0.2) },
                        new() { Month = "Mar", Progress = (int)(avgScore * 0.5), StudyHours = (int)(totalScore * 0.3) },
                        new() { Month = "Apr", Progress = (int)(avgScore * 0.7), StudyHours = (int)(totalScore * 0.4) },
                        new() { Month = "May", Progress = (int)(avgScore * 0.8), StudyHours = (int)(totalScore * 0.5) },
                        new() { Month = "Jun", Progress = (int)(avgScore * 0.9), StudyHours = (int)(totalScore * 0.8) },
                        new() { Month = "Jul", Progress = avgScore, StudyHours = (int)totalScore }
                    };
                }
                else
                {
                    // بيانات افتراضية لو الطالب لسه جديد ومظهرش تقييمات
                    monthlyProgress = new List<MonthlyProgressDto>
                    {
                        new() { Month = "Jan", Progress = 40, StudyHours = 100 },
                        new() { Month = "Feb", Progress = 55, StudyHours = 150 },
                        new() { Month = "Mar", Progress = 50, StudyHours = 140 },
                        new() { Month = "Apr", Progress = 65, StudyHours = 180 },
                        new() { Month = "May", Progress = 70, StudyHours = 200 },
                        new() { Month = "Jun", Progress = 85, StudyHours = 250 },
                        new() { Month = "Jul", Progress = 90, StudyHours = 280 }
                    };
                }

                var response = new AnalyticsDashboardDto
                {
                    MonthlyProgress = monthlyProgress,
                    SkillsProgress = skillsProgress
                };

                return Ok(new { success = true, data = response });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "حدث خطأ أثناء تحميل بيانات الإحصائيات", error = ex.Message });
            }
        }
    }
}