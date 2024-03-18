using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using app.appDbContext;

namespace WebApplication1.Controllers
{
    public class LessonsController : Controller
    {
        private readonly applicationContext _context;

        public LessonsController(applicationContext context)
        {
            _context = context;
        }

        // GET: Lessons
        public async Task<IActionResult> Index()
        {
            var applicationContext = _context.Lesson.Include(l => l.Course);
            return View(await applicationContext.ToListAsync());
        }

        // GET: Lessons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lesson = await _context.Lesson
                .Include(l => l.Course)
                .FirstOrDefaultAsync(m => m.LessonNo == id);
            if (lesson == null)
            {
                return NotFound();
            }

            return View(lesson);
        }

        // GET: Lessons/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Course, "CourseId", "CourseTitle");
            return View();
        }

        // POST: Lessons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        // [ValidateAntiForgeryToken] // Remove or comment out this line
        public async Task<IActionResult> Create([Bind("LessonNo,CourseId,LessonTitle,Duration")] Lesson lesson)
        {
            try
            {
                _context.Add(lesson);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Exception occurred while saving to the database: {ex.Message}");
                throw; // Rethrow the exception to handle it higher up
            }
        }
    }
}
