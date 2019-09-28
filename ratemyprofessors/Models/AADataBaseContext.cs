using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ratemyprofessors.Models;

namespace ratemyprofessors.Models
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {

        }

        public DbSet<University> Universities { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<ProfFac> ProfFacs { get; set; }
        public DbSet<ProfCourse> ProfCourses { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<ratemyprofessors.Models.Account> Accounts { get; set; }
        public DbSet<ContactUs> Contacts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<University>(e =>
            {
                e.HasIndex(c => c.Name).IsUnique();
            });
            modelBuilder.Entity<Faculty>(e =>
            {
                e.HasIndex(c => new { c.Name, c.UniversityID }).IsUnique();
                e.HasIndex(c => c.AliasName).IsUnique();
            });
            modelBuilder.Entity<Course>(e =>
            {
                e.HasIndex(c => new { c.Name, c.FacultyID }).IsUnique();
            });
            modelBuilder.Entity<Email>(e =>
            {
                e.HasIndex(c => c.Address).IsUnique();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
