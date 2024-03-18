using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class InstructorCourse
    {
        // Primary key
        public int InstructorCourseId { get; set; }

        // Foreign keys
        public int InstructorForeignKey { get; set; } // Different name for Instructor foreign key
        public int CourseForeignKey { get; set; } // Different name for Course foreign key

        // Navigation properties
        public Instructor Instructor { get; set; }
        public Course Course { get; set; }
    }
}
