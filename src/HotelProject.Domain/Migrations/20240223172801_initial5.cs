using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelProject.Domain.Migrations
{
    /// <inheritdoc />
    public partial class initial5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PriceOnePerson",
                table: "RoomTypes",
                newName: "PriceOneNight");

            migrationBuilder.AddColumn<int>(
                name: "MaxPersons",
                table: "RoomTypes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CheckoutDate",
                table: "Bookings",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CheckinDate",
                table: "Bookings",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxPersons",
                table: "RoomTypes");

            migrationBuilder.RenameColumn(
                name: "PriceOneNight",
                table: "RoomTypes",
                newName: "PriceOnePerson");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CheckoutDate",
                table: "Bookings",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CheckinDate",
                table: "Bookings",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }
    }
}
