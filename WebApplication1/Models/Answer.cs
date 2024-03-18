using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("Answer")]
    public class Answer
    {
        [Key]
        [Column("AnswerId")]
        public int AnswerId { get; set; }

        [Required]
        [Column("QuestionId")]
        public int QuestionId { get; set; }

        [Required]
        [Column("InstructorId")]
        public int InstructorId { get; set; }

        [Column("AnswerText", TypeName = "VARCHAR2")]
        [StringLength(255)]
        public string AnswerText { get; set; }

        [Required]
        [Column("AnswerDate", TypeName = "DATE")]
        public DateTime AnswerDate { get; set; }

        public virtual Question Question { get; set; }
        public virtual Instructor Instructor { get; set; }
    }
}
