using System;
using System.Collections.Generic;

namespace Management.Models1
{
    public partial class Cutomers
    {
        public Cutomers()
        {
            ShoortNumber = new HashSet<ShoortNumber>();
        }

        public long CustomerId { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public DateTime? BirthDate { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public short? Status { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }

        public ICollection<ShoortNumber> ShoortNumber { get; set; }
    }
}
