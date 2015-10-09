using Scouter.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace Scouter.Data
{
    public class FRCMatchRepository : GenericRepository<FRCMatch>
    {
        public FRCMatchRepository(DbContext context) : base(context) { this.Context = context; }
    }
}
