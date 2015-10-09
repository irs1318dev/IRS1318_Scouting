using Scouter.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scouter.Data.Configuration
{
	public class CurrentScouterDataConfiguration: EntityTypeConfiguration<CurrentScoutData>
	{
		public CurrentScouterDataConfiguration()
		{
		}
	}
}
