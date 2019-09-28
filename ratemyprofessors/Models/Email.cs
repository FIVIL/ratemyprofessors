using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ratemyprofessors.Models
{
    public class Email
    {
        [Key]
        public Guid ID { get; set; }

        [StringLength(60), EmailAddress]
        public string Address { get; set; }

        public bool Verified { get; set; }

        [JsonIgnore]
        public ICollection<Comment> Comments { get; set; }
    }
}
