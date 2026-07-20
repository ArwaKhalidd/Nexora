using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NexoraAPI.Models
{
    public class QuestionOption
    {
        [Key]
        public int Id { get; set; }

        public int QuestionId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Text { get; set; } = string.Empty;

        public bool IsCorrect { get; set; } = false;

        [ForeignKey("QuestionId")]
        public virtual AssessmentQuestion? Question { get; set; }
    }
}
