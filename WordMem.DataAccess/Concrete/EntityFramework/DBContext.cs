
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WordMem.Entity;

namespace WordMem.DataAccess.Concrete.EntityFramework
{
    public class DBContext:IdentityDbContext<ApplicationUser>
    {
 
        public DbSet<Category> Categories { get; set; }
        public DbSet<Word> Words { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<WordStatistic> WordStatics { get; set; }
        public DbSet<UserSetting> UserSettings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=WordMemDB;integrated security=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WordCategory>()
               .HasKey(pk => new { pk.WordId, pk.CategoryId });
            base.OnModelCreating(modelBuilder);
        }

    }
}
