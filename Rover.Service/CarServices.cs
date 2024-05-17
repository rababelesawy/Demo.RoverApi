using Rover.Core.Entities;
using Rover.Core.Interfaces;
using Rover.Core.Service.Contract;
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
        public async Task<int> CreateCarAsync(Car car)
        {
            await _carRepo.SaveAsync(car);



            return (car.Id);
        }
    }
}
