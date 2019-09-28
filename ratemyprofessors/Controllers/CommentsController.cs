using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ratemyprofessors.Models;

namespace ratemyprofessors.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly DataBaseContext _context;
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment env;
        public CommentsController(DataBaseContext context, IConfiguration configuration, IHostingEnvironment _env)
        {
            _context = context;
            _configuration = configuration;
            env = _env;
        }

        [HttpGet("latest")]
        public async Task<IActionResult> GetComment()
        {
            var comments = await _context.Comments
                 .AsNoTracking()
                 .Include(x => x.Professor)
                 .Include(x => x.Email)
                 .Where(x => x.Email.Verified && x.Verfied)
                 .OrderBy(x => x.DateTime)
                 .Take(5)
                 .ToListAsync();

            if (comments == null)
            {
                return NotFound();
            }
            if (comments.Count == 0)
            {
                return NotFound();
            }
            return Ok(comments);
        }

        [HttpGet("ByProf/{id}")]
        public async Task<IActionResult> GetComments([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comments = await _context.Comments
                .AsNoTracking()
                .Include(x => x.Email)
                .Where(x => x.Email.Verified && x.ProfessorID == id && x.Verfied)
                .ToListAsync();

            if (comments == null)
            {
                return NotFound();
            }
            if (comments.Count == 0)
            {
                return NotFound();
            }
            return Ok(comments);
        }

        [HttpGet("ProfAvg/{id}")]
        public async Task<IActionResult> GetCommentsAvg([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comments = await _context.Comments
                .AsNoTracking()
                .Include(x => x.Email)
                .Include(x => x.Professor)
                .Where(x => x.Email.Verified && x.ProfessorID == id && x.Verfied)
                .ToListAsync();

            if (comments == null)
            {
                return NotFound();
            }
            if (comments.Count == 0)
            {
                return NotFound();
            }
            if (!comments[0].Professor.Staff)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var item in comments)
                {
                    if(!string.IsNullOrWhiteSpace(item.Comments))
                    sb.Append(item.Comments + '$');
                }
                //sb.Length--;
                var comavg = new
                {
                    Marking = comments.Average(x => x.Marking),
                    Teaching = comments.Average(x => x.Teaching),
                    HomeWork = comments.Average(x => x.HomeWork),
                    Project = comments.Average(x => x.Project),
                    Moods = comments.Average(x => x.Moods),
                    RollCall = comments.Average(x => x.RollCall),
                    Exhausting = comments.Average(x => x.Exhausting),
                    HandOuts = comments.Average(x => x.HandOuts),
                    Update = comments.Average(x => x.Update),
                    ScapeAtTheEnd = comments.Average(x => x.ScapeAtTheEnd),
                    Answering = comments.Average(x => x.Answering),
                    HardExams = comments.Average(x => x.HardExams),
                    Knoledge = comments.Average(x => x.Knoledge),
                    OverAll = comments.Average(x => x.OverAll),
                    Score = comments.Average(x => x.Score),
                    Text = sb.ToString()
                };
                return Ok(comavg);
            }
            else
            {
                var comavg = new
                {
                    Teaching = comments.Average(x => x.Teaching),
                    HomeWork = comments.Average(x => x.HomeWork),
                    Project = comments.Average(x => x.Project),
                    Moods = comments.Average(x => x.Moods),
                    RollCall = comments.Average(x => x.RollCall),
                    Exhausting = comments.Average(x => x.Exhausting),
                    HandOuts = comments.Average(x => x.HandOuts),
                    Update = comments.Average(x => x.Update),
                    ScapeAtTheEnd = comments.Average(x => x.ScapeAtTheEnd),
                    Answering = comments.Average(x => x.Answering),
                    HardExams = comments.Average(x => x.HardExams),
                    Knoledge = comments.Average(x => x.Knoledge),
                    OverAll = comments.Average(x => x.OverAll),
                    Angry = comments.Average(x => x.Angry),
                    Bluntess = comments.Average(x => x.Bluntess),
                    DoYourWork = comments.Average(x => x.DoYourWork),
                    Bad = comments.Average(x => x.Bad),

                    Score = comments.Average(x => x.Score),
                    Text = comments.Select(x => x.Comments)
                };
                return Ok(comavg);
            }
        }
        // GET: api/Comments/5

        [HttpGet("{id}")]
        public async Task<IActionResult> GetComment([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = await _context.Comments.FindAsync(id);

            if (comment == null)
            {
                return NotFound();
            }
            if (!comment.Verfied) return NotFound();
            return Ok(comment);
        }

        [HttpGet("RatePlus/{id}")]
        public async Task<IActionResult> RatePlusComment([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = await _context.Comments.FindAsync(id);

            if (comment == null)
            {
                return NotFound();
            }
            comment.Like++;
            _context.Entry(comment).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(comment);
        }

        [HttpGet("RateNegetive/{id}")]
        public async Task<IActionResult> RateNegetiveComment([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = await _context.Comments.FindAsync(id);

            if (comment == null)
            {
                return NotFound();
            }
            comment.DisLike++;
            _context.Entry(comment).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(comment);
        }
        // POST: api/Comments

        [HttpPost]
        public async Task<IActionResult> PostComment([FromBody] Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_context.Comments
                .AsNoTracking()
                .Include(x => x.Email)
                .Any(x => x.Email.Address == comment.Email.Address && x.ProfessorID == comment.ProfessorID))
            {
                //duplicate
                HttpContext.Response.Headers.Add("MailRes", "1");
                return Ok();
            }
            string add = comment.Email.Address;
            comment.Email = null;
            _context.Comments.Add(comment);
            _context.Entry(comment).State = EntityState.Added;
            comment.Verfied = false;
            comment.DateTime = DateTime.Now;

            var email = await _context.Emails.FirstOrDefaultAsync(x => x.Address == add);
            if (email == null)
            {
                var Email = new Email()
                {
                    Address = add,
                    ID = Guid.NewGuid(),
                    Verified = false
                };
                _context.Emails.Add(Email);
                _context.Entry(Email).State = EntityState.Added;
                comment.EmailID = Email.ID;
                await SendMail(Email.Address, Email.ID);
                await _context.SaveChangesAsync();
                //confirmation mail send
                HttpContext.Response.Headers.Add("MailRes", "2");
                return Ok();
            }
            if (!email.Verified)
            {
                comment.EmailID = email.ID;
                _context.Entry(email).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                //please confirm your mial
                HttpContext.Response.Headers.Add("MailRes", "3");
                return Ok();
            }
            comment.EmailID = email.ID;
            _context.Entry(email).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            //done
            HttpContext.Response.Headers.Add("MailRes", "4");
            return Ok();
        }

        [HttpGet("ResednMail/{add}")]
        public async Task<IActionResult> Resend([FromRoute] string add)
        {
            var e = await _context.Emails.FirstOrDefaultAsync(x => x.Address == add);
            if (e == null) return NotFound();
            if (e.Verified) return Ok();
            await SendMail(add, e.ID);
            HttpContext.Response.Headers.Add("MailRes", "1");
            return Ok();
        }
        private async Task SendMail(string address, Guid code)
        {
            await Task.Run(() =>
            {
                using (var client = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = _configuration["Email:Email"],
                        Password = _configuration["Email:Password"]
                    };

                    client.Credentials = credential;
                    client.Host = _configuration["Email:Host"];
                    client.Port = int.Parse(_configuration["Email:Port"]);
                    client.EnableSsl = false;
                    using (var emailMessage = new MailMessage())
                    {
                        emailMessage.To.Add(new MailAddress(address));
                        emailMessage.From = new MailAddress(_configuration["Email:Email"]);
                        emailMessage.Sender = new MailAddress(_configuration["Email:Email"]);
                        emailMessage.Priority = MailPriority.High;
                        emailMessage.Subject = "تایید آدرس ایمیل";
                        var p = System.IO.Path.Combine(env.WebRootPath, "MessageTemplate.html");
                        var builder = new StringBuilder();
                        using (var reader = System.IO.File.OpenText(p))
                        {
                            builder.Append(reader.ReadToEnd());
                        }
                        builder.Replace("{{var}}", code.ToString());                        
                        emailMessage.Body = builder.ToString();
                        emailMessage.IsBodyHtml = true;
                        client.Send(emailMessage);
                    }
                }
            });
        }

    }
}