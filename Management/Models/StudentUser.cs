using System;
using System.Collections.Generic;

namespace Management.Models
{
    public partial class StudentUser
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Level { get; set; }

        public UserLevel LevelNavigation { get; set; }
        public Student Student { get; set; }
    }
}
