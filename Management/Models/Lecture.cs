using System;
using System.Collections.Generic;

namespace Management.Models
{
    public partial class Lecture
    {
        public Lecture()
        {
            Attachment = new HashSet<Attachment>();
            VideoAttachment = new HashSet<VideoAttachment>();
        }

        public int Id { get; set; }
        public int Sequance { get; set; }
        public int ChapterId { get; set; }
        public int SubjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Chapter Chapter { get; set; }
        public Subject Subject { get; set; }
        public ICollection<Attachment> Attachment { get; set; }
        public ICollection<VideoAttachment> VideoAttachment { get; set; }
    }
}
