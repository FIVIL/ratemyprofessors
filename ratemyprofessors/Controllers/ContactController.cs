using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ratemyprofessors.Models;

namespace ratemyprofessors.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly DataBaseContext _context;

        public ContactController(DataBaseContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> PostContact([FromBody] ContactUs CUS)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            CUS.ID = Guid.NewGuid();
            _context.Contacts.Add(CUS);
            _context.Entry(CUS).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}