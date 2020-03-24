using System;
using System.Collections.Generic;

namespace Management.Models
{
    public partial class Subject
    {
        public Subject()
        {
            Chapter = new HashSet<Chapter>();
            Exam = new HashSet<Exam>();
            Lecture = new HashSet<Lecture>();
            TeacherSubject = new HashSet<TeacherSubject>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int GradeId { get; set; }

        public Grade Grade { get; set; }
        public ICollection<Chapter> Chapter { get; set; }
        public ICollection<Exam> Exam { get; set; }
        public ICollection<Lecture> Lecture { get; set; }
        public ICollection<TeacherSubject> TeacherSubject { get; set; }
    }
}
