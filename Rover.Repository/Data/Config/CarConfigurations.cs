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
    public class CarConfigurations : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.Property(p=>p.Picture_Car).HasMaxLength(100);
            builder.Property(p => p.Picture_License).HasMaxLength(100);
            builder.Property(p=>p.Driver_License_Picture).HasMaxLength(100);
            builder.Property(p => p.License_Car).HasMaxLength(100);
            builder.Property(p => p.CarNumber).HasMaxLength(50);
            builder.Property(p => p.Model).HasMaxLength(30);
            builder.Property(p => p.Description).HasMaxLength(100);

            

        }
    }
}
