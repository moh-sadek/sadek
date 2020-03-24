using System;
using System.Collections.Generic;

namespace Management.Models
{
    public partial class Chapter
    {
        public Chapter()
        {
            Lecture = new HashSet<Lecture>();
        }

        public int Id { get; set; }
        public int Sequance { get; set; }
        public string Name { get; set; }
        public int SubjectId { get; set; }

        public Subject Subject { get; set; }
        public ICollection<Lecture> Lecture { get; set; }
    }
}
