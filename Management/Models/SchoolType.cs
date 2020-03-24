using System;
using System.Collections.Generic;

namespace Management.Models
{
    public partial class SchoolType
    {
        public SchoolType()
        {
            School = new HashSet<School>();
        }

        public int Id { get; set; }
        public string Type { get; set; }

        public ICollection<School> School { get; set; }
    }
}
