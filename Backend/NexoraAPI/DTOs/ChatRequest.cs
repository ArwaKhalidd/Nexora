namespace NexoraAPI.DTOs
{
    public class ChatRequest
    {
        public int StudentId { get; set; }

        public string Message { get; set; } = string.Empty;
    }
}