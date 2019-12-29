using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ratemyprofessors.Models;

namespace ratemyprofessors.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorsController : ControllerBase
    {
        private readonly DataBaseContext _context;
        private readonly ProfessorCache professorCache;
        public ProfessorsController(DataBaseContext context, ProfessorCache _professorCache)
        {
            _context = context;
            professorCache = _professorCache;
        }
        [HttpGet("ByID/{id}")]
        public async Task<IActionResult> GetByID([FromRoute]Guid id)
        {
            var p = await _context.Professors
                .Include(x => x.Comments)
                .FirstOrDefaultAsync(x => x.ID == id);
            if (p == null) return NotFound();
            return Ok(p);
        }

        [HttpGet("GetByName")]
        public IEnumerable<ProfessorCacheViewModel> GetByName()
        //public async Task<IEnumerable<Professor>> GetByName()
        {
            //var Profs = await _context.Professors
            //    .Where(x => x.Approved)
            //    .Include(x => x.Comments)
            //    .AsNoTracking()
            //    .ToListAsync();
            //return Profs;
            return professorCache.Professors;
        }

        [HttpGet("GetBests")]
        public IActionResult GetBests()
        //public async Task<IActionResult> GetBests()
        {
            //return Ok((await _context.Professors
            //    .AsNoTracking()
            //    .Include(x => x.Comments)
            //    .Where(x => x.Approved)
            //    .ToListAsync()).OrderBy(x => x.Score).Take(5));
            return Ok(professorCache.Professors
                .Where(x => x.CommentCount > ProfessorCacheViewModel.MaxComment / 10)
                .OrderBy(x => x.Score)
                .Take(5));
        }
        [HttpGet("GetWorst")]
        public IActionResult GetWorst()
        //public async Task<IActionResult> GetWorst()
        {
            //return Ok((await _context.Professors
            //    .AsNoTracking()
            //    .Include(x => x.Comments)
            //    .Where(x => x.Approved)
            //    .ToListAsync()).OrderByDescending(x => x.Score).Take(5));
            return Ok(professorCache.Professors
                 .Where(x => x.CommentCount > ProfessorCacheViewModel.MaxComment / 10)
                .OrderByDescending(x => x.Score)
                .Take(5));
        }
        // GET: api/Professors/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProfessors([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var course = await _context.Courses
                .AsNoTracking()
                .Include(x => x.ProfCourses)
                    .ThenInclude(y => y.Professor)
                        .ThenInclude(z => z.Comments)
                .FirstOrDefaultAsync(x => x.ID == id);

            if (course == null)
            {
                return NotFound();
            }
            return Ok(course.ProfCourses.Select(x => x.Professor).Where(x => x.Approved).OrderBy(x => x.FullName));
        }


        [HttpGet("ProfFac/{id}")]
        public IActionResult GetProfessorsByFac([FromRoute] string id)
        //public async Task<IActionResult> GetProfessorsByFac([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var course = await _context.Faculties
            //    .AsNoTracking()
            //    .Include(x => x.ProfFacs)
            //        .ThenInclude(y => y.Professor)
            //            .ThenInclude(z => z.Comments)
            //    .FirstOrDefaultAsync(x => x.AliasName == id);

            //if (course == null)
            //{
            //    return NotFound();
            //}
            //return Ok(course.ProfFacs.Select(x => x.Professor).Where(x => x.Approved));
            return Ok(professorCache.Professors.Where(x => x.FacIDs.Contains(id)).OrderBy(x => x.FullName));
        }


        // POST: api/Professors
        // TODO Return BadRequest on model error.
        [HttpPost("NewProf")]
        public async Task<IActionResult> PostProfessor([FromBody] Professor professor)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            professor.ID = Guid.NewGuid();
            professor.Staff = false;
            professor.Approved = false;
            var courses = professor.Courses.Split(';');
            foreach (var item in courses)
            {
                if (!string.IsNullOrWhiteSpace(item))
                {
                    if (Guid.TryParse(item, out var ID))
                    {
                        var pc = new ProfCourse
                        {
                            ID = Guid.NewGuid(),
                            CourseID = ID,
                            ProfessorID = professor.ID
                        };
                        _context.ProfCourses.Add(pc);
                        _context.Entry(pc).State = EntityState.Added;
                    }
                }
            }
            var facs = professor.Facs.Split(';');
            foreach (var item in facs)
            {
                if (!string.IsNullOrWhiteSpace(item))
                {
                    if (Guid.TryParse(item, out var ID))
                    {
                        var pc = new ProfFac
                        {
                            ID = Guid.NewGuid(),
                            FacultyID = ID,
                            ProfessorID = professor.ID
                        };
                        _context.ProfFacs.Add(pc);
                        _context.Entry(pc).State = EntityState.Added;
                    }
                }
            }

            professor.Courses = string.Empty;
            professor.Facs = string.Empty;
            _context.Professors.Add(professor);
            _context.Entry(professor).State = EntityState.Added;
            await _context.SaveChangesAsync();

            return Ok();
        }

    }
}