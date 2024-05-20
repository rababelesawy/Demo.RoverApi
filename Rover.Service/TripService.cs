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

namespace Rover.Service
{
    public class TripService : ITripService

    {
        private readonly IGenericRepository<Trip> _genericRepo;


        public TripService(IGenericRepository<Trip> genericrepo)
        {
            _genericRepo = genericrepo;

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
        #region by string place and days 
        public async Task<List<TripView>> SearchTripsDaysAsync(string searchTerm, int days)
        {
            var dateFrom = DateTime.Now.AddDays(-days);
            var lowerCaseTerm = searchTerm.ToLower();

            var trips = await _genericRepo.GetAllAsync()
                .Where(t => (t.From != null && t.From.ToLower().Contains(lowerCaseTerm))
                         || (t.To != null && t.To.ToLower().Contains(lowerCaseTerm))
                         || (t.Price != null && t.Price.ToString().Contains(lowerCaseTerm))
                         || (t.SeatsAvaliable != null && t.SeatsAvaliable.ToString().Contains(lowerCaseTerm))
                         && (days == 0 || t.Date >= dateFrom))
                .Select(t => new TripView
                {
                    From = t.From,
                    To = t.To,
                    Price = t.Price,
                    Date = t.Date,
                    Time = t.Time
                })
                .ToListAsync();

            return trips;
        }

        #endregion



        #region  Filtration by LastDays
        public IEnumerable<Trip> GetTripsFromLastDays(int days)
        {
            var dateFrom = DateTime.Now.AddDays(-days);
            return _genericRepo.GetAll()
                .Where(t => t.Date >= dateFrom)
                .ToList();
        }





        #endregion


        #region status


        //public async Task<bool> AcceptTripAsync(int tripId)
        //{
        //    var trip = await _genericRepo.GetAsync(tripId);
        //    if (trip == null)
        //        return false;

        //    if (trip.SeatsAvaliable > 0)
        //    {
        //        trip.SeatsAvaliable--;
        //        await _genericRepo.Edit(trip);
        //    }

        //    return true;
        //}

        //public async Task<bool> CancelTripAsync(int tripId, int userType)
        //{
        //    var trip = await _genericRepo.GetAsync(tripId);
        //    if (trip == null)
        //        return false;

        //    if (userType == 2) // Driver
        //    {
        //        trip. = "cancelled";
        //        await _genericRepo.Edit(trip);
        //    }
        //    else if (userType == 1) // Passenger
        //    {
        //        if (trip.Status == "completed")
        //        {
        //            trip.Status = "available";
        //            trip.SeatsAvaliable++;
        //            await _genericRepo.Edit(trip);
        //        }
        //    }

        //    return true;
        //}

        //public async Task<bool> RemoveTripIfNoBookingsAsync(int tripId)
        //{
        //    var trip = await _genericRepo.GetAsync(tripId);
        //    if (trip == null)
        //        return false;

        //    if (trip.Bookings.Count == 0 && trip.Expected_Arrivale < DateTime.Now)
        //    {
        //         _genericRepo.Delete(trip);

        //    }

        //    return false;
        //}
        #endregion




        #region

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



        }
}

 