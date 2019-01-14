using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using GeoStat.Entities;
using Microsoft.Azure.Mobile.Server.Tables;
namespace GeoStat.DataAccess
{
    public class GeoStatContext : IdentityDbContext<User>
    {
        public GeoStatContext(string connectionString) : base(connectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add(
                new AttributeToColumnAnnotationConvention<TableColumnAttribute, string>(
                    "ServiceTableColumn",
                    (property, attributes) => attributes.Single().ColumnType.ToString()));
        }

        public DbSet<Location> Locations { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupUser> GroupUsers { get; set; }
    }
}
