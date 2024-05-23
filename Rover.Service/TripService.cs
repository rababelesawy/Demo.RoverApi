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
using Rover.Repository.Migrations;

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


        #region Deleted

        public async Task<bool> DeleteTripAsync(int tripId, string userId)
        {
            var trip = await _storeContext.Trips.FirstOrDefaultAsync(t => t.Id == tripId && t.DriverId == userId);
            if (trip == null)
            {
                return false;
            }

            var deletedTrip = new Deleted_Trips
            {
                UserId = userId,
                TripId = tripId,
                DeleteDate = DateTime.UtcNow
            };

            _storeContext.Trips.Remove(trip);
            _storeContext.DeletedTrips.Add(deletedTrip);
            await _storeContext.SaveChangesAsync();

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





        #region  Update  Status trip
        public async Task<string> UpdateTripStatus(string userId, int tripId, int statusId)
        {
            if (userId == null)
            {
                throw new Exception("Invalid user ID");
            }

            var trip = await _genericRepo.GetAsync(tripId);
            var user = await _usersServices.GetUserData(userId);

            if (trip == null)
            {
                throw new Exception("Trip not found");
            }

            switch (statusId)
            {
                case 1: // Available
                    trip.StatusId = 1;
                    break;

                case 2: // Accepted
                    if (user.Type == 1) // passenger
                    {
                        if (trip.SeatsAvaliable > 0)
                        {
                            trip.SeatsAvaliable -= 1;
                            if (trip.SeatsAvaliable == 0)
                            {
                                trip.StatusId = 4; // Completed
                            }
                        }
                        else
                        {
                            return "No available seats";
                        }

                        var passenger_trip = new Passenger_Trips()
                        {
                            TripId = tripId,
                            PassengerId = userId
                        };

                        _storeContext.Add(passenger_trip);
                    }
                    else
                    {
                        return "You don't have permission"; // Driver
                    }
                    break;

                case 3: // Cancelled
                    if (userId == trip.DriverId && user.Type != 1) // Driver
                    {
                        trip.StatusId = 3; // cancel 
                    }
                    else if (userId != trip.DriverId && user.Type == 1) // Passenger
                    {
                        var passengerTrip = await _storeContext.Set<Passenger_Trips>()
                            .FirstOrDefaultAsync(x => x.PassengerId == userId && x.TripId == tripId);

                        if (passengerTrip != null)
                        {
                            trip.SeatsAvaliable += 1;
                            _storeContext.Remove(passengerTrip);

                            if (trip.StatusId == 4)
                            {
                                trip.StatusId = 1; // Available
                            }
                        }
                        else
                        {
                            return "This Trip Not Accepted to be Cancel"; //Passenger
                        }
                    }
                    else
                    {
                        return "You don't have permission to cancel this trip";  
                    }
                    break;

                case 4: // Completed
                    trip.StatusId = 4;
                    break;
                  
                case 5: // Start
                    if (userId == trip.DriverId && user.Type != 1)
                    {
                        trip.StatusId = 5;
                        return "The Trip Is Start";
                    }
                    break;

                case 6: // End
                    if (userId == trip.DriverId && user.Type != 1)
                    {
                        trip.StatusId = 6;
                        return "The Trip Is Ended";
                    }
                    break;


                default:
                    throw new Exception("Invalid status ID");
            }

            await _storeContext.SaveChangesAsync();
            return "Trip status updated successfully.";
        }

        #endregion




        #region  AvaliableTrip
        public async Task<List<TripView>> SearchAvailableTripsAsync(string searchTerm)
        {
            var now = DateTime.Now;
            var lowerCaseTerm = searchTerm.ToLower();

            var availableTrip = await _genericRepo.GetAllAsync()
                 .Where(t => t.DeleteDate == null
                          
                             && (t.Expected_Arrivale >= now)
                             &&(t.StatusId== null || t.StatusId ==1)
                             && (string.IsNullOrEmpty(searchTerm) || (t.From != null && t.From.ToLower().Contains(lowerCaseTerm))
                          || (t.To != null && t.To.ToLower().Contains(lowerCaseTerm))
                          || (t.Price != null && t.Price.ToString().Contains(lowerCaseTerm))
                          || (t.SeatsAvaliable != null && t.SeatsAvaliable.ToString().Contains(lowerCaseTerm))))






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
            return availableTrip;
        }

        #endregion

        #region Search by string place and days in History "My tip"
        public async Task<List<TripView>> SearchHistoricalTripsAsync(string userId, string searchTerm="", int days=0)
        {
            var dateFrom = DateTime.Now.AddDays(-days);

            var lowerCaseTerm = searchTerm.ToLower();

            var userTrips = await _genericRepo.GetAllAsync()
                .Where(t => t.DeleteDate == null
                            && (t.DriverId == userId || t.Passenger_Trips.Any(pt => pt.PassengerId == userId)) 
                            &&(days==0 || t.Expected_Arrivale <= dateFrom)
                            &&(string.IsNullOrEmpty(searchTerm) || (t.From != null && t.From.ToLower().Contains(lowerCaseTerm))
                         || (t.To != null && t.To.ToLower().Contains(lowerCaseTerm))
                         || (t.Price != null && t.Price.ToString().Contains(lowerCaseTerm))
                         || (t.SeatsAvaliable != null && t.SeatsAvaliable.ToString().Contains(lowerCaseTerm))))

                            

               

           
                .Select(t => new TripView
                {
                    Id= t.Id,
                    From = t.From,
                    To = t.To,
                    Price = t.Price,
                    Date = t.Date,
                    Time = t.Time
                })
                .ToListAsync();

            return userTrips;
        }

       



        #endregion









    }
}

