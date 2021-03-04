using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Online_Courses_MVC.Data;
using Online_Courses_MVC.Models;

namespace Online_Courses_MVC.Controllers
{
    public class EnrollmentsController : Controller
    {
        private readonly Online_Courses_DBContext _context;

        public EnrollmentsController(Online_Courses_DBContext context)
        {
            _context = context;
        }
        [Authorize]
        // GET: Enrollments
        public async Task<IActionResult> Index()
        {
            var online_Courses_DBContext = _context.Enrollment.Include(e => e.Course).Include(e => e.Instructor).Include(e => e.Student);
            return View(await online_Courses_DBContext.ToListAsync());
        }
        [Authorize]
        // GET: Enrollments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollment
                .Include(e => e.Course)
                .Include(e => e.Instructor)
                .Include(e => e.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }
        [Authorize]
        // GET: Enrollments/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Id");
            ViewData["InstructorId"] = new SelectList(_context.Set<Instructor>(), "Id", "Id");
            ViewData["StudentId"] = new SelectList(_context.Set<Student>(), "Id", "Id");
            return View();
        }

        // POST: Enrollments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CourseId,InstructorId,StudentId")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enrollment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Id", enrollment.CourseId);
            ViewData["InstructorId"] = new SelectList(_context.Set<Instructor>(), "Id", "Id", enrollment.InstructorId);
            ViewData["StudentId"] = new SelectList(_context.Set<Student>(), "Id", "Id", enrollment.StudentId);
            return View(enrollment);
        }
        [Authorize]
        // GET: Enrollments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollment.FindAsync(id);
            if (enrollment == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Id", enrollment.CourseId);
            ViewData["InstructorId"] = new SelectList(_context.Set<Instructor>(), "Id", "Id", enrollment.InstructorId);
            ViewData["StudentId"] = new SelectList(_context.Set<Student>(), "Id", "Id", enrollment.StudentId);
            return View(enrollment);
        }

        // POST: Enrollments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CourseId,InstructorId,StudentId")] Enrollment enrollment)
        {
            if (id != enrollment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enrollment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnrollmentExists(enrollment.Id))
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
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Id", enrollment.CourseId);
            ViewData["InstructorId"] = new SelectList(_context.Set<Instructor>(), "Id", "Id", enrollment.InstructorId);
            ViewData["StudentId"] = new SelectList(_context.Set<Student>(), "Id", "Id", enrollment.StudentId);
            return View(enrollment);
        }
        [Authorize]
        // GET: Enrollments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollment
                .Include(e => e.Course)
                .Include(e => e.Instructor)
                .Include(e => e.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // POST: Enrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enrollment = await _context.Enrollment.FindAsync(id);
            _context.Enrollment.Remove(enrollment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnrollmentExists(int id)
        {
            return _context.Enrollment.Any(e => e.Id == id);
        }
    }
}
