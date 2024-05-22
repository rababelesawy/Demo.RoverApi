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
        private readonly IUsersServices _usersServices;

        public TripController(ITripService tripService, IGenericRepository<Trip> genericRepository , IUsersServices usersServices)
        {
            _tripService = tripService;
            _genericRepository = genericRepository;
            _usersServices = usersServices;
        }


        #region  Create Trip

        [HttpPost("creates")] //post: / api/trip
        public async Task <ActionResult<int>> CreateTrip(TripDto tripDto , string UserId)

        {
            var User = await _usersServices.GetUserId(UserId); // Driver

            if (UserId == User.User_Id && User.Type == 1)
            {

                return NotFound(new ApiResponse(404, "User dont have Permission"));

            }

            if (User.Type != 1)
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
                    Expected_Arrivale = tripDto.Expected_Arrivale,
                    StatusId = 1,



                };

                var tripid = await _tripService.CreateTripAsync(trip);


                if (tripid is -1)
                    return BadRequest(new ApiResponse(400));


                return Ok(trip.Id);
            }

           


            return Ok();
                

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









        #region  Search Available Trips
      

            [HttpGet("Avaliable")]
            public async Task<IActionResult> SearchAvailableTripsAsync(string searchTerm)
            {
                try
                {
                    var trips = await _tripService.SearchAvailableTripsAsync(searchTerm);
                    return Ok(trips);
                }
                catch (Exception ex)
                {
                    // Handle exception and return appropriate error response
                    return StatusCode(500, new ApiResponse(500, ex.Message));
                }
            }
        
        #endregion


        #region Search Historical Trips


        [HttpGet("historical")]
        public async Task<IActionResult> SearchHistoricalTrips(string userId, string searchTerm, int days)
        {
            try
            {
                var trips = await _tripService.SearchHistoricalTripsAsync(userId, searchTerm, days);
                var tripViews = trips.Select(t => new TripView
                {

                    From = t.From,
                    To = t.To,
                    Price = t.Price,
                    Date = t.Date,
                    Time = t.Time
                }).ToList();
                return Ok(new { success = true, message = "Historical trips retrieved successfully.", data = tripViews });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Failed to retrieve historical trips: {ex.Message}" });
            }
        }
    }

    #endregion
}


























