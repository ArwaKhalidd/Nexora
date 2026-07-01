namespace NexoraAPI.DTOs.Auth
{
    public class RegisterDto
    {
        public int StudentId { get; set; }

        public string Password { get; set; } = string.Empty;
    }
}