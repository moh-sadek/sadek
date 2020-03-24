using System;
using System.Collections.Generic;

namespace Management.Models
{
    public partial class TakenExam
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int ExamId { get; set; }
        public int AnswerId { get; set; }
        public string Answer { get; set; }
        public float Score { get; set; }

        public Answer AnswerNavigation { get; set; }
        public Exam Exam { get; set; }
        public Student Student { get; set; }
    }
}
