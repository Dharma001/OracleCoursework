using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("Course")]
    public class Course
    {
        [Key]
        [Column("CourseId")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CourseId { get; set; }

        [Required]
        [StringLength(255)]
        [Column("CourseTitle", TypeName = "VARCHAR2")]
        [Display(Name = "Course Title")]
        public string CourseTitle { get; set; }

        [Required]
        [StringLength(255)]
        [Column("Description", TypeName = "VARCHAR2")]
        public string Description { get; set; }

        [Required]
        [Column("Duration", TypeName = "DATE")]
        [Display(Name = "Duration")]
        public DateTime Duration { get; set; }

        [Required]
        [Column("InstructorId")]
        public int InstructorId { get; set; }

        public virtual Instructor Instructor { get; set; }

        public virtual ICollection<Lesson> Lessons { get; set; }

        public virtual ICollection<Enrollment> Enrollment { get; set; }
    }
}
