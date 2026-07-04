namespace NexoraAPI.DTOs.Auth
{
    public class RegisterDto
    {
        public int? StudentId { get; set; }

        public string Password { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public Enums.UserRole Role { get; set; } = Enums.UserRole.Student;
    }
}