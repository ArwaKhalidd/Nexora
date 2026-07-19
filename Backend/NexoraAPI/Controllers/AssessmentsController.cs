using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NexoraAPI.DTOs;
using NexoraAPI.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NexoraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Ensure only authenticated students can view their assessments
    public class AssessmentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AssessmentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Assessments/student
        [HttpGet("student")]
        public async Task<ActionResult<IEnumerable<StudentAssessmentDto>>> GetStudentAssessments()
        {
            try
            {
                // 1. Get User ID from Token
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int currentUserId))
                {
                    return Unauthorized(new { success = false, message = "لم يتم التعرف على المستخدم، يرجى تسجيل الدخول مجدداً." });
                }

                // 2. Fetch User to resolve StudentId
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == currentUserId);
                if (user == null)
                {
                    return NotFound(new { success = false, message = "المستخدم غير موجود." });
                }

                var studentId = user.StudentId ?? user.Id;

                // 3. Fetch Assessments with Assessment Details (CodeModule, Type)
                var assessments = await _context.StudentAssessments
                    .Include(sa => sa.IdAssessmentNavigation)
                    .Where(sa => sa.IdStudent == studentId)
                    .Select(sa => new StudentAssessmentDto
                    {
                        AssessmentId = sa.IdAssessment ?? 0,
                        CodeModule = sa.IdAssessmentNavigation != null ? sa.IdAssessmentNavigation.CodeModule : "N/A",
                        AssessmentType = sa.IdAssessmentNavigation != null ? sa.IdAssessmentNavigation.AssessmentType : "N/A",
                        Score = sa.Score,
                        DateSubmitted = sa.DateSubmitted,
                        IsBanked = sa.IsBanked == 1
                    })
                    .ToListAsync();

                return Ok(new { success = true, data = assessments });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "حدث خطأ أثناء تحميل بيانات التقييمات", error = ex.Message });
            }
        }
    }
}
