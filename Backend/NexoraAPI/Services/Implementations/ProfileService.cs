using Microsoft.EntityFrameworkCore;
using NexoraAPI.DTOs.Profile;
using NexoraAPI.Helpers;
using NexoraAPI.Models;
using NexoraAPI.Services.Interfaces;

namespace NexoraAPI.Services.Implementations
{
    public class ProfileService : IProfileService
    {
        private readonly AppDbContext _context;

        public ProfileService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ProfileDto?> GetProfileAsync(int userId)
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

            return new ProfileDto
            {
                // User Details
                StudentId = user.StudentId,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role.ToString(),
                EmailVerified = user.EmailVerified,
                CreatedAt = user.CreatedAt,
                LastLogin = user.LastLogin,

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

        public async Task<bool> UpdateProfileAsync(int userId, UpdateProfileDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return false;

            // Update User fields
            if (!string.IsNullOrWhiteSpace(dto.FirstName))
                user.FirstName = dto.FirstName;

            if (!string.IsNullOrWhiteSpace(dto.LastName))
                user.LastName = dto.LastName;

            if (!string.IsNullOrWhiteSpace(dto.Email))
            {
                var exists = await _context.Users.AnyAsync(x =>
                    x.Email == dto.Email && x.Id != userId);

                if (exists)
                    return false;

                user.Email = dto.Email;
            }

            StudentInfo? student = null;

            if (user.StudentId.HasValue)
            {
                student = await _context.StudentInfos
                    .FirstOrDefaultAsync(s => s.IdStudent == user.StudentId.Value);
            }

            if (student == null)
            {
                student = new StudentInfo
                {
                    IdStudent = user.Id
                };

                _context.StudentInfos.Add(student);

                user.StudentId = student.IdStudent;
            }

            student.Gender = dto.Gender;
            student.HighestEducation = dto.HighestEducation;
            student.AgeBand = dto.AgeBand;
            student.Region = dto.Region;
            student.ImdBand = dto.ImdBand;
            student.Disability = dto.Disability;
            student.StudiedCredits = dto.StudiedCredits;
            student.NumOfPrevAttempts = dto.NumOfPrevAttempts;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ChangePasswordAsync(int userId, ChangePasswordDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
                return false;

            var validPassword = PasswordHasher.Verify(dto.CurrentPassword, user.PasswordHash);
            if (!validPassword)
                return false;

            // Check new password strength
            if (string.IsNullOrWhiteSpace(dto.NewPassword) || !System.Text.RegularExpressions.Regex.IsMatch(dto.NewPassword, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$"))
                return false;

            user.PasswordHash = PasswordHasher.Hash(dto.NewPassword);
            await _context.SaveChangesAsync();

            return true;
        }

    }
}