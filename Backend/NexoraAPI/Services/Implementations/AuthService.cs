using Microsoft.EntityFrameworkCore;
using NexoraAPI.DTOs.Auth;
using NexoraAPI.Helpers;
using NexoraAPI.Models;
using NexoraAPI.Services.Interfaces;

namespace NexoraAPI.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IJwtService _jwtService;

        public AuthService(AppDbContext context, IJwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        public async Task<bool> RegisterAsync(RegisterDto dto)
        {
            // التأكد أن الطالب موجود في StudentInfo
            var studentExists = await _context.StudentInfos
                .AnyAsync(s => s.IdStudent == dto.StudentId);

            if (!studentExists)
                return false;

            // التأكد أنه لم يسجل قبل كده
            var userExists = await _context.Users
                .AnyAsync(u => u.StudentId == dto.StudentId);

            if (userExists)
                return false;

            var user = new User
            {
                StudentId = dto.StudentId,
                PasswordHash = PasswordHasher.Hash(dto.Password)
            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<LoginResponseDto?> LoginAsync(LoginDto dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.StudentId == dto.StudentId);

            if (user == null)
                return null;

            var validPassword =
                PasswordHasher.Verify(dto.Password, user.PasswordHash);

            if (!validPassword)
                return null;

            var token = _jwtService.GenerateToken(user.StudentId);

            return new LoginResponseDto
            {
                Token = token
            };
        }
    }
}