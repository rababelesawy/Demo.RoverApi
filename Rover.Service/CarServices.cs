using Rover.Core.Entities;
using Rover.Core.Interfaces;
using Rover.Core.Service.Contract;
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

        public CarServices(IGenericRepository<Car> CarRepo)
        {
            _carRepo = CarRepo;
        }


        //Create Car
        public async Task<int> CreateCarAsync(Car car)
        {
            await _carRepo.SaveAsync(car);



            return (car.Id);
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


            _carRepo.Delete(car);

            return true;
        }

        //    public async Task<bool> DeleteCarById(int id)
        //    {
        //        var car = _carRepo.GetAsync(id);
        //        if (car == null)
        //            return false; // Car not found

        //        _carRepo.Delete(car);
        //        return true; // Successful deletion
        //    }
        //}
    }
}