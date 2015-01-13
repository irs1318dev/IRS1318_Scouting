using Scouter.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scouter.Data.Configuration
{
    public class RobotEventConfiguration : EntityTypeConfiguration<RobotEvent>
    {
        public RobotEventConfiguration()
        {
            this.Property(p => p.RobotEventType).IsRequired();
            this.Property(p => p.RobotMode).IsRequired();
        }
    }
}