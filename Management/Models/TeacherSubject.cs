using System;
using System.Collections.Generic;

namespace Management.Models
{
    public partial class TeacherSubject
    {
        public TeacherSubject()
        {
            Exam = new HashSet<Exam>();
        }

        public int Id { get; set; }
        public int TeacherId { get; set; }
        public int SubjectId { get; set; }
        public string Class { get; set; }
        public int? GradeId { get; set; }

        public Grade Grade { get; set; }
        public Subject Subject { get; set; }
        public Teacher Teacher { get; set; }
        public ICollection<Exam> Exam { get; set; }
    }
}
