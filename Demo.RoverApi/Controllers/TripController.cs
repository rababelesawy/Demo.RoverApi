using Demo.RoverApi.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rover.Core.Entities;
using Rover.Core.Service.Contract;
using ApiRover.Errors;
using System.Reflection;
using Rover.Core.Interfaces;
using Rover.Repository.GenericRepository;

namespace Demo.RoverApi.Controllers
{
    public class TripController : BaseApiController
    {


        private readonly ITripService _tripService;
        public TripController(ITripService tripService)
        {
            _tripService = tripService;
        }


        [HttpPost("creates")] //post: / api/trip
        public async Task<ActionResult<int>> CreateTrip(TripDto tripDto)
        {
            Trip trip = new Trip()
            {
                To = tripDto.From,
                From = tripDto.To,
                Price = tripDto.Price,
                Date = tripDto.Date,
                Time = tripDto.Time,
                SeatsAvaliable = tripDto.SeatsAvaliable,
                CarNumber = tripDto.CarNumber,
                Gender = tripDto.Gender,
                DriverId = tripDto.UserId,



        };
        var tripid = await _tripService.CreateTripAsync(trip);


            if (tripid is -1)
                return BadRequest(new ApiResponse(400));


            return Ok(trip.Id);
    }

    }
}
