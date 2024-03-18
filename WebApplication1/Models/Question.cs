using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("Question")]
    public class Question
    {
        [Key]
        [Column("QuestionId")]
        public int QuestionId { get; set; }

        [Required]
        [Column("StudentId")]
        public int StudentId { get; set; }

        [Required]
        [Column("LessonNo")]
        public int LessonNo { get; set; }

        [Required]
        [Column("QuestionText", TypeName = "VARCHAR2")]
        [StringLength(255)]
        public string QuestionText { get; set; }

        [Required]
        [Column("QuestionDate", TypeName = "DATE")]
        public DateTime QuestionDate { get; set; }

        [Required]
        [Column("CourseId")]
        public int CourseId { get; set; }

        [ForeignKey("StudentId")]
        public virtual Student Student { get; set; }

        [ForeignKey("LessonNo,CourseId")]
        public virtual Lesson Lesson { get; set; }

        public virtual ICollection<Answer> Answer { get; set; }
    }
}
