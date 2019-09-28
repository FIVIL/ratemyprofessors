using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ratemyprofessors.Models;

namespace ratemyprofessors.Pages.Admin.courseProf
{
    public class DetailsModel : PageModel
    {
        private readonly ratemyprofessors.Models.DataBaseContext _context;

        public DetailsModel(ratemyprofessors.Models.DataBaseContext context)
        {
            _context = context;
        }

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
            return Page();
        }
    }
}
