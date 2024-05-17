using Demo.RoverApi.Dtos;
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

        public CarController(ICarServices carServices , IGenericRepository<Car> genericRepository)
        {
            _carServices = carServices;
            genericRepository = genericRepository;
        }

        #region   Create Car

       
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



        #endregion




        #region  EditCar

        [HttpPut("update")] // PUT: /api/car/update
        public async Task<ActionResult<string>> UpdateCar(int id , CarDto carDto)
        {
            if (id  != carDto.Id)
            {
                return BadRequest(new ApiResponse(400, "Mismatched Car ID"));


            }
            var Car = new Car
            {
             Id = carDto.Id,
             Picture_License = carDto.Picture_License,
             Picture_Car= carDto.Picture_Car,
             Model = carDto.Model,
             Description = carDto.Description,
             License_Car= carDto.License_Car,
             DriverId = carDto.UserId,

            };

           var result = await _carServices.UpdateCarAsync(Car);
            if (result is null)
            {
                return ("Faild Update");
             
               
            }


            return ("succsessfull update");



        }



        #endregion





        #region   Delete Car

        //[HttpDelete("{id}")]
        //public ActionResult DeleteCar(int id)
        //{
        //    var car =  _genericRepository.GetAsync(id);

        //    if (car == null)
        //        return NotFound(new ApiResponse(404, "Car not found"));

        //    var result =  _carServices.DeleteCarAsync(car);


        //    return ("Sucsessfully Deleted");
        //}
        #endregion




    }

}