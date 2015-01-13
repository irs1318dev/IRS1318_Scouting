using Scouter.Models;
using System.Data.Entity;

namespace Scouter.Data
{
    public class FRCEventRepository : GenericRepository<FRCEvent>
    {
        public FRCEventRepository(DbContext context) : base(context) { }
    }
}