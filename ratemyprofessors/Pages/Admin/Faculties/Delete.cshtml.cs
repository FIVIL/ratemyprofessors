using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ratemyprofessors.Models;

namespace ratemyprofessors.Pages.Admin.Faculties
{
    public class DeleteModel : PageModel
    {
        private readonly ratemyprofessors.Models.DataBaseContext _context;

        public DeleteModel(ratemyprofessors.Models.DataBaseContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Faculty Faculty { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Faculty = await _context.Faculties
                .Include(f => f.University).FirstOrDefaultAsync(m => m.ID == id);

            if (Faculty == null)
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

            Faculty = await _context.Faculties.FindAsync(id);

            if (Faculty != null)
            {
                _context.Faculties.Remove(Faculty);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
