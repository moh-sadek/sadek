using System;
using System.Collections.Generic;

namespace Management.Models
{
    public partial class ShoortNumber
    {
        public ShoortNumber()
        {
            ShoortNumberActions = new HashSet<ShoortNumberActions>();
        }

        public long Id { get; set; }
        public int? Code { get; set; }
        public int? Amount { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public int? Smscount { get; set; }
        public int? UsageSms { get; set; }
        public string Service { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public long? CustomerId { get; set; }
        public short? State { get; set; }

        public Cutomers Customer { get; set; }
        public ICollection<ShoortNumberActions> ShoortNumberActions { get; set; }
    }
}
