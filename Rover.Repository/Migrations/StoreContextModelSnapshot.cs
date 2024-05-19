﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Rover.Repository.Data;

#nullable disable

namespace Rover.Repository.Migrations
{
    [DbContext(typeof(StoreContext))]
    partial class StoreContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Rover.Core.Entities.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CarNumber")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("DeleteDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("DriverId")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Driver_License_Picture")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<long?>("License_Car")
                        .HasMaxLength(100)
                        .HasColumnType("bigint");

                    b.Property<string>("Model")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Picture_Car")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Picture_License")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("DriverId");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("Rover.Core.Entities.Passenger_Trips", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DeleteDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PassengerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("TripId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("PassengerId");

                    b.HasIndex("TripId");

                    b.ToTable("User_Trips");
                });

            modelBuilder.Entity("Rover.Core.Entities.Trip", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CarId")
                        .HasColumnType("int");

                    b.Property<string>("CarNumber")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeleteDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DriverId")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("Expected_Arrivale")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Expected_Arrivale")
                        .HasColumnType("datetime2");

                    b.Property<string>("From")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("Gender")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.Property<long?>("Latitude")
                        .HasColumnType("bigint");

                    b.Property<long?>("Longitude")
                        .HasColumnType("bigint");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int?>("SeatsAvaliable")
                        .HasColumnType("int");

                    b.Property<int?>("StatusId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Time")
                        .HasColumnType("datetime2");

                    b.Property<string>("To")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.HasIndex("DriverId");

                    b.HasIndex("StatusId");

                    b.ToTable("Trips");
                });

            modelBuilder.Entity("Rover.Core.Entities.TripStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DeleteDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Trip_Status");
                });

            modelBuilder.Entity("Rover.Core.Entities.User", b =>
                {
                    b.Property<string>("User_Id")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("Age")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeleteDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("First_Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("Last_Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Phone")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("Type")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("User_Picture")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("User_Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Rover.Core.Entities.Car", b =>
                {
                    b.HasOne("Rover.Core.Entities.User", "Driver")
                        .WithMany()
                        .HasForeignKey("DriverId");

                    b.Navigation("Driver");
                });

            modelBuilder.Entity("Rover.Core.Entities.Passenger_Trips", b =>
                {
                    b.HasOne("Rover.Core.Entities.User", "Passenger")
                        .WithMany("PassengerTrips")
                        .HasForeignKey("PassengerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Rover.Core.Entities.Trip", "Trips")
                        .WithMany("Passenger_Trips")
                        .HasForeignKey("TripId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Passenger");

                    b.Navigation("Trips");
                });

            modelBuilder.Entity("Rover.Core.Entities.Trip", b =>
                {
                    b.HasOne("Rover.Core.Entities.Car", "Car")
                        .WithMany("Trips")
                        .HasForeignKey("CarId");

                    b.HasOne("Rover.Core.Entities.User", "Driver")
                        .WithMany("Trips")
                        .HasForeignKey("DriverId");

                    b.HasOne("Rover.Core.Entities.TripStatus", "Status")
                        .WithMany("Trips")
                        .HasForeignKey("StatusId");

                    b.Navigation("Car");

                    b.Navigation("Driver");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("Rover.Core.Entities.Car", b =>
                {
                    b.Navigation("Trips");
                });

            modelBuilder.Entity("Rover.Core.Entities.Trip", b =>
                {
                    b.Navigation("Passenger_Trips");
                });

            modelBuilder.Entity("Rover.Core.Entities.TripStatus", b =>
                {
                    b.Navigation("Trips");
                });

            modelBuilder.Entity("Rover.Core.Entities.User", b =>
                {
                    b.Navigation("PassengerTrips");

                    b.Navigation("Trips");
                });
#pragma warning restore 612, 618
        }
    }
}
