using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("Student")]
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("StudentId")]
        public int StudentId { get; set; }

        [Required]
        [StringLength(255)]
        [Column("FirstName", TypeName = "VARCHAR2")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(255)]
        [Column("LastName", TypeName = "VARCHAR2")]
        public string LastName { get; set; }

        [Required]
        [Column("DateOfBirth", TypeName = "DATE")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(255)]
        [Column("Contact", TypeName = "VARCHAR2")]
        public string Contact { get; set; }

        [StringLength(255)]
        [Column("EmailAddress", TypeName = "VARCHAR2")]
        public string EmailAddress { get; set; }

        [StringLength(255)]
        [Column("Country", TypeName = "VARCHAR2")]
        public string Country { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<Enrollment> Enrollment { get; set; }


    }
}
