using Microsoft.EntityFrameworkCore;
using ParkingService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingService.Context
{
    public class ParkingContext : DbContext
    {
        public ParkingContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<ParkingGarage> ParkingGarages { get; set; }
        public DbSet<ParkingSpot> parkingSpots { get; set; }
        public DbSet<ReservationTimeSlot> reservationTimeSlots { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //parkingGarage
            modelBuilder.Entity<ParkingGarage>(pg =>
            {
                pg.HasKey(e => e.parkingGarageID);
                pg.Property(e => e.parkingGarageID).ValueGeneratedOnAdd();
                pg.Property(e => e.parkingGarageID).IsRequired();
                pg.Property(e => e.name).IsRequired();
            });
            //ParkingSpot
            modelBuilder.Entity<ParkingSpot>(ps =>
            {
                ps.HasKey(e => e.parkingSpotID);
                ps.Property(e => e.parkingSpotID).ValueGeneratedOnAdd();
                ps.Property(e => e.parkingSpotID).IsRequired();
                ps.Property(e => e.name).IsRequired();
            });
            //ReservationTimeSlot
            modelBuilder.Entity<ReservationTimeSlot>(rt =>
            {
                rt.HasKey(e => e.reservationTimeSlotID);
                rt.Property(e => e.reservationTimeSlotID).ValueGeneratedOnAdd();
                rt.Property(e => e.reservationTimeSlotID).IsRequired();
                rt.Property(e => e.startReservation).IsRequired();
                rt.Property(e => e.endReservation).IsRequired();
            });
        }
    }
}
