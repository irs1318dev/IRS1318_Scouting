using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scouter.Models
{
    public class OAuthMembership
    {
        public string Provider { get; set; }
        public string ProviderUserId { get; set; }
        public int UserId { get; set; }
    }
}
