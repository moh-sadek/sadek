using System;
using System.Collections.Generic;

namespace Management.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public int Level { get; set; }
        public bool State { get; set; }

        public UserLevel LevelNavigation { get; set; }
    }
}
