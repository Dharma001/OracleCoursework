using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Instructor",
                columns: table => new
                {
                    InstructorId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    InstructorName = table.Column<string>(type: "VARCHAR2(255)", maxLength: 255, nullable: false),
                    EmailAddress = table.Column<string>(type: "VARCHAR2(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructor", x => x.InstructorId);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    FirstName = table.Column<string>(type: "VARCHAR2(255)", maxLength: 255, nullable: false),
                    LastName = table.Column<string>(type: "VARCHAR2(255)", maxLength: 255, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "DATE", nullable: false),
                    Contact = table.Column<string>(type: "VARCHAR2(255)", maxLength: 255, nullable: false),
                    EmailAddress = table.Column<string>(type: "VARCHAR2(255)", maxLength: 255, nullable: false),
                    Country = table.Column<string>(type: "VARCHAR2(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.StudentId);
                });

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    CourseTitle = table.Column<string>(type: "VARCHAR2(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "VARCHAR2(255)", maxLength: 255, nullable: false),
                    Duration = table.Column<DateTime>(type: "DATE", nullable: false),
                    InstructorId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.CourseId);
                    table.ForeignKey(
                        name: "FK_Course_Instructor",
                        column: x => x.InstructorId,
                        principalTable: "Instructor",
                        principalColumn: "InstructorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Enrollment",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CourseId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    EnrollDate = table.Column<DateTime>(type: "DATE", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollment", x => new { x.StudentId, x.CourseId });
                    table.ForeignKey(
                        name: "FK_Enrollment_Course",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enrollment_Student",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lesson",
                columns: table => new
                {
                    LessonNo = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CourseId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    LessonTitle = table.Column<string>(type: "VARCHAR2(255)", maxLength: 255, nullable: false),
                    Duration = table.Column<DateTime>(type: "DATE", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lesson", x => new { x.LessonNo, x.CourseId });
                    table.ForeignKey(
                        name: "FK_Lesson_Course",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Content",
                columns: table => new
                {
                    ContentId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    CourseId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    LessonNo = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ContentTitle = table.Column<string>(type: "VARCHAR2(255)", maxLength: 255, nullable: false),
                    ContentType = table.Column<string>(type: "VARCHAR2(255)", maxLength: 255, nullable: false),
                    ContentUrl = table.Column<string>(type: "VARCHAR2(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Content", x => x.ContentId);
                    table.ForeignKey(
                        name: "FK_Content_Lesson",
                        columns: x => new { x.LessonNo, x.CourseId },
                        principalTable: "Lesson",
                        principalColumns: new[] { "LessonNo", "CourseId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Progress",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CourseId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    LessonNo = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    LastAccess = table.Column<DateTime>(type: "DATE", nullable: true),
                    LessonStatus = table.Column<bool>(type: "NUMBER(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Progress", x => new { x.StudentId, x.CourseId, x.LessonNo });
                    table.ForeignKey(
                        name: "FK_Progress_Lesson",
                        columns: x => new { x.LessonNo, x.CourseId },
                        principalTable: "Lesson",
                        principalColumns: new[] { "LessonNo", "CourseId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Progress_Student",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    QuestionId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    StudentId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    LessonNo = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    QuestionText = table.Column<string>(type: "VARCHAR2(255)", maxLength: 255, nullable: false),
                    QuestionDate = table.Column<DateTime>(type: "DATE", nullable: false),
                    CourseId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.QuestionId);
                    table.ForeignKey(
                        name: "FK_Question_Lesson",
                        columns: x => new { x.LessonNo, x.CourseId },
                        principalTable: "Lesson",
                        principalColumns: new[] { "LessonNo", "CourseId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Question_Student",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Answer",
                columns: table => new
                {
                    AnswerId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    QuestionId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    InstructorId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    AnswerText = table.Column<string>(type: "VARCHAR2(255)", maxLength: 255, nullable: false),
                    AnswerDate = table.Column<DateTime>(type: "DATE", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answer", x => x.AnswerId);
                    table.ForeignKey(
                        name: "FK_Answer_Instructor",
                        column: x => x.InstructorId,
                        principalTable: "Instructor",
                        principalColumn: "InstructorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Answer_Question",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answer_InstructorId",
                table: "Answer",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_Answer_QuestionId",
                table: "Answer",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Content_LessonNo_CourseId",
                table: "Content",
                columns: new[] { "LessonNo", "CourseId" });

            migrationBuilder.CreateIndex(
                name: "IX_Course_InstructorId",
                table: "Course",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollment_CourseId",
                table: "Enrollment",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_CourseId",
                table: "Lesson",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Progress_LessonNo_CourseId",
                table: "Progress",
                columns: new[] { "LessonNo", "CourseId" });

            migrationBuilder.CreateIndex(
                name: "IX_Question_LessonNo_CourseId",
                table: "Question",
                columns: new[] { "LessonNo", "CourseId" });

            migrationBuilder.CreateIndex(
                name: "IX_Question_StudentId",
                table: "Question",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answer");

            migrationBuilder.DropTable(
                name: "Content");

            migrationBuilder.DropTable(
                name: "Enrollment");

            migrationBuilder.DropTable(
                name: "Progress");

            migrationBuilder.DropTable(
                name: "Question");

            migrationBuilder.DropTable(
                name: "Lesson");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "Instructor");
        }
    }
}
