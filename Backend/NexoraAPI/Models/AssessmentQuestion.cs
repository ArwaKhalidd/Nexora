using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NexoraAPI.Models
{
    public class AssessmentQuestion
    {
        [Key]
        public int Id { get; set; }

        public int AssessmentId { get; set; }

        [Required]
        [MaxLength(500)]
        public string Text { get; set; } = string.Empty;

        [MaxLength(50)]
        public string QuestionType { get; set; } = "MultipleChoice";

        public int Points { get; set; } = 1;

        // For text-based or simple direct matching
        public string? CorrectAnswer { get; set; }

        [ForeignKey("AssessmentId")]
        public virtual Assessment? Assessment { get; set; }

        public virtual ICollection<QuestionOption> Options { get; set; } = new List<QuestionOption>();
    }
}
