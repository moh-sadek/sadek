using System;
using System.Collections.Generic;

namespace Management.Models
{
    public partial class ExamType
    {
        public ExamType()
        {
            Exam = new HashSet<Exam>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Exam> Exam { get; set; }
    }
}
