using Rover.Core.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rover.Core.Entities;
using Rover.Core.Service.Contract;
using ApiRover.Errors;
using Rover.Core.Interfaces;
using Rover.Service;

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


        public async Task<ActionResult<int>> CreateCar(CarDto carDto ,string UserId)
        {
            var User = await _usersServices.GetUserData(UserId);
           

            if (User == null )
            {
                return NotFound(new ApiResponse(404, "User Not Found"));

            }
            if (User != null && User.Type == 1)
            {
                return NotFound(new ApiResponse(404, "You don't have permission"));

            }




            if (User.Type != 1)
            {

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

                return Ok(car.Id);
            }
            



            return Ok();
       
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





        #region  EditCar

        [HttpPut("update")] // PUT: /api/car/update
        public async Task<ActionResult<string>> UpdateCar(CarDto carDto , string UserId)
        {

            var user = await _usersServices.GetUserId(UserId);
            var car = await _carServices.GetCarByIdAsync (carDto.Id);

            if (user == null)
            {
                return NotFound(new ApiResponse(404, "User Not Found"));

            }
            if (user != null && user.Type == 1 && UserId != car.DriverId)
            {
                return Unauthorized(new ApiResponse(401, "You don't have permission"));

            }
            if (user != null && user.Type != 1 && user.User_Id == carDto.UserId)
            {


                var Car = new Car
                {
                    Id = carDto.Id,
                    Picture_License = carDto.Picture_License,
                    Picture_Car = carDto.Picture_Car,
                    Model = carDto.Model,
                    Description = carDto.Description,
                    License_Car = carDto.License_Car,
                    DriverId = carDto.UserId,
                    Driver_License_Picture = carDto.Driver_License_Picture,

                };

                var result = await _carServices.UpdateCarAsync(Car);
                if (result is null)
                {
                    return ("Faild Update");


                }
            }

            return ("succsessfull update");



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