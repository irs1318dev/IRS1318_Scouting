using System;
using System.Collections.Generic;

namespace Scouter.Models
{
    public class User : IAuditInfo
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        public ICollection<Role> Roles { get; set; }

        public User()
        {
            this.Roles = new List<Role>();
        }
    }
}
