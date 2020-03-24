using System;
using System.Collections.Generic;

namespace Management.Models
{
    public partial class Attachment
    {
        public int Id { get; set; }
        public int LectureId { get; set; }
        public string Url { get; set; }

        public Lecture Lecture { get; set; }
    }
}
