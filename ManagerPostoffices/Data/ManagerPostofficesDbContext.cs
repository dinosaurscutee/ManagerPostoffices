using ManagerPostoffices.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace ManagerPostoffices.Data
{
    public class ManagerPostofficesDbContext : DbContext
    {
        public ManagerPostofficesDbContext(DbContextOptions options) : base(options)
        {
        }

        // Constructor này dành cho các hoạt động thiết kế
        public ManagerPostofficesDbContext()
        {
        }

        public DbSet<Recipient> Recipients { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Transport> Transports { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<DeliveryStatus> DeliveryStatuses { get; set; }
        public DbSet<PackageDeliveryHistory> PackageDeliveryHistory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>()
                .HasOne(a => a.Recipient)
                .WithMany(r => r.Addresses)
                .HasForeignKey(a => a.RecipientId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Package>()
                .HasOne(p => p.Recipient)
                .WithMany(r => r.Packages)
                .HasForeignKey(p => p.RecipientId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Package>()
                .HasOne(p => p.Address)
                .WithMany()
                .HasForeignKey(p => p.AddressId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Package>()
                .HasOne(p => p.Transport)
                .WithMany(t => t.Packages)
                .HasForeignKey(p => p.TransportId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DeliveryStatus>()
                .HasMany(ds => ds.PackageDeliveryHistory)
                .WithOne(pd => pd.DeliveryStatus)
                .HasForeignKey(pd => pd.DeliveryStatusId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PackageDeliveryHistory>()
                .HasKey(pd => new { pd.PackageId, pd.DeliveryStatusId });

            modelBuilder.Entity<PackageDeliveryHistory>()
                .HasOne(pd => pd.Package)
                .WithMany(p => p.PackageDeliveryHistory)
                .HasForeignKey(pd => pd.PackageId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PackageDeliveryHistory>()
                .HasOne(pd => pd.DeliveryStatus)
                .WithMany(ds => ds.PackageDeliveryHistory)
                .HasForeignKey(pd => pd.DeliveryStatusId)
                .OnDelete(DeleteBehavior.Restrict);

            // Gọi phương thức SeedData để thêm dữ liệu mẫu
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Recipients
            modelBuilder.Entity<Recipient>().HasData(
                new Recipient { RecipientId = 1, Name = "John Doe", ContactInfo = "john@example.com" },
                new Recipient { RecipientId = 2, Name = "Jane Smith", ContactInfo = "jane@example.com" }
            );

            // Seed Addresses
            modelBuilder.Entity<Address>().HasData(
                new Address { AddressId = 1, Street = "123 Main St", City = "City1", State = "State1", RecipientId = 1 },
                new Address { AddressId = 2, Street = "456 Oak St", City = "City2", State = "State2", RecipientId = 2 }
            );

            // Seed Transports
            modelBuilder.Entity<Transport>().HasData(
                new Transport { TransportId = 1, Name = "Carrier1" },
                new Transport { TransportId = 2, Name = "Carrier2" }
            );

            // Seed Packages
            modelBuilder.Entity<Package>().HasData(
                new Package { PackageId = 1, Size = "Small", Weight = 2.5, Value = 50, RecipientId = 1, AddressId = 1, TransportId = 1 },
                new Package { PackageId = 2, Size = "Large", Weight = 5.0, Value = 100, RecipientId = 2, AddressId = 2, TransportId = 2 }
            );

            // Seed DeliveryStatuses
            modelBuilder.Entity<DeliveryStatus>().HasData(
                new DeliveryStatus
                {
                    DeliveryStatusId = 1,
                    Status = "In Progress",
                    StatusDescription = "Package is being processed",
                    UpdateTime = DateTime.Now,
                    TimeOutForDelivery = null,
                    TimeDelivered = null,
                    TimeCancelled = null
                },
                new DeliveryStatus
                {
                    DeliveryStatusId = 2,
                    Status = "Delivered",
                    StatusDescription = "Package has been delivered",
                    UpdateTime = DateTime.Now,
                    TimeOutForDelivery = DateTime.Now.AddHours(1),
                    TimeDelivered = DateTime.Now,
                    TimeCancelled = null
                },
                new DeliveryStatus
                {
                    DeliveryStatusId = 3,
                    Status = "Cancelled",
                    StatusDescription = "Package has been cancelled",
                    UpdateTime = DateTime.Now,
                    TimeOutForDelivery = null,
                    TimeDelivered = null,
                    TimeCancelled = DateTime.Now
                }
            );

            modelBuilder.Entity<PackageDeliveryHistory>().HasData(
                new PackageDeliveryHistory { PackageId = 1, DeliveryStatusId = 1, TimeStamp = DateTime.Now, IsCurrentStatus = true },
                new PackageDeliveryHistory { PackageId = 2, DeliveryStatusId = 2, TimeStamp = DateTime.Now, IsCurrentStatus = true },
                new PackageDeliveryHistory { PackageId = 3, DeliveryStatusId = 3, TimeStamp = DateTime.Now, IsCurrentStatus = true }
            );

        }
    }
}
