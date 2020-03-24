using System;
using System.Collections.Generic;

namespace Management.Models
{
    public partial class Question
    {
        public Question()
        {
            Answer = new HashSet<Answer>();
        }

        public int Id { get; set; }
        public string Question1 { get; set; }
        public float Score { get; set; }
        public int ExamId { get; set; }
        public string AnswerId { get; set; }

        public Exam Exam { get; set; }
        public ICollection<Answer> Answer { get; set; }
    }
}
