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
    public class EnrollmentsController : Controller
    {
        private readonly applicationContext _context;

        public EnrollmentsController(applicationContext context)
        {
            _context = context;
        }

        // GET: Enrollments
        public async Task<IActionResult> Index()
        {
            var applicationContext = _context.Enrollment.Include(e => e.Course).Include(e => e.Student);
            return View(await applicationContext.ToListAsync());
        }

        // GET: Enrollments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollment
                .Include(e => e.Course)
                .Include(e => e.Student)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // GET: Enrollments/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Course, "CourseId", "CourseTitle");
            ViewData["StudentId"] = new SelectList(_context.Student, "StudentId", "FirstName");
            return View();
        }

        // POST: Enrollments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,CourseId,EnrollDate")] Enrollment enrollment)
        {
            try
            {
                _context.Add(enrollment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                ModelState.AddModelError("", "An error occurred while saving the enrollment.");
                ViewData["CourseId"] = new SelectList(_context.Course, "CourseId", "CourseTitle", enrollment.CourseId);
                ViewData["StudentId"] = new SelectList(_context.Student, "StudentId", "FirstName", enrollment.StudentId);
                return View(enrollment);
            }
        }

    }
}
