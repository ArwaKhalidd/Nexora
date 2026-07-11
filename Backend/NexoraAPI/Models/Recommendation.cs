using System;
using System.ComponentModel.DataAnnotations;

namespace NexoraAPI.Models
{
    public class Recommendation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } // اسم الكورس أو الفيديو

        public string Description { get; set; } // وصف مبسط للمصدر

        [Required]
        public string ResourceUrl { get; set; } // لينك الكورس الحقيقي

        public string Type { get; set; } // نوعه (Course, Video, Article)

        public string SubjectName { get; set; } // المادة المستهدفة (مثلاً Data Structures, OOP)
    }
}