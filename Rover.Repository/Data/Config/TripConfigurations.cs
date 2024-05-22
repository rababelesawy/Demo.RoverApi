﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rover.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Rover.Repository.Data.Config
{
    public class TripConfigurations : IEntityTypeConfiguration<Trip>
    {
        public void Configure(EntityTypeBuilder<Trip> builder)
        {
           builder.Property(p=>p.From).HasMaxLength(50);
            builder.Property(p=>p.To).HasMaxLength(50);
            builder.Property(p=>p.SeatsAvaliable);
            builder.Property(p => p.Price).HasColumnType("decimal(18, 2)");
            builder.Property(p => p.CarNumber).HasMaxLength(50);
            builder.Property(p=>p.Gender).HasMaxLength(50);
            builder.Property(e => e.Time).HasColumnType("time");
            builder.Property(e => e.Date).HasColumnType("date");
            builder.Property(e => e.Expected_Arrivale).HasColumnType("time");






            builder.HasOne(p => p.Car).WithMany(p => p.Trips).HasForeignKey(p => p.CarId);
            builder.HasOne(p => p.Status).WithMany(p => p.Trips).HasForeignKey(p => p.StatusId);

        }
    }
}
