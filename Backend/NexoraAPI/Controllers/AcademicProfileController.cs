using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using NexoraAPI.DTOs.AcademicProfile;
using NexoraAPI.Services.Interfaces;

namespace NexoraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AcademicProfileController : ControllerBase
    {
        private readonly IAcademicProfileService _service;

        public AcademicProfileController(IAcademicProfileService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {
            var userId = int.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var profile = await _service.GetProfileAsync(userId);

            if (profile == null)
                return NotFound();

            return Ok(profile);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProfile(UpdateAcademicProfileDto dto)
        {
            var userId = int.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var updated = await _service.UpdateProfileAsync(userId, dto);

            if (!updated)
                return NotFound();

            return Ok("Profile updated successfully.");
        }
    }
}