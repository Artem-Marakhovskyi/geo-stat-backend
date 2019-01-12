using System.Data.Entity;
using GeoStat.Entities;

namespace GeoStat.DataAccess
{
    public class GeoStatContext: DbContext
    {
        public GeoStatContext(string connectionString) : base(connectionString)
        {
            Database.CreateIfNotExists();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GroupUser>().HasKey(sc => new { sc.UserId, sc.GroupId });
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupUser> GroupUsers { get; set; }
    }
}
