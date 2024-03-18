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
    public class QuestionsController : Controller
    {
        private readonly applicationContext _context;

        public QuestionsController(applicationContext context)
        {
            _context = context;
        }

        // GET: Questions
        public async Task<IActionResult> Index()
        {
            var applicationContext = _context.Question.Include(q => q.Lesson).Include(q => q.Student);
            return View(await applicationContext.ToListAsync());
        }

        // GET: Questions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Question
                .Include(q => q.Lesson)
                .Include(q => q.Student)
                .FirstOrDefaultAsync(m => m.QuestionId == id);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        // GET: Questions/Create
        public IActionResult Create()
        {
            ViewData["LessonNo"] = new SelectList(_context.Lesson, "LessonNo", "LessonNo");
            ViewData["CourseId"] = new SelectList(_context.Lesson, "CourseId", "CourseId");
            ViewData["StudentId"] = new SelectList(_context.Student, "StudentId", "StudentId");
            return View();
        }


        // POST: Questions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QuestionId,StudentId,LessonNo,QuestionText,QuestionDate,CourseId")] Question question)
        {
            try
            {
                _context.Add(question);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                ModelState.AddModelError("", "An error occurred while saving the question.");
                ViewData["LessonNo"] = new SelectList(_context.Lesson, "LessonNo", "LessonNo", question.LessonNo);
                ViewData["CourseId"] = new SelectList(_context.Course, "CourseId", "CourseTitle", question.CourseId);
                ViewData["StudentId"] = new SelectList(_context.Student, "StudentId", "FirstName", question.StudentId);
                return View(question);
            }
        }

        // GET: Questions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Question.FindAsync(id); 
            if (question == null)
            {
                return NotFound();
            }
            ViewData["LessonNo"] = new SelectList(_context.Lesson, "LessonNo", "LessonNo", question.LessonNo);
            ViewData["CourseId"] = new SelectList(_context.Course, "CourseId", "CourseTitle", question.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Student, "StudentId", "FirstName", question.StudentId);
            return View(question);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("QuestionId,StudentId,LessonNo,QuestionText,QuestionDate,CourseId")] Question question)
        {
            if (id != question.QuestionId)
            {
                return NotFound();
            }

            try
            {
                _context.Update(question);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionExists(question.QuestionId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction(nameof(Index));
        }


        // GET: Questions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Question
                .Include(q => q.Lesson)
                .Include(q => q.Student)
                .FirstOrDefaultAsync(m => m.QuestionId == id);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var question = await _context.Question.FindAsync(id);
            if (question != null)
            {
                _context.Question.Remove(question);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionExists(int id)
        {
            return _context.Question.Any(e => e.QuestionId == id);
        }
    }
}
