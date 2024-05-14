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
    public class DriverConfigurations : IEntityTypeConfiguration<Driver>
    {
        public void Configure(EntityTypeBuilder<Driver> builder)
        {
          builder.Property(p=>p.Age);
          builder.Property(p=>p.Picture_License).HasMaxLength(100);
            builder.Property(p=>p.Phone).HasMaxLength(11);
            
        }
    }
}
