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
            //ParkingGarage
            modelBuilder.Entity<ParkingGarage>(pg =>
            {
                pg.HasKey(e => e.parkingGarageID);
                pg.Property(e => e.parkingGarageID).ValueGeneratedOnAdd();
                pg.Property(e => e.parkingGarageID).IsRequired();
                pg.Property(e => e.name).IsRequired();
                pg.Property(e => e.address).IsRequired();
                pg.Property(e => e.city).IsRequired();
                pg.Property(e => e.postcode).IsRequired();
            });
            //ParkingSpot
            modelBuilder.Entity<ParkingSpot>(ps =>
            {
                ps.HasKey(e => e.parkingSpotID);
                ps.Property(e => e.parkingSpotID).ValueGeneratedOnAdd();
                ps.Property(e => e.parkingSpotID).IsRequired();
                ps.Property(e => e.name).IsRequired();
                ps.HasOne(e => e.parkingGarage).WithMany(e => e.parkingSpots).HasForeignKey(e => e.parkingGarageID).IsRequired();
            });
            //ReservationTimeSlot
            modelBuilder.Entity<ReservationTimeSlot>(rt =>
            {
                rt.HasKey(e => e.reservationTimeSlotID);
                rt.Property(e => e.reservationTimeSlotID).ValueGeneratedOnAdd();
                rt.Property(e => e.reservationTimeSlotID).IsRequired();
                rt.Property(e => e.startReservation).IsRequired();
                rt.Property(e => e.endReservation).IsRequired();
                rt.HasOne(e => e.parkingSpot).WithMany(e => e.reservationTimeSlots).HasForeignKey(e => e.parkingSpotID).IsRequired();
            });

            //ParkingGarage data
            modelBuilder.Entity<ParkingGarage>().HasData(
                new ParkingGarage
                {
                    parkingGarageID = 1,
                    name = "CimParking",
                    address = "De Waal 21b",
                    city = "Best",
                    postcode = "5684 PH",
                    totalParkingSpots = 150
                }
            ); ;

            //ParkingSpot data
            modelBuilder.Entity<ParkingSpot>().HasData(
                new ParkingSpot
                {
                    parkingSpotID = 1,
                    name = "A1",
                    parkingGarageID = 1
                },
                new ParkingSpot
                {
                    parkingSpotID = 2,
                    name = "A2",
                    parkingGarageID = 1
                },
                new ParkingSpot
                {
                    parkingSpotID = 3,
                    name = "A3",
                    parkingGarageID = 1
                }
            );

            //ReservationTimeSlot data
            modelBuilder.Entity<ReservationTimeSlot>().HasData(
                new ReservationTimeSlot
                {
                    reservationTimeSlotID = 1,
                    startReservation = new DateTime(2021, 3, 26, 10, 30, 00),
                    endReservation = new DateTime(2021, 3, 26, 11, 30, 00),
                    parkingSpotID = 1
                },
                new ReservationTimeSlot
                {
                    reservationTimeSlotID = 2,
                    startReservation = new DateTime(2021, 3, 27, 10, 00, 00),
                    endReservation = new DateTime(2021, 3, 27, 10, 00, 00),
                    parkingSpotID = 1
                },
                new ReservationTimeSlot
                {
                    reservationTimeSlotID = 3,
                    startReservation = new DateTime(2021, 3, 26, 10, 30, 00),
                    endReservation = new DateTime(2021, 3, 26, 10, 30, 00),
                    parkingSpotID = 2
                }
            );
        }
    }
}
