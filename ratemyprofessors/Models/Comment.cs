using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ratemyprofessors.Models
{
    public class Comment
    {
        [Key]
        public Guid ID { get; set; }

        [StringLength(20)]
        public string ShowName { get; set; }

        public byte Teaching { get; set; }//2
        public byte Marking { get; set; }//3
        public byte HomeWork { get; set; }//2
        public byte Project { get; set; }//1
        public byte Moods { get; set; }//1
        //absent present//9
        public byte RollCall { get; set; }//2
        public byte Exhausting { get; set; }//1
        public byte HandOuts { get; set; }//2
        public byte Update { get; set; }//1//15
        public byte ScapeAtTheEnd { get; set; }//3
        public byte Answering { get; set; }//1
        public byte HardExams { get; set; }//2//21
        public byte Knoledge { get; set; }//2
        public byte OverAll { get; set; }//4//27

        [StringLength(399)]
        public string Comments { get; set; }
        public byte Like { get; set; }
        public byte DisLike { get; set; }
        public DateTime DateTime { get; set; }

        public byte? Angry { get; set; }
        //shour
        public byte? Bluntess { get; set; }
        public byte? DoYourWork { get; set; }
        public byte? Bad { get; set; }

        public double Score
        {
            get =>
                ((double)((double)(
                (Teaching * 2)
                + (Marking * 3)
                + (HomeWork * -1)
                + (Project * -1)
                + (Moods)
                + (RollCall * -1)
                + (Exhausting * -1)
                + (HandOuts * -1)
                + (Update)
                + (ScapeAtTheEnd * -3)
                + (Answering)
                + (HardExams * -2)
                + (Knoledge * 2)
                + (OverAll * 4)
                ) / (double)24));
        }


        public Guid ProfessorID { get; set; }
        public Professor Professor { get; set; }

        public Guid? AccountID { get; set; }
        [JsonIgnore]
        public Account Account { get; set; }

        public Guid EmailID { get; set; }
        public Email Email { get; set; }

        public bool Verfied { get; set; }
    }
}
