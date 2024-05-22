using Rover.Core.Entities;
using Rover.Core.Interfaces;
using Rover.Core.Service.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rover.Repository.GenericRepository;
using Rover.Repository.Data;
using Rover.Core;
using Rover.Core.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Runtime.ConstrainedExecution;

using System.Runtime.CompilerServices;

namespace Rover.Service
{
    public class TripService : ITripService

    {
        private readonly IGenericRepository<Trip> _genericRepo;
        
        private readonly IUsersServices _usersServices;
        private readonly StoreContext _storeContext;

        public TripService(IGenericRepository<Trip> genericrepo, IUsersServices usersServices , StoreContext storeContext)
        {
            _genericRepo = genericrepo;
            _usersServices = usersServices;
            _storeContext = storeContext;
        }


        #region  CreateTrip
        public async Task<int> CreateTripAsync(Trip trip)
        {
            await _genericRepo.SaveTripAsync(trip);



            return (trip.Id);
        }


        #endregion

        #region EditTrip

        public async Task<string> UpdateTripAsync(Trip trip)
        {
            await _genericRepo.Edit(trip);

            return ("Sucssessfull update");
        }

        #endregion

        #region DeleteTrip
        public async Task<bool> DeleteTripAsync(Trip trip)
        {
            trip.DeleteDate = DateTime.Now;
            _genericRepo.Delete(trip);

            return true;
        }

        #endregion

        #region GetAllTrip

        public async Task<List<TripView>> GetTripListAsync()
        {


            return await _genericRepo.GetAllAsync().Where(x => x.DeleteDate == null).Select(x => new TripView()
            {
                From = x.From,
                To = x.To,
                Price = x.Price,
                Date = x.Date,
                Time = x.Time,

            }).ToListAsync();


        }

        #endregion

        #region search by string name 

        public async Task<List<TripView>> SearchTripsAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return await GetTripListAsync();
            }

            var lowerCaseTerm = searchTerm.ToLower();

