using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rover.Repository.Migrations
{
    /// <inheritdoc />
    public partial class updatedateinbaseentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDate",
                table: "Trip_Status",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "Trip_Status",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDate",
                table: "Passenger_Trips",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "Passenger_Trips",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeleteDate",
                table: "Trip_Status");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Trip_Status");

            migrationBuilder.DropColumn(
                name: "DeleteDate",
                table: "Passenger_Trips");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Passenger_Trips");
        }
    }
}
