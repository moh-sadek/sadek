using System;
using System.Collections.Generic;

namespace Management.Models
{
    public partial class UserLevel
    {
        public UserLevel()
        {
            StudentUser = new HashSet<StudentUser>();
            User = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<StudentUser> StudentUser { get; set; }
        public ICollection<User> User { get; set; }
    }
}
