using app.appDbContext;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly applicationContext _context;

        public HomeController(ILogger<HomeController> logger, applicationContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var courseEnrollments = _context.Enrollment
                .GroupBy(e => e.CourseId)
                .Select(g => new { CourseId = g.Key, EnrollmentCount = g.Count() })
                .ToList();

            var instructorCourses = _context.Course
                .GroupBy(c => c.InstructorId)
                .Select(g => new { InstructorId = g.Key, CourseCount = g.Count() })
                .ToList();

            var courseLessons = _context.Lesson
                .GroupBy(l => l.CourseId)
                .Select(g => new { CourseId = g.Key, LessonCount = g.Count() })
                .ToList();

                ViewBag.InstructorCount = _context.Instructor.Count();
                ViewBag.StudentCount = _context.Student.Count();
                ViewBag.CourseCount = _context.Course.Count();
                ViewBag.EnrollmentCount = _context.Enrollment.Count();

            var chartData = new
            {
                enrollmentLabels = courseEnrollments.Select(e => _context.Course.FirstOrDefault(c => c.CourseId == e.CourseId)?.CourseTitle ?? "Unknown").ToArray(),
                enrollmentData = courseEnrollments.Select(e => e.EnrollmentCount).ToArray(),
                instructorLabels = instructorCourses.Select(i => _context.Instructor.FirstOrDefault(ins => ins.InstructorId == i.InstructorId)?.InstructorName ?? "Unknown").ToArray(),
                instructorData = instructorCourses.Select(i => i.CourseCount).ToArray(),
                lessonLabels = courseLessons.Select(l => _context.Course.FirstOrDefault(c => c.CourseId == l.CourseId)?.CourseTitle ?? "Unknown").ToArray(),
                lessonData = courseLessons.Select(l => l.LessonCount).ToArray()
            };

            ViewData["ChartData"] = chartData;

            return View();
        }




        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
