using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using GeoStat.Entities;

namespace GeoStat.DataAccess
{
    public class GeoStatContext: DbContext
    {
        public GeoStatContext(DbContextOptions<GeoStatContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GroupUser>().HasKey(sc => new { sc.UserId, sc.GroupId });
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupUser> GroupUsers { get; set; }
    }
}
