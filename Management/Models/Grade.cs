using System;
using System.Collections.Generic;

namespace Management.Models
{
    public partial class Grade
    {
        public Grade()
        {
            Student = new HashSet<Student>();
            Subject = new HashSet<Subject>();
            TeacherSubject = new HashSet<TeacherSubject>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Student> Student { get; set; }
        public ICollection<Subject> Subject { get; set; }
        public ICollection<TeacherSubject> TeacherSubject { get; set; }
    }
}
