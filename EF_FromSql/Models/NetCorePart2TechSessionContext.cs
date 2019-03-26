using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EF_FromSql.Models
{
    public partial class NetCorePart2TechSessionContext : DbContext
    {
        public NetCorePart2TechSessionContext()
        {
        }

        public NetCorePart2TechSessionContext(DbContextOptions<NetCorePart2TechSessionContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Articulos> Articulos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=JOSEBELTRANC1D2\\SQLEXPRESS;Database=NetCorePart2TechSession;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Articulos>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }
    }
}
