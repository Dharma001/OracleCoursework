using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("Progress")] // Assuming your table name is "Progress" in the database
    public class Progress
    {
        [Key]
        [Column("StudentId")]
        public int StudentId { get; set; }

        [Key]
        [Column("CourseId")]
        public int CourseId { get; set; }

        [Key]
        [Column("LessonNo")]
        public int LessonNo { get; set; }

        [Column("LastAccess", TypeName = "DATE")]
        public DateTime? LastAccess { get; set; }

        [Column("LessonStatus")]
        public bool LessonStatus { get; set; }

        public virtual Lesson Lesson { get; set; }

        public virtual Student Student { get; set; }
    }
}
