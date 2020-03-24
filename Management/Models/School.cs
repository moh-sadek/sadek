using System;
using System.Collections.Generic;

namespace Management.Models
{
    public partial class School
    {
        public School()
        {
            Student = new HashSet<Student>();
            Teacher = new HashSet<Teacher>();
        }

        public int Id { get; set; }
        public int MunicipalityId { get; set; }
        public string Name { get; set; }
        public int SchoolTypeId { get; set; }
        public int EducationLevelId { get; set; }
        public string Address { get; set; }

        public EducationLevel EducationLevel { get; set; }
        public Municipality Municipality { get; set; }
        public SchoolType SchoolType { get; set; }
        public ICollection<Student> Student { get; set; }
        public ICollection<Teacher> Teacher { get; set; }
    }
}
