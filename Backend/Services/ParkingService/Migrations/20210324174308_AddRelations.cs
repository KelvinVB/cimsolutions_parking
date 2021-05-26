using Microsoft.EntityFrameworkCore.Migrations;

namespace ParkingService.Migrations
{
    public partial class AddRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_parkingSpots_ParkingGarages_parkingGarageID",
                table: "parkingSpots");

            migrationBuilder.DropForeignKey(
                name: "FK_reservationTimeSlots_parkingSpots_parkingSpotID",
                table: "reservationTimeSlots");

            migrationBuilder.AlterColumn<int>(
                name: "parkingSpotID",
                table: "reservationTimeSlots",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "parkingGarageID",
                table: "parkingSpots",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "postcode",
                table: "ParkingGarages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "city",
                table: "ParkingGarages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "address",
                table: "ParkingGarages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_parkingSpots_ParkingGarages_parkingGarageID",
                table: "parkingSpots",
                column: "parkingGarageID",
                principalTable: "ParkingGarages",
                principalColumn: "parkingGarageID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_reservationTimeSlots_parkingSpots_parkingSpotID",
                table: "reservationTimeSlots",
                column: "parkingSpotID",
                principalTable: "parkingSpots",
                principalColumn: "parkingSpotID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_parkingSpots_ParkingGarages_parkingGarageID",
                table: "parkingSpots");

            migrationBuilder.DropForeignKey(
                name: "FK_reservationTimeSlots_parkingSpots_parkingSpotID",
                table: "reservationTimeSlots");

            migrationBuilder.AlterColumn<int>(
                name: "parkingSpotID",
                table: "reservationTimeSlots",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "parkingGarageID",
                table: "parkingSpots",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "postcode",
                table: "ParkingGarages",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "city",
                table: "ParkingGarages",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "address",
                table: "ParkingGarages",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_parkingSpots_ParkingGarages_parkingGarageID",
                table: "parkingSpots",
                column: "parkingGarageID",
                principalTable: "ParkingGarages",
                principalColumn: "parkingGarageID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_reservationTimeSlots_parkingSpots_parkingSpotID",
                table: "reservationTimeSlots",
                column: "parkingSpotID",
                principalTable: "parkingSpots",
                principalColumn: "parkingSpotID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
