using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rover.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rover.Repository.Data.Config
{
    public class Passenger_TripConfigurations : IEntityTypeConfiguration<Passenger_Trips>
    {
        public void Configure(EntityTypeBuilder<Passenger_Trips> builder)
        {
            builder.HasOne(p => p.Trips).WithMany(p => p.Passenger_Trips).HasForeignKey(p => p.TripId);
            builder.HasOne(p => p.Passenger).WithMany(p => p.PassengerTrips).HasForeignKey(p => p.PassengerId);
        }
    }
}
