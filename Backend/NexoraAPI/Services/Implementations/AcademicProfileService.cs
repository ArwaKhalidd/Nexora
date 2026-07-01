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

        public async Task<AcademicProfileDto?> GetProfileAsync(int studentId)
        {
            var student = await _context.StudentInfos
                .FirstOrDefaultAsync(s => s.IdStudent == studentId);

            if (student == null)
                return null;

            return new AcademicProfileDto
            {
                StudentId = student.IdStudent,
                Gender = student.Gender,
                HighestEducation = student.HighestEducation,
                AgeBand = student.AgeBand,
                Region = student.Region,
                ImdBand = student.ImdBand,
                Disability = student.Disability,
                StudiedCredits = student.StudiedCredits,
                NumOfPrevAttempts = student.NumOfPrevAttempts,
                FinalResult = student.FinalResult
            };
        }

        public async Task<bool> UpdateProfileAsync(int studentId, UpdateAcademicProfileDto dto)
        {
            var student = await _context.StudentInfos
                .FirstOrDefaultAsync(s => s.IdStudent == studentId);

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