using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ratemyprofessors.Models;

namespace ratemyprofessors.Pages.Admin.courseProf
{
    public class EditModel : PageModel
    {
        private readonly ratemyprofessors.Models.DataBaseContext _context;

        public EditModel(ratemyprofessors.Models.DataBaseContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ProfCourse ProfCourse { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProfCourse = await _context.ProfCourses
                .Include(p => p.Course)
                .Include(p => p.Professor).FirstOrDefaultAsync(m => m.ID == id);

            if (ProfCourse == null)
            {
                return NotFound();
            }
           ViewData["CourseID"] = new SelectList(_context.Courses, "ID", "AliasNames");
           ViewData["ProfessorID"] = new SelectList(_context.Professors.OrderBy(x=>x.FullName), "ID", "FullName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ProfCourse).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfCourseExists(ProfCourse.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ProfCourseExists(Guid id)
        {
            return _context.ProfCourses.Any(e => e.ID == id);
        }
    }
}
