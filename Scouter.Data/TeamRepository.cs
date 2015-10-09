using Scouter.Models;
using System.Data.Entity;

namespace Scouter.Data
{
    public class TeamRepository : GenericRepository<Team>
    {
        public TeamRepository(DbContext context) : base(context) { }
    }
}
