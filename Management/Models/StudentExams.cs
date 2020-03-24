using System;
using System.Collections.Generic;

namespace Management.Models
{
    public partial class StudentExams
    {
        public int Id { get; set; }
        public int? StudentId { get; set; }
        public int? ExamId { get; set; }
        public double? StudentDegree { get; set; }
        public double? FinalDegree { get; set; }

        public Exam Exam { get; set; }
        public Student Student { get; set; }
    }
}
