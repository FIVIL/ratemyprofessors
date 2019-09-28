using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ratemyprofessors.Models;

namespace ratemyprofessors.Pages.Admin.facProf
{
    public class EditModel : PageModel
    {
        private readonly ratemyprofessors.Models.DataBaseContext _context;

        public EditModel(ratemyprofessors.Models.DataBaseContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ProfFac ProfFac { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProfFac = await _context.ProfFacs
                .Include(p => p.Faculty)
                .Include(p => p.Professor).FirstOrDefaultAsync(m => m.ID == id);

            if (ProfFac == null)
            {
                return NotFound();
            }
           ViewData["FacultyID"] = new SelectList(_context.Faculties, "ID", nameof(Faculty.Name));
           ViewData["ProfessorID"] = new SelectList(_context.Professors.OrderBy(x=>x.FullName), "ID", nameof(Professor.FullName));
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ProfFac).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfFacExists(ProfFac.ID))
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

        private bool ProfFacExists(Guid id)
        {
            return _context.ProfFacs.Any(e => e.ID == id);
        }
    }
}
