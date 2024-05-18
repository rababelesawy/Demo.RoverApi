using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rover.Repository.Migrations
{
    /// <inheritdoc />
    public partial class DeleteDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDate",
                table: "Passengers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "Passengers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDate",
                table: "Drivers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "Drivers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDate",
                table: "Cars",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "Cars",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeleteDate",
                table: "Passengers");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Passengers");

            migrationBuilder.DropColumn(
                name: "DeleteDate",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "DeleteDate",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Cars");
        }
    }
}
