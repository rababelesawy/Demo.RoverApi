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

        #region  EditTrip

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



        #endregion


        #region  DeleteTrip

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteTrip(int id)
        {
            var trip = await _genericRepository.GetAsync(id);

            if (trip == null)
                return NotFound(new ApiResponse(404, "Car not found"));

            var result = await _tripService.DeleteTripAsync(trip);


            return ("Sucsessfully Deleted");
        }


        #endregion


        #region   TripDetails 



        [HttpGet("Details")]
        public async Task<ActionResult<TripDto>> GetTripId(int id)

        {

            var trip = await _genericRepository.GetAsync(id);

            return Ok(trip);
        }

        #endregion


        #region Search Trip with Name place 


        [HttpGet("search")]
        public ActionResult<IEnumerable<TripDto>> SearchTrips(string searchTerm, int days)
        {
            var trips = _tripService.SearchTrips(searchTerm ,days);

            if (!trips.Any())
                return NotFound(new ApiResponse(404, "No trips found matching the search term"));

            return Ok(trips);
        }


        #endregion



        #region   //New By Days


        [HttpGet("last/7")]
        public ActionResult<IEnumerable<Trip>> GetTripsFromLast7Days()
        {
            var trips = _tripService.GetTripsFromLastDays(7);

            if (!trips.Any())
                return NotFound(new ApiResponse(404, "No trips found in the last 7 days"));

            return Ok(trips);
        }

        [HttpGet("last/30")]
        public ActionResult<IEnumerable<Trip>> GetTripsFromLast30Days()
        {
            var trips = _tripService.GetTripsFromLastDays(30);

            if (!trips.Any())
                return NotFound(new ApiResponse(404, "No trips found in the last 30 days"));

            return Ok(trips);
        }

        [HttpGet("last/90")]
        public ActionResult<IEnumerable<Trip>> GetTripsFromLast90Days()
        {
            var trips = _tripService.GetTripsFromLastDays(90);

            if (!trips.Any())
                return NotFound(new ApiResponse(404, "No trips found in the last 90 days"));

            return Ok(trips);
        }

        [HttpGet("last/{days}")]
        public ActionResult<IEnumerable<Trip>> GetTripsFromLastDays(int days)
        {
            var trips = _tripService.GetTripsFromLastDays(days);

            if (!trips.Any())
                return NotFound(new ApiResponse(404, $"No trips found in the last {days} days"));

            return Ok(trips);
        }
        #endregion

        #region Trip Status 



        #endregion

    }
}


















