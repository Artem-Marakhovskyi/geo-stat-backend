using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using GeoStat.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace GeoStat.DataAccess
{
    public class GeoStatContext : IdentityDbContext<User>
    {
        public GeoStatContext(DbContextOptions<GeoStatContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GroupUser>().HasKey(sc => new { sc.UserId, sc.GroupId });
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Location> Locations { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupUser> GroupUsers { get; set; }
    }
}
