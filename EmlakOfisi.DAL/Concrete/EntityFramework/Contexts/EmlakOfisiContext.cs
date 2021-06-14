using EmlakOfisi.Entities.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmlakOfisi.DAL.Concrete.EntityFramework.Contexts
{
    public class EmlakOfisiContext:IdentityDbContext<User,Role,int>
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server =.; Database = EMLAKOFS3; Trusted_Connection = True; ");
        }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<CompanyUser> CompanyUsers { get; set; }
        public virtual DbSet<NumberOfRoom> NumberOfRooms { get; set; }
        public virtual DbSet<RealEstateAd> RealEstateAds { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Company>(entity =>
            {
                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            builder.Entity<CompanyUser>(entity =>
            {
                entity.HasKey(e => new { e.CompanyId, e.UserId });

                entity.ToTable("Company_Users");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanyUsers)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Company_Users_Companies");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CompanyUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Company_Users_AspNetUsers");
            });

            builder.Entity<NumberOfRoom>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(10);
            });

            builder.Entity<RealEstateAd>(entity =>
            {
                entity.HasIndex(e => e.NumberOfRoomsId);

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.AdName)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ImagePath).HasMaxLength(150);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.HasOne(d => d.NumberOfRooms)
                    .WithMany(p => p.RealEstateAds)
                    .HasForeignKey(d => d.NumberOfRoomsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RealEstateAds_NumberOfRooms");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RealEstateAds)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RealEstateAds_AspNetUsers1");
            });
        }
    }
}
