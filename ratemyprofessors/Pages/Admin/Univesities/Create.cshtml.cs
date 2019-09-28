using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ratemyprofessors.Models;

namespace ratemyprofessors.Pages.Admin.Univesities
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
            return Page();
        }

        [BindProperty]
        public University University { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Universities.Add(University);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}