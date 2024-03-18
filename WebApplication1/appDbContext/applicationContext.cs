using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace app.appDbContext
{
    public class applicationContext : DbContext
    {
        public applicationContext(DbContextOptions<applicationContext> options)
            : base(options)
        {
        }

        public DbSet<WebApplication1.Models.Student> Student { get; set; } = default!;
        public DbSet<WebApplication1.Models.Instructor> Instructor { get; set; } = default!;

        public DbSet<WebApplication1.Models.Course> Course { get; set; } = default!;
        public DbSet<WebApplication1.Models.Lesson> Lesson { get; set; } = default!;
        public DbSet<WebApplication1.Models.Enrollment> Enrollment { get; set; } = default!;
        public DbSet<WebApplication1.Models.Question> Question { get; set; } = default!;
        public DbSet<WebApplication1.Models.Content> Content { get; set; } = default!;
        public DbSet<WebApplication1.Models.Answer> Answer { get; set; } = default!;
        public DbSet<WebApplication1.Models.Progress> Progress { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define composite primary keys
            modelBuilder.Entity<WebApplication1.Models.Lesson>()
                .HasKey(l => new { l.LessonNo, l.CourseId });

            modelBuilder.Entity<WebApplication1.Models.Enrollment>()
                .HasKey(e => new { e.StudentId, e.CourseId });

            // Define foreign key constraints with custom names for Lesson entity
            modelBuilder.Entity<WebApplication1.Models.Lesson>()
                .HasOne(l => l.Course)
                .WithMany(c => c.Lessons)
                .HasForeignKey(l => l.CourseId)
                .HasConstraintName("FK_Lesson_Course")
                .OnDelete(DeleteBehavior.Cascade);

            // Define foreign key constraints with custom names for Enrollment entity
            modelBuilder.Entity<WebApplication1.Models.Enrollment>()
                .HasOne(e => e.Student)
                .WithMany(s => s.Enrollment)
                .HasForeignKey(e => e.StudentId)
                .HasConstraintName("FK_Enrollment_Student")
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WebApplication1.Models.Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollment)
                .HasForeignKey(e => e.CourseId)
                .HasConstraintName("FK_Enrollment_Course")
                .OnDelete(DeleteBehavior.Cascade);

            // Define foreign key constraints with custom names for Progress entity
            modelBuilder.Entity<WebApplication1.Models.Progress>()
                .HasKey(p => new { p.StudentId, p.CourseId, p.LessonNo });

            modelBuilder.Entity<WebApplication1.Models.Progress>()
                .HasOne(p => p.Lesson)
                .WithMany()
                .HasForeignKey(p => new { p.LessonNo, p.CourseId })
                .HasConstraintName("FK_Progress_Lesson")
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WebApplication1.Models.Progress>()
                .HasOne(p => p.Student)
                .WithMany()
                .HasForeignKey(p => p.StudentId)
                .HasConstraintName("FK_Progress_Student")
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Answer>()
               .HasKey(a => a.AnswerId);

            modelBuilder.Entity<Answer>()
                .HasOne(a => a.Question)
                .WithMany(q => q.Answer)
                .HasForeignKey(a => a.QuestionId)
                .HasConstraintName("FK_Answer_Question")
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Answer>()
                .HasOne(a => a.Instructor)
                .WithMany()
                .HasForeignKey(a => a.InstructorId)
                .HasConstraintName("FK_Answer_Instructor")
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Course>()
    .HasKey(c => c.CourseId);

            // Define foreign key constraint name using Fluent API
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Instructor)
                .WithMany(i => i.Course)
                .HasForeignKey(c => c.InstructorId)
                .HasConstraintName("FK_Course_Instructor")
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Content>()
    .HasKey(c => c.ContentId);

            // Define foreign key constraint name using Fluent API
            modelBuilder.Entity<Content>()
                .HasOne(c => c.Lesson)
                .WithMany(l => l.Content)
                .HasForeignKey(c => new { c.LessonNo, c.CourseId })
                .HasConstraintName("FK_Content_Lesson")
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Question>()
                .HasKey(q => q.QuestionId);

            modelBuilder.Entity<Question>()
                .HasOne(q => q.Student)
                .WithMany(s => s.Questions)
                .HasForeignKey(q => q.StudentId)
                .HasConstraintName("FK_Question_Student")
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Question>()
                .HasOne(q => q.Lesson)
                .WithMany(l => l.Questions)
                .HasForeignKey(q => new { q.LessonNo, q.CourseId })
                .HasConstraintName("FK_Question_Lesson")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
