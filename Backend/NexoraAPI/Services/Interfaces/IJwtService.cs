using NexoraAPI.Models;

namespace NexoraAPI.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}