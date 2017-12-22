using System;
using System.Collections.Generic;

namespace ARBUserManagement.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public byte[] Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Region { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
