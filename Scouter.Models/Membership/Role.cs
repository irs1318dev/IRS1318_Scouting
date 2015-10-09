using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scouter.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public ICollection<User> Users { get; set; }

        public Role()
        {
            this.Users = new List<User>();
        }
    }
}
