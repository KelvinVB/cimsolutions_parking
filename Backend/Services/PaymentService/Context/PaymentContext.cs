using Microsoft.EntityFrameworkCore;
using PaymentService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentService.Context
{
    public class PaymentContext : DbContext
    {
        public PaymentContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Customer> customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //ParkingGarage
            modelBuilder.Entity<Customer>(c =>
            {
                c.HasKey(e => e.id);
                c.Property(e => e.id).ValueGeneratedOnAdd();
                c.Property(e => e.accountId).IsRequired();
                c.Property(e => e.customerId).IsRequired();
            });

            //ParkingGarage data
            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    id = 1,
                    accountId = "accountId",
                    customerId = "customerId"
                }
            );
        }
    }
}
