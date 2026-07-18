using System;
using System.ComponentModel.DataAnnotations;

namespace NexoraAPI.Models
{
    public class Recommendation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty; // اسم الكورس أو الفيديو

        public string Description { get; set; } = string.Empty; // وصف مبسط للمصدر

        [Required]
        public string ResourceUrl { get; set; } = string.Empty; // لينك الكورس الحقيقي

        public string Type { get; set; } = string.Empty; // نوعه (Course, Video, Article)

        public string SubjectName { get; set; } = string.Empty; // المادة المستهدفة (مثلاً Data Structures, OOP)
    }
}