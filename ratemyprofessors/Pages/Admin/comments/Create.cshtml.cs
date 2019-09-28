using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ratemyprofessors.Models;

namespace ratemyprofessors.Pages.Admin.comments
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
        ViewData["AccountID"] = new SelectList(_context.Accounts, "ID", "Name");
        ViewData["ProfessorID"] = new SelectList(_context.Professors, "ID", "Name");
            return Page();
        }

        [BindProperty]
        public Comment Comment { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Comments.Add(Comment);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}