using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Rover.Core.Entities;

using Rover.Repository.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Rover.Repository.Data
{
    public  class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options):base(options)
        
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
     
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet <Passenger_Trips> Passenger_Trips { get; set; }
        public DbSet<TripStatus> Trip_Status { get; set; }




    }
}
