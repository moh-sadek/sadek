using System;
using System.Collections.Generic;

namespace Management.Models
{
    public partial class Centers
    {
        public Centers()
        {
            Organizations = new HashSet<Organizations>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public short? Status { get; set; }

        public ICollection<Organizations> Organizations { get; set; }
    }
}
