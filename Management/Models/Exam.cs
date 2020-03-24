using System;
using System.Collections.Generic;

namespace Management.Models
{
    public partial class Exam
    {
        public Exam()
        {
            Question = new HashSet<Question>();
            StudentExams = new HashSet<StudentExams>();
            TakenExam = new HashSet<TakenExam>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public float Score { get; set; }
        public TimeSpan Duration { get; set; }
        public int SubjectId { get; set; }
        public int ExamTypeId { get; set; }
        public int GradeId { get; set; }
        public int Year { get; set; }
        public int TeacherSubjectId { get; set; }
        public short ExamType { get; set; }

        public ExamType ExamTypeNavigation { get; set; }
        public Subject Subject { get; set; }
        public TeacherSubject TeacherSubject { get; set; }
        public ICollection<Question> Question { get; set; }
        public ICollection<StudentExams> StudentExams { get; set; }
        public ICollection<TakenExam> TakenExam { get; set; }
    }
}
