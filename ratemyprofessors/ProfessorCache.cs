using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ratemyprofessors.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ratemyprofessors
{
    public class ProfessorCache
    {
        private List<ProfessorCacheViewModel> Profs { get; set; }
        private DateTime LastUpdate { get; set; }
        private readonly object _lock;
        private readonly IConfiguration _configuration;
        public ProfessorCache(IConfiguration configuration)
        {
            Profs = new List<ProfessorCacheViewModel>();
            LastUpdate = DateTime.Now;
            _lock = new object();
            _configuration = configuration;
            Update();
        }
        private void Update()
        {
            var OptionsBuilder = new DbContextOptionsBuilder<DataBaseContext>();
            OptionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            List<Professor> ProfsL;
            using (var DB = new DataBaseContext(OptionsBuilder.Options))
            {
                ProfsL = DB.Professors
                   .AsNoTracking()
                   .Include(x => x.Comments)
                   .Include(x => x.ProfFacs)
                       .ThenInclude(y => y.Faculty)
                   .Where(x => x.Approved)
                   .ToList();
            }
            lock (_lock)
            {
                Profs.Clear();
                ProfessorCacheViewModel.MaxComment = int.MinValue;
                foreach (var item in ProfsL)
                {
                    Profs.Add(new ProfessorCacheViewModel
                    {
                        ID = item.ID,
                        FullName = item.FullName,
                        Score = item.Score,
                        FacIDs = item.ProfFacs.Select(x => x.Faculty.AliasName).ToList(),
                        ImageLink=item.ImageLink,
                        CommentCount=item.CommentCount
                    });
                    if (item.CommentCount > ProfessorCacheViewModel.MaxComment)
                        ProfessorCacheViewModel.MaxComment = item.CommentCount;
                }
                LastUpdate = DateTime.Now;
            }
        }
        public List<ProfessorCacheViewModel> Professors
        {
            get
            {
                if (LastUpdate.AddHours(1) < DateTime.Now) Update();
                return Profs;
            }
        }
    }
    public class ProfessorCacheViewModel
    {
        public static int MaxComment;
        public Guid ID { get; set; }
        public string FullName { get; set; }
        public double Score { get; set; }
        public int CommentCount { get; set; }
        public List<string> FacIDs { get; set; } = new List<string>();
        public string ImageLink { get; set; }
    }
}
