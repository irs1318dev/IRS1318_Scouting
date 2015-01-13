using Scouter.Models;
using System.Data.Entity.ModelConfiguration;

namespace Scouter.Data.Configuration
{
    public class TeamConfiguration : EntityTypeConfiguration<Team>
    {
        public TeamConfiguration()
        {
            this.Property(p => p.Name).IsRequired().HasMaxLength(100);
            this.Property(p => p.Description).IsOptional().HasMaxLength(500);
            this.Property(p => p.ImageName).IsOptional().HasMaxLength(500);

            this.Property(p => p.CreatedOn).IsRequired().HasColumnType("datetime");
            this.Property(p => p.ModifiedOn).IsRequired().HasColumnType("datetime");
        }
    }
}
