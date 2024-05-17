using Demo.RoverApi.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rover.Core.Entities;
using Rover.Core.Service.Contract;
using ApiRover.Errors;
namespace Demo.RoverApi.Controllers
{

    public class CarController : BaseApiController
    {
        private readonly ICarServices _carServices;
        public CarController(ICarServices carServices)
        {
            _carServices = carServices;
        }


       
        [HttpPost("CreateCar")]


        public async Task<ActionResult<int>> CreateCar(CarDto carDto)
        {

            Car car = new Car()
            {
                Picture_Car = carDto.Picture_Car,
                Picture_License = carDto.Picture_License,
                License_Car = carDto.License_Car,
                Model = carDto.Model,
                Description = carDto.Description,
                DriverId = carDto.UserId,



            };
            var tripid = await _carServices.CreateCarAsync(car);


            if (tripid is -1)
                return BadRequest(new ApiResponse(400));


            return Ok(car.Id);
        }
    }

}