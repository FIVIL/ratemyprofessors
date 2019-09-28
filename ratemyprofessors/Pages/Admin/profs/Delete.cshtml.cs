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
    public class DeleteModel : PageModel
    {
        private readonly ratemyprofessors.Models.DataBaseContext _context;

        public DeleteModel(ratemyprofessors.Models.DataBaseContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Professor Professor { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Professor = await _context.Professors.FirstOrDefaultAsync(m => m.ID == id);

            if (Professor == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Professor = await _context.Professors.FindAsync(id);

            if (Professor != null)
            {
                _context.Professors.Remove(Professor);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
