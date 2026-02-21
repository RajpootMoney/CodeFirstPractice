using System.Collections.Generic;
using CodeFirstPractice.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeFirstPractice.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }

    public DbSet<Order> Orders { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Customer>()
            .HasOne(c => c.Profile)
            .WithOne(p => p.Customer)
            .HasForeignKey<CustomerProfile>(cp => cp.CustomerId);

        modelBuilder
            .Entity<Order>()
            .HasMany(o => o.OrderItems)
            .WithOne(o => o.Order)
            .HasForeignKey(oi => oi.OrderId);

        modelBuilder
            .Entity<Order>()
            .Navigation(o => o.OrderItems)
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        modelBuilder
            .Entity<Order>()
            .Metadata.FindNavigation(nameof(Order.OrderItems))
            .SetField("_items");

        modelBuilder.Entity<OrderItem>().HasOne(o => o.Product).WithMany();

        modelBuilder
            .Entity<Product>()
            .OwnsOne(
                p => p.Price,
                money =>
                {
                    money.Property(m => m.Amount).HasColumnName("PriceAmount");

                    money.Property(m => m.Currency).HasColumnName("PriceCurrency").HasMaxLength(3);
                }
            );
    }
}
