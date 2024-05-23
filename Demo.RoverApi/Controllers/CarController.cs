using Rover.Core.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rover.Core.Entities;
using Rover.Core.Service.Contract;
using ApiRover.Errors;
using Rover.Core.Interfaces;
using Rover.Service;
using Microsoft.EntityFrameworkCore;

namespace Demo.RoverApi.Controllers
{

    public class CarController : BaseApiController
    {
        private readonly ICarServices _carServices;
        private readonly IGenericRepository<Car> _genericRepository;
        private readonly IUsersServices _usersServices;

        public CarController(ICarServices carServices , IGenericRepository<Car> genericRepository , IUsersServices usersServices)
        {
            _carServices = carServices;
            _genericRepository = genericRepository;
            _usersServices = usersServices;
        }

        #region   Create Car


        [HttpPost("CreateCar")]


        public async Task<ActionResult<int>> CreateCar(CarDto carDto )
        {
            var User = await _usersServices.GetUserData(carDto.UserId);
           

            if (User == null )
            {
                return NotFound(new ApiResponse(404, "User Not Found"));

            }
      
            

                Car car = new Car()
                {
                    Picture_Car = carDto.Picture_Car,
                    Picture_License = carDto.Picture_License,
                    License_Car = carDto.License_Car,
                    Model = carDto.Model,
                    Description = carDto.Description,
                    DriverId = carDto.UserId,
                    Driver_License_Picture = carDto.Driver_License_Picture,



                };
                var tripid = await _carServices.CreateCarAsync(car);

             
                if (tripid is -1)
                    return BadRequest(new ApiResponse(400));


                await _usersServices.UpdateUserType(carDto.UserId, 2);
                await _genericRepository.SaveChangesAsync();
            
          



            return Ok(car.Id);
        
        }



        #endregion


        #region Get car by Id

        [HttpGet("GetCarById/{id}")]

        public async Task<ActionResult<string>> GetCarById(int id , string Userid )
        {
            var car = await _carServices.GetCarByIdAsync(id);
            var User = await _usersServices.GetUserData(Userid);
            if (id == car.Id && User.UserId == car.DriverId)
            {

                if (car == null)
                {
                    return NotFound();
                }

                var carDto = new CarDto
                {
                    Id = car.Id,
                    Picture_Car = car.Picture_Car,
                    Picture_License = car.Picture_License,
                    License_Car = (long)car.License_Car,
                    Model = car.Model,
                    Description = car.Description,
                    UserId = car.DriverId,
                    Driver_License_Picture = car.Driver_License_Picture
                };

                return Ok(carDto);
            }


            if (id != car.Id || User.UserId != car.DriverId)
            {
                return "You Dont Have Permission To See this Car";

            }
                return Ok();
           
        }

        #endregion





        #region  updateCar

        [HttpPut("update")] // PUT: /api/car/update
        public async Task<ActionResult<string>> UpdateCar(CarDto carDto)
        {
            var car = await _carServices.GetCarByIdAsync(carDto.Id);

            if (car == null)
            {
                return NotFound(new ApiResponse(404, "Car Not Found"));
            }

            if (carDto.UserId != car.DriverId)
            {
                return Unauthorized(new ApiResponse(401, "You don't have permission"));
            }

            // Update the existing car entity with the new values from the DTO
            car.Picture_License = carDto.Picture_License;
            car.Picture_Car = carDto.Picture_Car;
            car.Model = carDto.Model;
            car.Description = carDto.Description;
            car.License_Car = carDto.License_Car;
            car.Driver_License_Picture = carDto.Driver_License_Picture;

            var result = await _carServices.UpdateCarAsync(car);
            if (result == null)
            {
                return BadRequest(new ApiResponse(400, "Failed to update car"));
            }

            return Ok("Successfully updated car");
        }

        #endregion





        #region   Delete Car

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteCar(int id)
        {
            var car = await _genericRepository.GetAsync(id);

            if (car == null)
                return NotFound(new ApiResponse(404, "Car not found"));

            var result = await _carServices.DeleteCarAsync(car);


            return ("Sucsessfully Deleted");
        }
        #endregion




    }

}