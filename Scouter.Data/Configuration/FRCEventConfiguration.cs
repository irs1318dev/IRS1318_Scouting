using Scouter.Models;
using System.Data.Entity.ModelConfiguration;

namespace Scouter.Data.Configuration
{
    public class FRCEventConfiguration : EntityTypeConfiguration<FRCCompetition>
    {
        public FRCEventConfiguration()
        {
            this.Property(p => p.Name).IsRequired();
            this.Property(p => p.BeginDate).IsRequired().HasColumnType("datetime");
            this.Property(p => p.FinishDate).IsRequired().HasColumnType("datetime");
            this.Property(p => p.Venue).IsRequired();
            this.Property(p => p.City).IsRequired();
            this.Property(p => p.State).IsRequired();
            this.Property(p => p.Type).IsRequired();
            this.Property(p => p.ImageName).IsOptional();

            this.Property(p => p.CreatedOn).IsRequired().HasColumnType("datetime");
            this.Property(p => p.ModifiedOn).IsRequired().HasColumnType("datetime");
        }
    }
}
