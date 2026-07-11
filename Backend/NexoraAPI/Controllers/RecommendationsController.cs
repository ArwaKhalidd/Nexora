using Microsoft.AspNetCore.Mvc;
using NexoraAPI.DTOs;
using NexoraAPI.Services;

namespace NexoraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecommendationsController : ControllerBase
    {
        private readonly ResourceService _resourceService;
        private readonly StudentProfileService _studentProfileService;
        private readonly RecommendationEngineService _recommendationEngine;

        public RecommendationsController(
            StudentProfileService studentProfileService,
            RecommendationEngineService recommendationEngine,
            ResourceService resourceService)
        {
            _studentProfileService = studentProfileService;
            _recommendationEngine = recommendationEngine;
            _resourceService = resourceService;
        }

        // ============================================================
        // Returns all learning resources in the system.
        // Used for the Resources page.
        // ============================================================
        [HttpGet("resources")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetResources()
        {
            var resources = await _resourceService.GetAllRecommendations();
            return Ok(resources);
        }

        // ============================================================
        // Returns dashboard information for a student.
        // Used when opening the Recommendation page.
        // ============================================================
        [HttpGet("dashboard/{studentId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDashboard(int studentId)
        {
            var dashboard = await _studentProfileService.GetDashboardDataAsync(studentId);

            if (dashboard == null)
            {
                return NotFound(new ApiErrorDto
                {
                    Message = "Student profile not found."
                });
            }

            return Ok(dashboard);
        }

        // ============================================================
        // Returns personalized recommendations.
        // ============================================================
        [HttpGet("student/{studentId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetStudentRecommendations(int studentId)
        {
            var profile = await _studentProfileService.GetStudentProfileAsync(studentId);

            if (profile == null)
            {
                return NotFound(new ApiErrorDto
                {
                    Message = "Student not found."
                });
            }

            var recommendations =
                await _recommendationEngine.GenerateRecommendations(profile);

            var response = new RecommendationResponseDto
            {
                StudentId = studentId,
                StudentName = profile.StudentName,
                RecommendationCount = recommendations.Count,
                GeneratedAt = DateTime.Now,
                Recommendations = recommendations
            };

            return Ok(response);
        }
    }
}