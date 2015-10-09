using Scouter.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Scouter.Data.Configuration
{
    public class RoleConfiguration : EntityTypeConfiguration<Role>
    {
        public RoleConfiguration()
        {
            this.ToTable("webpages_Roles");
            this.Property(p => p.RoleName).HasMaxLength(256).IsRequired();
        }
    }
}
