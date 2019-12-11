using System;
using System.Collections.Generic;

namespace Management.Models1
{
    public partial class ShoortNumberActions
    {
        public long Id { get; set; }
        public long? ShoortNumberId { get; set; }
        public short? ActionType { get; set; }
        public string ActionDescription { get; set; }
        public int? Amount { get; set; }
        public int? Smscount { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatecdOn { get; set; }

        public ShoortNumber ShoortNumber { get; set; }
    }
}
