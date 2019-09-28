using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ratemyprofessors.Models;
using Microsoft.EntityFrameworkCore;

namespace ratemyprofessors.Pages.Admin.comments
{
    public class Index2Model : PageModel
    {
        private readonly ratemyprofessors.Models.DataBaseContext _context;

        public Index2Model(ratemyprofessors.Models.DataBaseContext context)
        {
            _context = context;
        }

        [BindProperty]
        public IList<Comment> Commentss { get; set; }

        public async Task OnGetAsync()
        {
            Commentss = await _context.Comments
               .Include(c => c.Professor)
               .Include(c=>c.Email)
               .Where(x => x.Verfied)
               .OrderBy(x => x.ProfessorID)
               .ToListAsync();
        }
    }
}