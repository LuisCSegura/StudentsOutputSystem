using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SSE.BOL
{
    public class Student
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string IdNumber { get; set; }
        public char Genre { get; set; }
        public string Section { get; set; }
        public Image PersonalPhoto { get; set; }
        public Image SectionPhoto { get; set; }
        public bool PLunch { get; set; }
        public bool PFreeLessons { get; set; }
        public bool PFreeAfternoon { get; set; }
    }
}
