namespace NexoraAPI.DTOs.Auth
{
    public class LoginDto
    {
        public int StudentId { get; set; }

        public string Password { get; set; } = string.Empty;
    }
}