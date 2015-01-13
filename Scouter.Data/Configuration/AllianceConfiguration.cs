using Scouter.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Scouter.Data.Configuration
{
    public class AllianceConfiguration : EntityTypeConfiguration<Alliance>
    {
        public AllianceConfiguration()
        {
            this.HasKey(k => k.Id);
            
        }
    }
}
