using System;
using System.Collections.Generic;

namespace Management.Models
{
    public partial class UnknownNumber
    {
        public int Id { get; set; }
        public int? Code { get; set; }
        public int? Count { get; set; }
        public DateTime? CreatecdOn { get; set; }
    }
}
