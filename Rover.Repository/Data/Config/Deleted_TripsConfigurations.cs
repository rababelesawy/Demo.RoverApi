using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rover.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rover.Repository.Data.Config
{
    public class Deleted_TripsConfigurations : IEntityTypeConfiguration<Deleted_Trips>

    {
        public void Configure(EntityTypeBuilder<Deleted_Trips> builder)
        {
            builder.HasOne(p => p.Trip).WithMany(p => p.DeletedTrips).HasForeignKey(p => p.TripId);
            builder.HasOne(p => p.User).WithMany(p => p.DeletedTrips).HasForeignKey(p => p.UserId);
        }
    }
}
