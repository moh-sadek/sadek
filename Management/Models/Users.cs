using System;
using System.Collections.Generic;

namespace Management.Models1
{
    public partial class Users
    {
        public long UserId { get; set; }
        public string Name { get; set; }
        public int? LoginName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public byte[] Image { get; set; }
    }
}
