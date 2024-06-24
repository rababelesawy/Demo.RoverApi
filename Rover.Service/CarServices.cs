using Microsoft.EntityFrameworkCore;
using Rover.Core.Dtos;
using Rover.Core.Entities;
using Rover.Core.Interfaces;
using Rover.Core.Service.Contract;
using Rover.Repository.Data;
using Rover.Repository.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rover.Service
{
    public class CarServices : ICarServices
    {
        private readonly IGenericRepository<Car> _carRepo;
        private readonly IUsersServices _usersServices;
        private readonly StoreContext _context;

        public CarServices(IGenericRepository<Car> CarRepo , IUsersServices usersServices , StoreContext context)
        {
            _carRepo = CarRepo;
           _usersServices = usersServices;
            _context = context;
        }


        //Create Car
        public async Task<int> CreateCarAsync(Car car)
        {

          
            await _carRepo.SaveAsync(car);



            return (car.Id);
        }


        // Get car by id 
        public async Task<Car> GetCarByIdAsync(int id)
        {
            return await _carRepo.GetByIdAsync(id);
        }



        // Update car
        public async Task<string> UpdateCarAsync(Car car)
        {
            await _carRepo.Edit(car);

            return ("Sucssessfull update");
        }




        // DeleteCar

        public async Task<bool> DeleteCarAsync(Car car)
        {

           car.DeleteDate = DateTime.Now;
            _carRepo.Delete(car);

            return true;
        }

        public async Task<CarDto> GetcarbyCarNumber(string carnumber)
        {
            try
            {

                var car = await _context.Cars.FirstOrDefaultAsync(u => u.CarNumber == carnumber);
                if (car != null)
                {
                    return new CarDto()
                    
                    {
                        Id = car.Id,
                       Picture_Car = car.Picture_Car,
                       License_Car = car.License_Car,
                        Model=car.Model,
                        Description = car.Description,
                        UserId = car.DriverId,
                        Driver_License_Picture = car.Driver_License_Picture,

                        CarNumber= car.CarNumber,
             

                    };
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
