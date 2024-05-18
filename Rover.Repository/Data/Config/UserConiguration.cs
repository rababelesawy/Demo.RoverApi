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
    public class UserConiguration : IEntityTypeConfiguration <User>

    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(p => p.UserId).HasMaxLength(100);
            builder.Property(p => p.First_Name).HasMaxLength(100);
            builder.Property(p => p.Last_Name).HasMaxLength(100);
            builder.Property(p => p.Password).HasMaxLength(30);
            builder.Property(p => p.Picture_License).HasMaxLength(100);
            builder.Property(p => p.Picture_User).HasMaxLength(100);
            builder.Property(p => p.Phone).HasMaxLength(100);
            builder.Property(p => p.Email).HasMaxLength(100);
            builder.Property(p => p.Email).HasMaxLength(100);

    




    }
    }
}
