using Microsoft.EntityFrameworkCore.Migrations;

namespace ParkingService.Migrations
{
    public partial class AddedLicensePlate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "accountID",
                table: "reservationTimeSlots",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "licensePlateNumber",
                table: "reservationTimeSlots",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "reservationTimeSlots",
                keyColumn: "reservationTimeSlotID",
                keyValue: 1,
                column: "licensePlateNumber",
                value: "A-111-AA");

            migrationBuilder.UpdateData(
                table: "reservationTimeSlots",
                keyColumn: "reservationTimeSlotID",
                keyValue: 2,
                column: "licensePlateNumber",
                value: "A-111-AA");

            migrationBuilder.UpdateData(
                table: "reservationTimeSlots",
                keyColumn: "reservationTimeSlotID",
                keyValue: 3,
                column: "licensePlateNumber",
                value: "A-111-AA");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "accountID",
                table: "reservationTimeSlots");

            migrationBuilder.DropColumn(
                name: "licensePlateNumber",
                table: "reservationTimeSlots");
        }
    }
}
