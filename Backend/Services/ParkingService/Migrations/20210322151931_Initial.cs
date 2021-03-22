using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ParkingService.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ParkingGarages",
                columns: table => new
                {
                    parkingGarageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    adress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    city = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    postcode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    totalParkingSpots = table.Column<int>(type: "int", nullable: false),
                    freeParkingSpots = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingGarages", x => x.parkingGarageID);
                });

            migrationBuilder.CreateTable(
                name: "parkingSpots",
                columns: table => new
                {
                    parkingSpotID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    parkStatus = table.Column<int>(type: "int", nullable: false),
                    parkingGarageID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_parkingSpots", x => x.parkingSpotID);
                    table.ForeignKey(
                        name: "FK_parkingSpots_ParkingGarages_parkingGarageID",
                        column: x => x.parkingGarageID,
                        principalTable: "ParkingGarages",
                        principalColumn: "parkingGarageID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "reservationTimeSlots",
                columns: table => new
                {
                    reservationTimeSlotID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    startReservation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    endReservation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    parkingSpotID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reservationTimeSlots", x => x.reservationTimeSlotID);
                    table.ForeignKey(
                        name: "FK_reservationTimeSlots_parkingSpots_parkingSpotID",
                        column: x => x.parkingSpotID,
                        principalTable: "parkingSpots",
                        principalColumn: "parkingSpotID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_parkingSpots_parkingGarageID",
                table: "parkingSpots",
                column: "parkingGarageID");

            migrationBuilder.CreateIndex(
                name: "IX_reservationTimeSlots_parkingSpotID",
                table: "reservationTimeSlots",
                column: "parkingSpotID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "reservationTimeSlots");

            migrationBuilder.DropTable(
                name: "parkingSpots");

            migrationBuilder.DropTable(
                name: "ParkingGarages");
        }
    }
}
