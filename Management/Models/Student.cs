using System;
using System.Collections.Generic;

namespace Management.Models
{
    public partial class Student
    {
        public Student()
        {
            StudentExams = new HashSet<StudentExams>();
            StudentUser = new HashSet<StudentUser>();
            TakenExam = new HashSet<TakenExam>();
        }

        public int Id { get; set; }
        public int SchoolId { get; set; }
        public int GradeId { get; set; }
        public string Name { get; set; }
        public string NationalIdPassport { get; set; }
        public string Phone { get; set; }

        public Grade Grade { get; set; }
        public School School { get; set; }
        public ICollection<StudentExams> StudentExams { get; set; }
        public ICollection<StudentUser> StudentUser { get; set; }
        public ICollection<TakenExam> TakenExam { get; set; }
    }
}
