using Dsw2026Ej15.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dsw2026Ej15.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Doctor> Doctors => Set<Doctor>();
        public DbSet<Speciality> Specialities => Set<Speciality>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Speciality>(entity =>
            {
                entity.ToTable("Specialities");

                entity.HasKey(s => s.Id);

                entity.Property(s => s.Id)
                    .ValueGeneratedNever();

                entity.Property(s => s.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(s => s.Description)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.ToTable("Doctors");

                entity.HasKey(d => d.Id);

                entity.Property(d => d.Id)
                    .ValueGeneratedNever();

                entity.Property(d => d.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(d => d.LicenseNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(d => d.IsActive)
                    .IsRequired();

                entity.HasOne(d => d.Speciality)
                    .WithMany()
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
