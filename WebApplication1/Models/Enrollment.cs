using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("Enrollment")]
    public class Enrollment
    {
        [Key]
        [Column("StudentId")]
        public int StudentId { get; set; }

        [Key]
        [Column("CourseId")]
        public int CourseId { get; set; }

        // Navigation properties
        public virtual Student Student { get; set; }
        public virtual Course Course { get; set; }

        [Column("EnrollDate", TypeName = "DATE")]
        public DateTime EnrollDate { get; set; }
    }
}
