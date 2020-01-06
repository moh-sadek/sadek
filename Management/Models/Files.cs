using System;
using System.Collections.Generic;

namespace Management.Models
{
    public partial class Files
    {
        public Files()
        {
            FileContent = new HashSet<FileContent>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreatecdOn { get; set; }

        public ICollection<FileContent> FileContent { get; set; }
    }
}
