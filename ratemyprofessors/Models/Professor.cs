using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ratemyprofessors.Models
{
    public class Professor
    {
        [Key]
        public Guid ID { get; set; }

        [StringLength(40)]
        public string Name { get; set; }

        [StringLength(40)]
        public string LastName { get; set; }

        public string FullName { get => Name + " " + LastName; }

        [StringLength(200)]
        public string Link { get; set; }

        [StringLength(200)]
        public string PrivateLink { get; set; }

        [StringLength(200)]
        public string WPLink { get; set; }

        [StringLength(200)]
        public string ImageLink { get; set; }

        public string Comment { get; set; }

        public bool Staff { get; set; }

        public double Score { get => (Comments != null) ? (Comments.Count > 0) ? Math.Round(Comments.Average(x => x.Score), 2) : 0 : 0; }
        public int CommentCount { get => (Comments != null) ? Comments.Count : 0; }

        [JsonIgnore]
        public ICollection<ProfFac> ProfFacs { get; set; }

        [JsonIgnore]
        public ICollection<ProfCourse> ProfCourses { get; set; }

        [NotMapped]
        public string Courses { get; set; }

        [NotMapped]
        public string Facs { get; set; }

        [JsonIgnore]
        public ICollection<Comment> Comments { get; set; }

        public bool Approved { get; set; }
    }
}
