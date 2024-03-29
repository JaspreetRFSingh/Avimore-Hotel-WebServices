﻿using Microsoft.EntityFrameworkCore;
using Northstar.WS.Utility;

#nullable disable

namespace Northstar.WS.Models
{
    /// <summary>
    /// DBContext class for the Avimore DB
    /// </summary>
    public partial class AvimoreDBContext : DbContext
    {
        public AvimoreDBContext()
        {
        }

        public AvimoreDBContext(DbContextOptions<AvimoreDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<FacilityAddress> FacilityAddresses { get; set; }
        public virtual DbSet<Hotel> Hotels { get; set; }
        public virtual DbSet<HotelRoom> HotelRooms { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(CommonConstants.DefaultConnectionStringAvimoreDb);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<FacilityAddress>(entity =>
            {
                entity.HasKey(e => e.AddressId);

                entity.ToTable("FacilityAddress");

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.Country).HasMaxLength(50);

                entity.Property(e => e.Street).HasMaxLength(50);
            });

            modelBuilder.Entity<Hotel>(entity =>
            {
                entity.ToTable("Hotel");

                entity.HasIndex(e => e.LocationId, "Unique_Hotel")
                    .IsUnique();

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.Tagline).IsUnicode(false);

                entity.Property(e => e.Title).IsUnicode(false);

                entity.Property(e => e.Website).IsUnicode(false);

                entity.HasOne(d => d.Location)
                    .WithOne(p => p.Hotel)
                    .HasForeignKey<Hotel>(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Hotel__LocationI__2D27B809");
            });

            modelBuilder.Entity<HotelRoom>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Hotel_Room");

                entity.HasOne(d => d.Hotel)
                    .WithMany()
                    .HasForeignKey(d => d.HotelId)
                    .HasConstraintName("FK__Hotel_Roo__Hotel__300424B4");

                entity.HasOne(d => d.Room)
                    .WithMany()
                    .HasForeignKey(d => d.RoomId)
                    .HasConstraintName("FK__Hotel_Roo__RoomI__30F848ED");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.ToTable("Room");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastModified).HasColumnType("datetime");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.UserRole)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserRoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_UserRole");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("UserRole");

                entity.Property(e => e.UserRoleId).ValueGeneratedNever();

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
