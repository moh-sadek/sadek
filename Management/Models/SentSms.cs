using System;
using System.Collections.Generic;

namespace Management.Models
{
    public partial class SentSms
    {
        public long Id { get; set; }
        public int? ShortCode { get; set; }
        public DateTime? CreatedOn { get; set; }
        public short? Status { get; set; }
    }
}
