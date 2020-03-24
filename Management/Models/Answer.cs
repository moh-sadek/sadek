using System;
using System.Collections.Generic;

namespace Management.Models
{
    public partial class Answer
    {
        public Answer()
        {
            TakenExam = new HashSet<TakenExam>();
        }

        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string ExamAnswer { get; set; }

        public Question Question { get; set; }
        public ICollection<TakenExam> TakenExam { get; set; }
    }
}
