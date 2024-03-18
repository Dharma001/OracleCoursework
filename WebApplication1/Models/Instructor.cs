using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("Instructor")]
    public class Instructor
    {
        [Key]
        [Column("InstructorId")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InstructorId { get; set; }

        [Column("InstructorName", TypeName = "VARCHAR2")]
        [StringLength(255)]
        public string InstructorName { get; set; }

        [Column("EmailAddress", TypeName = "VARCHAR2")]
        [StringLength(255)]
        public string EmailAddress { get; set; }

        public virtual ICollection<Course> Course { get; set; }
    }
}
