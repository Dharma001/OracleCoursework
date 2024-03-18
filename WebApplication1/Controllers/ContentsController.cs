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
    public class ContentsController : Controller
    {
        private readonly applicationContext _context;

        public ContentsController(applicationContext context)
        {
            _context = context;
        }

        // GET: Contents
        public async Task<IActionResult> Index()
        {
            var applicationContext = _context.Content.Include(c => c.Lesson);
            return View(await applicationContext.ToListAsync());
        }

        // GET: Contents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var content = await _context.Content
                .Include(c => c.Lesson)
                .FirstOrDefaultAsync(m => m.ContentId == id);
            if (content == null)
            {
                return NotFound();
            }

            return View(content);
        }

        // GET: Contents/Create
        public IActionResult Create()
        {
            ViewData["LessonNo"] = new SelectList(_context.Lesson, "LessonNo", "LessonNo");
            ViewData["CourseId"] = new SelectList(_context.Lesson, "CourseId", "CourseId");
            return View();
        }
        // POST: Contents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContentId,CourseId,LessonNo,ContentTitle,ContentType,ContentUrl")] Content content)
        {
            try
            {
                _context.Add(content);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                ModelState.AddModelError("", "An error occurred while saving the content.");
                ViewData["LessonNo"] = new SelectList(_context.Lesson, "LessonNo", "LessonNo", content.LessonNo);
                ViewData["CourseId"] = new SelectList(_context.Course, "CourseId", "CourseTitle", content.CourseId);
                return View(content);
            }
        }


        // GET: Contents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var content = await _context.Content.FindAsync(id);
            if (content == null)
            {
                return NotFound();
            }
            ViewData["LessonNo"] = new SelectList(_context.Lesson, "LessonNo", "LessonNo");
            ViewData["CourseId"] = new SelectList(_context.Lesson, "CourseId", "CourseId");
            return View(content);
        }

        // POST: Contents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContentId,CourseId,LessonNo,ContentTitle,ContentType,ContentUrl")] Content content)
        {
            if (id != content.ContentId)
            {
                return NotFound();
            }

            try
            {
                _context.Update(content);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContentExists(content.ContentId))
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

        // GET: Contents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var content = await _context.Content
                .Include(c => c.Lesson)
                .FirstOrDefaultAsync(m => m.ContentId == id);
            if (content == null)
            {
                return NotFound();
            }

            return View(content);
        }

        // POST: Contents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var content = await _context.Content.FindAsync(id);
            if (content != null)
            {
                _context.Content.Remove(content);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContentExists(int id)
        {
            return _context.Content.Any(e => e.ContentId == id);
        }
    }
}
