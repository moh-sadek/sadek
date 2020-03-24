using System;
using System.Collections.Generic;

namespace Management.Models
{
    public partial class Municipality
    {
        public Municipality()
        {
            School = new HashSet<School>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }

        public ICollection<School> School { get; set; }
    }
}