            return await _genericRepo.GetAllAsync()
                .Where(t => (t.From != null && t.From.ToLower().Contains(lowerCaseTerm))
                         || (t.To != null && t.To.ToLower().Contains(lowerCaseTerm))
                         || (t.Price != null && t.Price.ToString().Contains(lowerCaseTerm))
                         || (t.SeatsAvaliable != null && t.SeatsAvaliable.ToString().Contains(lowerCaseTerm)))
                .Select(x => new TripView
                {
                    From = x.From,
                    To = x.To,
                    Price = x.Price,
                    Date = x.Date,
                    Time = x.Time
                }).ToListAsync();
        }


        #endregion







        #region  Filtration by LastDays
        public IEnumerable<Trip> GetTripsFromLastDays(int days)
        {
            var dateFrom = DateTime.Now.AddDays(-days);

            // Retrieve and filter trips based on the DateTime value
            return _genericRepo.GetAll()
                .Where(t => t.Date >= dateFrom)
                .ToList();
        }






            #endregion



            #region TripDetails

            public async Task<DetailsTrips> GetTripDetailsAsync(int id)
        {
            var trip = await _genericRepo.GetAsync(id);

            if (trip == null)
            {
                return null; // or throw an exception, handle as needed
            }

            return new DetailsTrips
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
        }

        #endregion



        
     


        #region   Status trip
        public async Task<string> UpdateTripStatus(string userId, int tripId, int statusId)
        {
            var trip = await _genericRepo.GetAsync(tripId);
            var user = await _usersServices.GetUserData(userId);
            if (trip == null)
            {
                throw new Exception("Trip not found");
            }

            if (userId != null)
            {
                switch (statusId)
                {
                    case 1: // Available
                        trip.StatusId = 1;
                        break;

                    case 2: // Accepted

                        if (user.Type == 1) { // passenger

                            if (trip.SeatsAvaliable > 0)
                            {
                                trip.SeatsAvaliable -= 1;
                                if (trip.SeatsAvaliable == 0)
                                {
                                    trip.StatusId = 4; // Completed
                                }
                            }
                            var passenger_trip = new Passenger_Trips()
                            {
                                TripId = tripId,
                                PassengerId = userId,

                            };

                            _storeContext.Add<Passenger_Trips>(passenger_trip);
                            await _storeContext.SaveChangesAsync();

                            

                        }
                        else
                        {
                            return "You Dont Have Permission";  // Driver
                        }


                        break;

                    case 3: // Cancelled
                        if (userId == trip.DriverId &&  user.Type != 1)
                        {
                            trip.StatusId = 3; // cancle 
                        }
                        else {

                            trip.SeatsAvaliable += 1;
                            if (trip.SeatsAvaliable > 0 && trip.StatusId == 4)
                            {
                             

                                var result = _storeContext.Set<Passenger_Trips>().FirstOrDefaultAsync(x => x.PassengerId == userId && x.TripId == tripId);
                                _storeContext.Remove(result);
                                await _storeContext.SaveChangesAsync();
                                trip.StatusId = 1; // Available
                            }
                        }


                       
                        
                        break;

                    case 4: // Completed
                        trip.StatusId = 4;
                        break;

                    default:
                        throw new Exception("Invalid status ID");
                }

                await _genericRepo.SaveChangesAsync();
                return "Trip status updated successfully.";
            }
            else
            {
                throw new Exception("Invalid user ID");
            }

        }





     

        #endregion



        #region  AvaliableTrip
        public async Task<List<TripView>> SearchAvailableTripsAsync(string searchTerm)
        {
            var now = DateTime.Now;
            var lowerCaseTerm = searchTerm.ToLower();

            var availableTrips = await _genericRepo.GetAllAsync()
                .Where(t => t.DeleteDate == null
                            && t.Expected_Arrivale > now
                            && t.StatusId == 1 // Status 1 for Available
                            && t.StatusId != 3 // Status 3 for Cancelled
                            && t.StatusId != 4 // Status 4 for Completed
                            && (t.From != null && t.From.ToLower().Contains(lowerCaseTerm))
                            || (t.To != null && t.To.ToLower().Contains(lowerCaseTerm))
                            || (t.Price != null && t.Price.ToString().Contains(lowerCaseTerm))
                            || (t.SeatsAvaliable != null && t.SeatsAvaliable.ToString().Contains(lowerCaseTerm)))
                .Select(t => new TripView
                {
                    Id = t.Id,
                    From = t.From,
                    To = t.To,
                    Price = t.Price,
                    Date = t.Date,
                    Time = t.Time
                })
                .ToListAsync();

            return availableTrips;
        }


        #endregion

        #region Search by string place and days in History "My tip"
        public async Task<List<TripView>> SearchHistoricalTripsAsync(string userId, string searchTerm, int days)
        {
            var dateFrom = DateTime.Now.AddDays(-days);
         
            var lowerCaseTerm = searchTerm.ToLower();

            var userTrips = await _genericRepo.GetAllAsync()
                .Where(t => t.DeleteDate == null
                            && (t.DriverId == userId || t.Passenger_Trips.Any(pt => pt.PassengerId == userId)))
                .ToListAsync();

            var historicalTrips = userTrips
                .Where(t => t.Expected_Arrivale <= dateFrom
                            && t.DeleteDate == null)
                .Where(t => (t.From != null && t.From.ToLower().Contains(lowerCaseTerm))
                         || (t.To != null && t.To.ToLower().Contains(lowerCaseTerm))
                         || (t.Price != null && t.Price.ToString().Contains(lowerCaseTerm))
                         || (t.SeatsAvaliable != null && t.SeatsAvaliable.ToString().Contains(lowerCaseTerm)))
                .Select(t => new TripView
                {
                    From = t.From,
                    To = t.To,
                    Price = t.Price,
                    Date = t.Date,
                    Time = t.Time // التحقق من أن الخاصية Time من نوع TimeOnly
                })
                .ToList();

            return historicalTrips;
        }

     

        #endregion









    }
}

