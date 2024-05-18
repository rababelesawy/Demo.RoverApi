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

        #region   Search in trip

        public IEnumerable<Trip> SearchTrips(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return Enumerable.Empty<Trip>();

            var lowerCaseTerm = searchTerm.ToLower();

            return _genericRepo.GetAll()
                .Where(t => (t.From != null && t.From.ToLower().Contains(lowerCaseTerm))
                         || (t.To != null && t.To.ToLower().Contains(lowerCaseTerm))
                         || (t.Price != null && t.Price.ToString().Contains(lowerCaseTerm))
                         || (t.SeatsAvaliable != null && t.SeatsAvaliable.ToString().Contains(lowerCaseTerm)))
                .ToList();
        }


        #endregion


        #region  GetTripsFromLastDays
        public IEnumerable<Trip> GetTripsFromLastDays(int days)
        {
            var dateFrom = DateTime.Now.AddDays(-days);
            return _genericRepo.GetAll()
                .Where(t => t.Date >= dateFrom)
                .ToList();
        }


        #endregion


    }
}

 