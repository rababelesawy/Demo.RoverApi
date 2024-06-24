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
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace Demo.RoverApi.Controllers
{
    public class TripController : BaseApiController
    {


        private readonly ITripService _tripService;
        private readonly IGenericRepository<Trip> _genericRepository;
        private readonly IUsersServices _usersServices;
        private readonly ICarServices _carServices;

        public TripController(ITripService tripService, IGenericRepository<Trip> genericRepository , IUsersServices usersServices , ICarServices carServices)
        {
            _tripService = tripService;
            _genericRepository = genericRepository;
            _usersServices = usersServices;
            _carServices = carServices;
        }


        #region  Create Trip


        [HttpPost("create")] // POST: /api/trip/create
        public async Task<ActionResult<int>> CreateTrip(TripDto tripDto)
        {
            // Assuming user and car validation is done before this point
            var user = await _usersServices.GetUserData(tripDto.DriverId);
            var car = await _carServices.GetcarbyCarNumber(tripDto.CarNumber);

            // Check if user exists and has the correct type and if the car belongs to the user
            if (user == null || user.Type == 1 || car == null || user.UserId != car.UserId)
            {
                return BadRequest(new ApiResponse(400, "Invalid user or car information"));
            }

            // Create the trip entity
            var trip = new Trip
            {
                From = tripDto.From,
                To = tripDto.To,
                Price = tripDto.Price,
                Date = tripDto.Date,
                Time = tripDto.Time,
                SeatsAvaliable = tripDto.SeatsAvaliable,
                CarNumber = tripDto.CarNumber,
                Gender = tripDto.Gender,
                DriverId = tripDto.DriverId,
                Expected_Arrivale = tripDto.Expected_Arrivale,
                StatusId = 1
            };

            // Check if the user ID matches the driver ID
            if (tripDto.DriverId != trip.DriverId)
            {
                return BadRequest(new ApiResponse(400, "User does not match the driver"));
            }

            // Create the trip
            var tripId = await _tripService.CreateTripAsync(trip);

            // Check if the trip creation was successful
            if (tripId == -1)
            {
                return BadRequest(new ApiResponse(400, "Failed to create trip"));
            }

            // Return the trip ID
            return Ok(tripId);
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

        [HttpDelete("{tripId}")]
        public async Task<ActionResult> DeleteTrip(int tripId, string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("User ID is required.");
            }

            var result = await _tripService.DeleteTripAsync(tripId, userId);

            if (!result)
            {
                return NotFound("Trip not found or you do not have permission to delete this trip.");
            }

            return Ok("Trip deleted successfully.");
        }

        #endregion


        #region   TripDetails 



        
        [HttpGet("details/{id}")]
        public async Task<IActionResult> GetTripDetails(int id)
        {
            try
            {
                var tripDetails = await _tripService.GetTripDetailsAsync(id);
                if (tripDetails == null)
                {
                    return NotFound();
                }
                return Ok(tripDetails);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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







        #region  Filtration by LastDays
        [HttpGet("last/7")]
        public ActionResult<IEnumerable<TripDto>> GetTripsFromLast7Days()
        {
            var trips = _tripService.GetTripsFromLastDays(7);

            if (!trips.Any())
                return NotFound(new ApiResponse(404, "No trips found in the last 7 days"));

            var tripDtos = trips.Select(t => new TripDto
            {
                Id = t.Id,
                From = t.From,
                To = t.To,
                Price = (decimal)t.Price,
                Date = t.Date,
                Time = t.Time,
                CarNumber = t.CarNumber,
                DriverId = t.DriverId,

            });

            return Ok(tripDtos);
        }

        [HttpGet("last/30")]
        public ActionResult<IEnumerable<TripDto>> GetTripsFromLast30Days()
        {
            var trips = _tripService.GetTripsFromLastDays(30);

            if (!trips.Any())
                return NotFound(new ApiResponse(404, "No trips found in the last 30 days"));

            var tripDtos = trips.Select(t => new TripDto
            {
                Id = t.Id,
                From = t.From,
                To = t.To,
                Price = (decimal)t.Price,
                Date = t.Date,
                Time = t.Time,
                CarNumber = t.CarNumber,
                DriverId = t.DriverId,
            });

            return Ok(tripDtos);
        }


        [HttpGet("last/90")]
        public ActionResult<IEnumerable<TripDto>> GetTripsFromLast90Days()
        {
            var trips = _tripService.GetTripsFromLastDays(90);

            if (!trips.Any())
                return NotFound(new ApiResponse(404, "No trips found in the last 90 days"));

            var tripDtos = trips.Select(t => new TripDto
            {
                Id = t.Id,
                From = t.From,
                To = t.To,
                Price = (decimal)t.Price,
                Date = t.Date,
                Time = t.Time,
                CarNumber = t.CarNumber,
                DriverId = t.DriverId,
            });

            return Ok(tripDtos);
        }

        [HttpGet("last/{days}")]
        public ActionResult<IEnumerable<TripDto>> GetTripsFromLastDays(int days)
        {
            var trips = _tripService.GetTripsFromLastDays(days);

            if (!trips.Any())
                return NotFound(new ApiResponse(404, $"No trips found in the last {days} days"));

            var tripDtos = trips.Select(t => new TripDto
            {
                Id = t.Id,
                From = t.From,
                To = t.To,
                Price = (decimal)t.Price,
                Date = t.Date,
                Time = t.Time,
                CarNumber = t.CarNumber,
                DriverId = t.DriverId,
            });

            return Ok(tripDtos);
        }


        #endregion

        #region status



        [HttpPost("UpdateTripStatus")]
        public async Task<IActionResult> UpdateTripStatus(string userId, int tripId, int statusId)
        {
            try
            {
                var result = await _tripService.UpdateTripStatus(userId, tripId, statusId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        #endregion










        #region Search Avaliable Trips


        [HttpGet("Avaliable")]
        public async Task<IActionResult> SearchAvailableTripsAsyncs(string? searchTerm="")
        {
             
            try
            {
                var trips = await _tripService.SearchAvailableTripsAsync(searchTerm);
              
                return Ok(trips);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse(500, ex.Message));
            }
        }

        #endregion


        #region Search Historical Trips


        [HttpGet("historical")]
        public async Task<IActionResult> SearchHistoricalTrips(string userId, string? searchTerm ="", int days = 0)
        {
            try
            {
                var trips = await _tripService.SearchHistoricalTripsAsync(userId, searchTerm, days);
             
                return Ok(new { success = true, message = "Historical trips retrieved successfully.", data = trips });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Failed to retrieve historical trips: {ex.Message}" });
            }
        
        }
    }

    #endregion
}
 



























