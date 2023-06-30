using Microsoft.EntityFrameworkCore;
using ProjektProgramowanie.Model;
using System.Reflection.Metadata;

namespace ProjektProgramowanie
{
    public class DataContext : DbContext
    {
        public DbSet<lokale> lokale { get; set; }
        public DbSet<dania> dania { get; set; }
        public DbSet<opinie> opinie { get; set; }
        public DbSet<promocje> promocje { get; set; }
        public DbSet<oferta> oferta { get; set; }
        public DbSet<promocjelokalu> promocjelokalu { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(@"Data Source=./lokaleFinalSqlite.db");
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<lokale>().HasMany(e => e.Dania).WithMany(e=>e.Lokale).UsingEntity<oferta>();
            modelBuilder.Entity<lokale>().HasMany(e => e.Promocje).WithMany(e => e.Lokale).UsingEntity<promocjelokalu>();
        }
        
    }
}
