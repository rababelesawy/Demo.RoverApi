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
        private readonly IUsersServices _usersServices;

        public CarServices(IGenericRepository<Car> CarRepo , IUsersServices usersServices)
        {
            _carRepo = CarRepo;
           _usersServices = usersServices;
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





  
    }
}
