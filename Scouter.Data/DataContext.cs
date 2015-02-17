using System;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;

using Scouter.Models;
using Scouter.Data.Configuration;

namespace Scouter.Data
{
    public class DataContext : DbContext
    {
        public DbSet<FRCCompetition> FRCEvents { get; set; }
        public DbSet<FRCMatch> FRCMatches { get; set; }
        public DbSet<Alliance> Alliances { get; set; }
        public DbSet<Team> Teams { get; set; }
        //public DbSet<zzParticipant> Participants { get; set; }
        public DbSet<RobotEvent> RobotEvents { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
		public DbSet<CurrentScoutData> CurrentScoutData { get; set; }
		public DbSet<ScoutingNotes> Notes { get; set; }

        public static string ConnectionStringName
        {
            get
            {
                if (ConfigurationManager.AppSettings[Environment.MachineName.ToLower()] != null)
                    return ConfigurationManager.AppSettings[Environment.MachineName.ToLower()];

                return "DefaultConnection";
            }
        }

        static DataContext()
        {
            Database.SetInitializer(new CustomDatabaseInitializer());
        }

        public DataContext() : base(nameOrConnectionString: DataContext.ConnectionStringName) { Configuration.AutoDetectChangesEnabled = false; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Data
            modelBuilder.Configurations.Add(new FRCEventConfiguration());
            modelBuilder.Configurations.Add(new FRCMatchConfiguration());
			modelBuilder.Configurations.Add(new CurrentScouterDataConfiguration());
            modelBuilder.Configurations.Add(new TeamConfiguration());
            modelBuilder.Configurations.Add(new RobotEventConfiguration());

            // Membership
            modelBuilder.Configurations.Add(new MembershipConfiguration());
            modelBuilder.Configurations.Add(new OAuthMembershipConfiguration());
            modelBuilder.Configurations.Add(new RoleConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());
            
            //base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Manage CreatedOn and ModifiedOn fields of all Entities that implement IAuditInfo
        /// </summary>
        private void ApplyRules()
        {
            foreach(var entry in this.ChangeTracker.Entries()
                .Where(
                       e => e.Entity is IAuditInfo &&
                       (e.State == EntityState.Added) ||
                       (e.State == EntityState.Modified)))
            {
                IAuditInfo e = (IAuditInfo)entry.Entity;
                if (entry.State == EntityState.Added)
                    e.CreatedOn = DateTime.Now;

                e.ModifiedOn = DateTime.Now;
            }
        }

        public override int SaveChanges()
        {
            ApplyRules();
            Configuration.AutoDetectChangesEnabled = true;
            int i = base.SaveChanges();
            Configuration.AutoDetectChangesEnabled = false;
            return i;
        }
    }
}