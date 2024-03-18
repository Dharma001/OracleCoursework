using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("Content")]
    public class Content
    {
        [Key]
        [Column("ContentId")]
        public int ContentId { get; set; }

        [Required]
        [Column("CourseId")]
        public int CourseId { get; set; }

        [Required]
        [Column("LessonNo")]
        public int LessonNo { get; set; }

        [Column("ContentTitle", TypeName = "VARCHAR2")]
        [StringLength(255)]
        public string ContentTitle { get; set; }

        [Column("ContentType", TypeName = "VARCHAR2")]
        [StringLength(255)]
        public string ContentType { get; set; }

        [Column("ContentUrl", TypeName = "VARCHAR2")]
        [StringLength(255)]
        public string ContentUrl { get; set; }

        // Navigation property
        [ForeignKey("LessonNo,CourseId")]
        public virtual Lesson Lesson { get; set; }

    }
}
