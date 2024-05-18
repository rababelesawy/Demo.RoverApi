using ApiRover.Errors;
using Rover.Core.Dtos;
using Microsoft.AspNetCore.Mvc;
using Rover.Core.Entities;
using Rover.Core.Service.Contract;

namespace Demo.RoverApi.Controllers
{

    public class DriverController : BaseApiController
    {
        private readonly IDriverService _driverService;

        public DriverController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        //[HttpPost("CreateDriver")]


        //public async Task<ActionResult<int>> CreateDriver(DriverDto driverDto)
        //{
        //    Driver Driver = new Driver()
        //    {
        //        Age = driverDto.Age,
        //        Phone = driverDto.Phone,
        //        Picture_License = driverDto.Picture_License,
        //        UserId = driverDto.UserId,



        //    };
        //    var tripid = await _driverService.CreateDriverAsync(Driver);


        //    if (tripid is -1)
        //        return BadRequest(new ApiResponse(400));


        //    return Ok(Driver.Id);
    //}


    }


}


