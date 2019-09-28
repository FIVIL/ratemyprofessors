using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ratemyprofessors.Models;

namespace ratemyprofessors.Pages.Admin.profs
{
    public class DetailsModel : PageModel
    {
        private readonly ratemyprofessors.Models.DataBaseContext _context;

        public DetailsModel(ratemyprofessors.Models.DataBaseContext context)
        {
            _context = context;
        }

        public Professor Professor { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Professor = await _context.Professors
                .AsNoTracking()
                .Include(x=>x.ProfCourses)
                    .ThenInclude(y=>y.Course)
                .Include(x=>x.ProfFacs)
                    .ThenInclude(y=>y.Faculty)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Professor == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
