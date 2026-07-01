using NexoraAPI.DTOs.Auth;

namespace NexoraAPI.Services.Interfaces
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(RegisterDto dto);

        Task<LoginResponseDto?> LoginAsync(LoginDto dto);
    }
}