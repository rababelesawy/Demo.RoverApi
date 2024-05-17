using Microsoft.EntityFrameworkCore;
using Rover.Core;
using Rover.Core.Entities;
using Rover.Core.Interfaces;
using Rover.Core.Service.Contract;
using Rover.Repository.Data;
using Rover.Repository.GenericRepository;
using Rover.Service;
namespace Demo.RoverApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            #region
            //builder.Services.AddScoped(typeof(IGenericRepository<Passenger>), typeof(GenericRepository<Passenger>));
            //builder.Services.AddScoped(typeof(IGenericRepository<Trip>), typeof(GenericRepository<Trip>));
            //builder.Services.AddScoped(typeof(IGenericRepository<Car>), typeof(GenericRepository<Car>));
            //builder.Services.AddScoped(typeof(IGenericRepository<Driver>), typeof(GenericRepository<Driver>));

            #endregion
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            builder.Services.AddScoped(typeof(ITripService), typeof(TripService));
            builder.Services.AddScoped(typeof(IDriverService), typeof(DriverServices));
            builder.Services.AddScoped(typeof(ICarServices), typeof(CarServices));
            builder.Services.AddScoped(typeof(IPassengerServices), typeof(PassengerServices));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}