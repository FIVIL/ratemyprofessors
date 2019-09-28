using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ratemyprofessors.Models;

namespace ratemyprofessors.Pages.Admin.courseProf
{
    public class CreateModel : PageModel
    {
        private readonly ratemyprofessors.Models.DataBaseContext _context;

        public CreateModel(ratemyprofessors.Models.DataBaseContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CourseID"] = new SelectList(_context.Courses, "ID", "AliasNames");
        ViewData["ProfessorID"] = new SelectList(_context.Professors.OrderBy(x=>x.FullName), "ID", "FullName");
            return Page();
        }

        [BindProperty]
        public ProfCourse ProfCourse { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ProfCourses.Add(ProfCourse);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}