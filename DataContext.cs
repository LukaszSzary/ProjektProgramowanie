using Microsoft.EntityFrameworkCore;
using ProjektProgramowanie.Model;

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
            modelBuilder.Entity<oferta>().HasKey(sc => new { sc.LokaleId,sc.DaniaId});
            modelBuilder.Entity<promocjelokalu>().HasKey(sc => new { sc.LokaleId, sc.PromocjeId });
        }
    }
}
