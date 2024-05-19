using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rover.Repository.Migrations
{
    /// <inheritdoc />
    public partial class editformillion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Drivers_DriverId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Passenger_Trips_Passengers_PassengerId",
                table: "Passenger_Trips");

            migrationBuilder.DropForeignKey(
                name: "FK_Passenger_Trips_Trips_TripId",
                table: "Passenger_Trips");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Drivers_DriverId",
                table: "Trips");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "Passengers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Passenger_Trips",
                table: "Passenger_Trips");

            migrationBuilder.RenameTable(
                name: "Passenger_Trips",
                newName: "User_Trips");

            migrationBuilder.RenameIndex(
                name: "IX_Passenger_Trips_TripId",
                table: "User_Trips",
                newName: "IX_User_Trips_TripId");

            migrationBuilder.RenameIndex(
                name: "IX_Passenger_Trips_PassengerId",
                table: "User_Trips",
                newName: "IX_User_Trips_PassengerId");

            migrationBuilder.AlterColumn<string>(
                name: "DriverId",
                table: "Trips",
                type: "nvarchar(100)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Expected_Arrivale",
                table: "Trips",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Latitude",
                table: "Trips",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Longitude",
                table: "Trips",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DriverId",
                table: "Cars",
                type: "nvarchar(100)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CarNumber",
                table: "Cars",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Driver_License_Picture",
                table: "Cars",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TripId",
                table: "User_Trips",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PassengerId",
                table: "User_Trips",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_User_Trips",
                table: "User_Trips",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    User_Id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    User_Picture = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    First_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Last_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.User_Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Users_DriverId",
                table: "Cars",
                column: "DriverId",
                principalTable: "Users",
                principalColumn: "User_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Users_DriverId",
                table: "Trips",
                column: "DriverId",
                principalTable: "Users",
                principalColumn: "User_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Trips_Trips_TripId",
                table: "User_Trips",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Trips_Users_PassengerId",
                table: "User_Trips",
                column: "PassengerId",
                principalTable: "Users",
                principalColumn: "User_Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Users_DriverId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Users_DriverId",
                table: "Trips");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Trips_Trips_TripId",
                table: "User_Trips");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Trips_Users_PassengerId",
                table: "User_Trips");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User_Trips",
                table: "User_Trips");

            migrationBuilder.DropColumn(
                name: "Expected_Arrivale",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "CarNumber",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Driver_License_Picture",
                table: "Cars");

            migrationBuilder.RenameTable(
                name: "User_Trips",
                newName: "Passenger_Trips");

            migrationBuilder.RenameIndex(
                name: "IX_User_Trips_TripId",
                table: "Passenger_Trips",
                newName: "IX_Passenger_Trips_TripId");

            migrationBuilder.RenameIndex(
                name: "IX_User_Trips_PassengerId",
                table: "Passenger_Trips",
                newName: "IX_Passenger_Trips_PassengerId");

            migrationBuilder.AlterColumn<int>(
                name: "DriverId",
                table: "Trips",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DriverId",
                table: "Cars",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TripId",
                table: "Passenger_Trips",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PassengerId",
                table: "Passenger_Trips",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Passenger_Trips",
                table: "Passenger_Trips",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Age = table.Column<int>(type: "int", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Phone = table.Column<long>(type: "bigint", maxLength: 11, nullable: true),
                    Picture_License = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Passengers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Age = table.Column<int>(type: "int", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Phone = table.Column<long>(type: "bigint", maxLength: 11, nullable: true),
                    Picture_Passanger = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passengers", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Drivers_DriverId",
                table: "Cars",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Passenger_Trips_Passengers_PassengerId",
                table: "Passenger_Trips",
                column: "PassengerId",
                principalTable: "Passengers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Passenger_Trips_Trips_TripId",
                table: "Passenger_Trips",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Drivers_DriverId",
                table: "Trips",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id");
        }
    }
}
