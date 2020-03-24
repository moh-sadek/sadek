using System;
using System.Collections.Generic;

namespace Management.Models
{
    public partial class Teacher
    {
        public Teacher()
        {
            TeacherSubject = new HashSet<TeacherSubject>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string NationalIdPassport { get; set; }
        public string Phone { get; set; }
        public int? SchoolId { get; set; }

        public School School { get; set; }
        public ICollection<TeacherSubject> TeacherSubject { get; set; }
    }
}
