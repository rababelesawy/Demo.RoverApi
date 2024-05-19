using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rover.Repository.Migrations
{
    /// <inheritdoc />
    public partial class updatepropintrip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Expected_Arrivale",
                table: "Trips",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Expected_Arrivale",
                table: "Trips");
        }
    }
}
