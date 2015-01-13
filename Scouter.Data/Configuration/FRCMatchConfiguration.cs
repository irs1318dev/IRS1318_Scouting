using Scouter.Models;
using System.Data.Entity.ModelConfiguration;

namespace Scouter.Data.Configuration
{
    public class FRCMatchConfiguration : EntityTypeConfiguration<FRCMatch>
    {
        public FRCMatchConfiguration()
        {
            this.Property(p => p.SequenceNumber).IsRequired();

            this.Property(p => p.CreatedOn).IsRequired().HasColumnType("datetime");
            this.Property(p => p.ModifiedOn).IsRequired().HasColumnType("datetime");
        }
    }
}
