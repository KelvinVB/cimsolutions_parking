﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ParkingService.Context;

namespace ParkingService.Migrations
{
    [DbContext(typeof(ParkingContext))]
    partial class ParkingContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ParkingService.Models.ParkingGarage", b =>
                {
                    b.Property<int>("parkingGarageID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("city")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("freeParkingSpots")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("postcode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("totalParkingSpots")
                        .HasColumnType("int");

                    b.HasKey("parkingGarageID");

                    b.ToTable("ParkingGarages");
                });

            modelBuilder.Entity("ParkingService.Models.ParkingSpot", b =>
                {
                    b.Property<int>("parkingSpotID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("parkStatus")
                        .HasColumnType("int");

                    b.Property<int>("parkingGarageID")
                        .HasColumnType("int");

                    b.HasKey("parkingSpotID");

                    b.HasIndex("parkingGarageID");

                    b.ToTable("parkingSpots");
                });

            modelBuilder.Entity("ParkingService.Models.ReservationTimeSlot", b =>
                {
                    b.Property<int>("reservationTimeSlotID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("endReservation")
                        .HasColumnType("datetime2");

                    b.Property<int>("parkingSpotID")
                        .HasColumnType("int");

                    b.Property<DateTime>("startReservation")
                        .HasColumnType("datetime2");

                    b.HasKey("reservationTimeSlotID");

                    b.HasIndex("parkingSpotID");

                    b.ToTable("reservationTimeSlots");
                });

            modelBuilder.Entity("ParkingService.Models.ParkingSpot", b =>
                {
                    b.HasOne("ParkingService.Models.ParkingGarage", "parkingGarage")
                        .WithMany("parkingSpots")
                        .HasForeignKey("parkingGarageID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("parkingGarage");
                });

            modelBuilder.Entity("ParkingService.Models.ReservationTimeSlot", b =>
                {
                    b.HasOne("ParkingService.Models.ParkingSpot", "parkingSpot")
                        .WithMany("reservationTimeSlots")
                        .HasForeignKey("parkingSpotID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("parkingSpot");
                });

            modelBuilder.Entity("ParkingService.Models.ParkingGarage", b =>
                {
                    b.Navigation("parkingSpots");
                });

            modelBuilder.Entity("ParkingService.Models.ParkingSpot", b =>
                {
                    b.Navigation("reservationTimeSlots");
                });
#pragma warning restore 612, 618
        }
    }
}
