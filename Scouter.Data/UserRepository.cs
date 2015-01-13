using Scouter.Models;
using System.Data.Entity;

namespace Scouter.Data
{
    public class UserRepository : GenericRepository<User>
    {
        public UserRepository(DbContext context) : base(context) { }
    }
}
