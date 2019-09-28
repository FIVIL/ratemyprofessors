using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ratemyprofessors.Models;

namespace ratemyprofessors.Pages.Admin.comments
{
    public class IndexModel : PageModel
    {
        private readonly ratemyprofessors.Models.DataBaseContext _context;

        public IndexModel(ratemyprofessors.Models.DataBaseContext context)
        {
            _context = context;
        }

        [BindProperty]
        public IList<Comment> Commentss { get; set; }


        public async Task OnGetAsync()
        {
            Commentss = await _context.Comments
                .Include(c => c.Professor)
                .Where(x => !x.Verfied)
                .OrderBy(x => x.ProfessorID)
                .ToListAsync();
        }
        public async Task OnPostAsync()
        {
            foreach (var item in Commentss)
            {
                _context.Comments.Update(item);
                _context.Entry(item).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();
            Commentss = await _context.Comments
                .Include(c => c.Professor)
                .Where(x => !x.Verfied)
                .OrderBy(x => x.ProfessorID)
                .ToListAsync();
        }
    }
}
