using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ParkingService.Migrations
{
    public partial class addedSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ParkingGarages",
                columns: new[] { "parkingGarageID", "address", "city", "freeParkingSpots", "name", "postcode", "totalParkingSpots" },
                values: new object[] { 1, "De Waal 21b", "Best", 0, "CimParking", "5684 PH", 150 });

            migrationBuilder.InsertData(
                table: "parkingSpots",
                columns: new[] { "parkingSpotID", "name", "parkStatus", "parkingGarageID" },
                values: new object[] { 1, "A1", 0, 1 });

            migrationBuilder.InsertData(
                table: "parkingSpots",
                columns: new[] { "parkingSpotID", "name", "parkStatus", "parkingGarageID" },
                values: new object[] { 2, "A2", 0, 1 });

            migrationBuilder.InsertData(
                table: "parkingSpots",
                columns: new[] { "parkingSpotID", "name", "parkStatus", "parkingGarageID" },
                values: new object[] { 3, "A3", 0, 1 });

            migrationBuilder.InsertData(
                table: "reservationTimeSlots",
                columns: new[] { "reservationTimeSlotID", "endReservation", "parkingSpotID", "startReservation" },
                values: new object[] { 1, new DateTime(2021, 3, 26, 11, 30, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2021, 3, 26, 10, 30, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "reservationTimeSlots",
                columns: new[] { "reservationTimeSlotID", "endReservation", "parkingSpotID", "startReservation" },
                values: new object[] { 2, new DateTime(2021, 3, 27, 10, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2021, 3, 27, 10, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "reservationTimeSlots",
                columns: new[] { "reservationTimeSlotID", "endReservation", "parkingSpotID", "startReservation" },
                values: new object[] { 3, new DateTime(2021, 3, 26, 10, 30, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2021, 3, 26, 10, 30, 0, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "parkingSpots",
                keyColumn: "parkingSpotID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "reservationTimeSlots",
                keyColumn: "reservationTimeSlotID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "reservationTimeSlots",
                keyColumn: "reservationTimeSlotID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "reservationTimeSlots",
                keyColumn: "reservationTimeSlotID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "parkingSpots",
                keyColumn: "parkingSpotID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "parkingSpots",
                keyColumn: "parkingSpotID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ParkingGarages",
                keyColumn: "parkingGarageID",
                keyValue: 1);
        }
    }
}
