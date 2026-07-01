namespace NexoraAPI.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(int studentId);
    }
}