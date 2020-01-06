using System;
using System.Collections.Generic;

namespace Management.Models
{
    public partial class FileContent
    {
        public long Id { get; set; }
        public long? FilesId { get; set; }
        public string Code { get; set; }
        public int? Count { get; set; }
        public DateTime? CreatecdOn { get; set; }

        public Files Files { get; set; }
    }
}
