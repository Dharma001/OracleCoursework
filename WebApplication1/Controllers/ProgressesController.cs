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
    public class ProgressesController : Controller
    {
        private readonly applicationContext _context;

        public ProgressesController(applicationContext context)
        {
            _context = context;
        }

        // GET: Progresses
        public async Task<IActionResult> Index()
        {
            var applicationContext = _context.Progress.Include(p => p.Lesson).Include(p => p.Student);
            return View(await applicationContext.ToListAsync());
        }

        // GET: Progresses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var progress = await _context.Progress
                .Include(p => p.Lesson)
                .Include(p => p.Student)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (progress == null)
            {
                return NotFound();
            }

            return View(progress);
        }

        // GET: Progresses/Create
        public IActionResult Create()
        {
            ViewData["LessonNo"] = new SelectList(_context.Lesson, "LessonNo", "LessonNo");
            ViewData["StudentId"] = new SelectList(_context.Student, "StudentId", "FirstName");
            ViewData["CourseId"] = new SelectList(_context.Lesson, "CourseId", "CourseTitle");
            return View();
        }

        // POST: Progresses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,CourseId,LessonNo,LastAccess,LessonStatus")] Progress progress)
        {
            try
            {
                _context.Add(progress);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                ModelState.AddModelError("", "An error occurred while saving the progress.");
                ViewData["LessonNo"] = new SelectList(_context.Lesson, "LessonNo", "LessonNo", progress.LessonNo);
                ViewData["StudentId"] = new SelectList(_context.Student, "StudentId", "FirstName", progress.StudentId);
                ViewData["CourseId"] = new SelectList(_context.Course, "CourseId", "CourseTitle", progress.CourseId);
                return View(progress);
            }
        }

    }
}
