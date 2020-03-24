using System;
using System.Collections.Generic;

namespace Management.Models
{
    public partial class EducationLevel
    {
        public EducationLevel()
        {
            School = new HashSet<School>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<School> School { get; set; }
    }
}
