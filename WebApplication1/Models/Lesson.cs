using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("Lesson")]
    public class Lesson
    {
        [Key]
        [Column("LessonNo")]
        public int LessonNo { get; set; }

        [Column("CourseId")]
        public int CourseId { get; set; }

        [ForeignKey("CourseId")]
        public Course Course { get; set; }

        [Column("LessonTitle", TypeName = "VARCHAR2")]
        [StringLength(255)]
        public string LessonTitle { get; set; }

        [Column("Duration", TypeName = "DATE")]
        public DateTime Duration { get; set; }

        public virtual ICollection<Question> Questions { get; set; }

        public virtual ICollection<Content> Content { get; set; }
    }
}
