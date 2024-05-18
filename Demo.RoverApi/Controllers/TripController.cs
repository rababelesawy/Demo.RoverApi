using Rover.Core.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rover.Core.Entities;
using Rover.Core.Service.Contract;
using ApiRover.Errors;
using System.Reflection;
using Rover.Core.Interfaces;
using Rover.Repository.GenericRepository;
using Rover.Service;

namespace Demo.RoverApi.Controllers
{
    public class TripController : BaseApiController
    {


        private readonly ITripService _tripService;
        private readonly IGenericRepository<Trip> _genericRepository;

        public TripController(ITripService tripService , IGenericRepository<Trip> genericRepository)
        {
            _tripService = tripService;
            _genericRepository = genericRepository;
        }


        #region  Create Trip

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

        #endregion




        #region GetAllTrips
        [HttpGet]
        public async Task <ActionResult<TripView>> GetTrip()

        {
         
            var trip = await _tripService.GetTripListAsync();
            
            return Ok(trip);
        }


        #endregion



        [HttpPut("update")] // PUT: /api/trip/update
        public async Task<ActionResult<string>> UpdateTrip(TripDto tripDto)
            {
            var trip = new Trip
            {
                Id = tripDto.Id,
                From = tripDto.From,
                To = tripDto.To,
                Price = tripDto.Price,
                Date = tripDto.Date,
                Time = tripDto.Time,
                SeatsAvaliable = tripDto.SeatsAvaliable,
                CarNumber = tripDto.CarNumber,
                Gender = tripDto.Gender,
                DriverId = tripDto.UserId,
            };
            var result = await _tripService.UpdateTripAsync(trip);

            if (result is null)
            
                return ("Faild Update");


            return ("succsessfull update");
            }











      
    }
}
