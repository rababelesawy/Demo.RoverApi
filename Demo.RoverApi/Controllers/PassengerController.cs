using ApiRover.Errors;
using Rover.Core.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rover.Core.Entities;
using Rover.Core.Service.Contract;
using Rover.Service;

namespace Demo.RoverApi.Controllers
{

    public class PassengerController : BaseApiController
    {
        private readonly IPassengerServices _passengerServices;

        public PassengerController(IPassengerServices  passengerServices) {
            _passengerServices = passengerServices;
        }


        [HttpPost("CreatePassenger")]


        public async Task<ActionResult<int>> CreatePassenger(PassengerDto passengerDto)
        {

            Passenger passenger = new Passenger()
            {


                Age = passengerDto.Age,
                Phone = passengerDto.Phone,
                Picture_Passanger = passengerDto.Picture_Passanger,
                UserId = passengerDto.UserId,

            };
            var tripid = await _passengerServices.CreatePassengerAsync(passenger);


            if (tripid is -1)
                return BadRequest(new ApiResponse(400));


            return Ok(passenger.Id);
        }

    }
}
