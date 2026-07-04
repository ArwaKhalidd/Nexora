using Microsoft.EntityFrameworkCore;
using NexoraAPI.DTOs.AcademicProfile;
using NexoraAPI.Models;
using NexoraAPI.Services.Interfaces;

namespace NexoraAPI.Services.Implementations
{
    public class AcademicProfileService : IAcademicProfileService
    {
        private readonly AppDbContext _context;

        public AcademicProfileService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserProfileDto?> GetProfileAsync(int userId)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return null;

            StudentInfo? student = null;
            if (user.StudentId.HasValue)
            {
                student = await _context.StudentInfos
                    .FirstOrDefaultAsync(s => s.IdStudent == user.StudentId.Value);
            }

            return new UserProfileDto
            {
                // User Details
                StudentId = user.StudentId,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role.ToString(),
                EmailVerified = user.EmailVerified,

                // Academic Details
                Gender = student?.Gender,
                HighestEducation = student?.HighestEducation,
                AgeBand = student?.AgeBand,
                Region = student?.Region,
                ImdBand = student?.ImdBand,
                Disability = student?.Disability,
                StudiedCredits = student?.StudiedCredits,
                NumOfPrevAttempts = student?.NumOfPrevAttempts,
                FinalResult = student?.FinalResult
            };
        }

        public async Task<bool> UpdateProfileAsync(int userId, UpdateAcademicProfileDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null || !user.StudentId.HasValue)
                return false;

            var student = await _context.StudentInfos
                .FirstOrDefaultAsync(s => s.IdStudent == user.StudentId.Value);

            if (student == null)
                return false;

            student.Gender = dto.Gender;
            student.HighestEducation = dto.HighestEducation;
            student.AgeBand = dto.AgeBand;
            student.Region = dto.Region;
            student.ImdBand = dto.ImdBand;
            student.Disability = dto.Disability;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}