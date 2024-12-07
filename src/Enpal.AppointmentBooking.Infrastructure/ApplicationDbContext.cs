using System;
using Enpal.AppointmentBooking.Core.Entities;
using Enpal.AppointmentBooking.Core.Interface;
using Microsoft.EntityFrameworkCore;

namespace Enpal.AppointmentBooking.Infrastructure;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options),
        IApplicationDbContext
{
    public DbSet<SalesManager> SalesManager { get; set; }
    public DbSet<Slot> Slot { get; set; }

    public IQueryable<T> Set<T>()
        where T : class
    {
        return base.Set<T>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SalesManager>(entity =>
        {
            entity.ToTable("sales_managers");

            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name").HasMaxLength(250).IsRequired();

            entity
                .Property(e => e.Languages)
                .HasColumnName("languages")
                .HasColumnType("character varying[]");

            entity
                .Property(e => e.Products)
                .HasColumnName("products")
                .HasColumnType("character varying[]");

            entity
                .Property(e => e.CustomerRatings)
                .HasColumnName("customer_ratings")
                .HasColumnType("character varying[]")
                .IsRequired();

            entity
                .HasMany(e => e.Slots)
                .WithOne(e => e.SalesManager)
                .HasForeignKey(e => e.SalesManagerId);
        });

        modelBuilder.Entity<Slot>(entity =>
        {
            entity.ToTable("slots");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.StartDate).HasColumnName("start_date").IsRequired();

            entity.Property(e => e.EndDate).HasColumnName("end_date").IsRequired();
            entity.Property(e => e.Booked).HasColumnName("booked").IsRequired();

            entity
                .HasOne(e => e.SalesManager)
                .WithMany(e => e.Slots)
                .HasForeignKey(e => e.SalesManagerId)
                .HasConstraintName("fk_sales_manager_id");
        });
    }
}
