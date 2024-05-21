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

        public TripController(ITripService tripService, IGenericRepository<Trip> genericRepository)
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
                DriverId = tripDto.DriverId,



            };
            var tripid = await _tripService.CreateTripAsync(trip);


            if (tripid is -1)
                return BadRequest(new ApiResponse(400));


            return Ok(trip.Id);
        }

        #endregion




        #region GetAllTrips
        [HttpGet]
        public async Task<ActionResult<TripView>> GetTrip()

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
                DriverId = tripDto.DriverId,
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
        public async Task<ActionResult<DetailsTrips>> GetTripDetails(int id)
        {
            var trip = await _genericRepository.GetAsync(id);

            if (trip == null)
            {
                return NotFound(); // Return 404 if trip with the given ID is not found
            }

            var tripDetails = new DetailsTrips
            {
                From = trip.From,
                To = trip.To,
                Price = trip.Price,
                Date = trip.Date,
                Time = trip.Time,
                Expected_Arrivale = trip.Expected_Arrivale,
                SeatsAvaliable = trip.SeatsAvaliable,
                CarNumber = trip.CarNumber,
                Gender = trip.Gender
            };

            return Ok(tripDetails);
        }

        #endregion

        #region   Search word

        [HttpGet("searchbystring")]
        public async Task<ActionResult<List<TripView>>> SearchTripsAsync(string searchTerm)
        {
            var trips = await _tripService.SearchTripsAsync(searchTerm);
            return Ok(trips);
        }

        #endregion


        #region String place and days 

        [HttpGet("searchs")]
        public async Task<ActionResult<List<TripView>>> SearchTripsAsync(string searchTerm, int days)
        {
            var trips = await _tripService.SearchTripsDaysAsync(searchTerm, days);
            return Ok(trips);
        }


        #endregion




        #region  Filtration by LastDays
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




        [HttpPost("update-status")]
        public async Task<IActionResult> UpdateTripStatus([FromQuery] string userId, [FromQuery] int tripId, [FromQuery] int statusId)
        {
            if (tripId <= 0 || statusId <= 0 || string.IsNullOrWhiteSpace(userId))
            {
                return BadRequest("Invalid parameters.");
            }

            try
            {
                var result = await _tripService.UpdateTripStatus(userId, tripId, statusId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }


}























