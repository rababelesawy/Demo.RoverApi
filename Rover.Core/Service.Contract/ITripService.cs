using Rover.Core.Dtos;
using Rover.Core.Entities;
using Rover.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rover.Core.Service.Contract
{
    public interface ITripService
    {
        Task <int> CreateTripAsync(Trip trip);
        Task<string> UpdateTripAsync(Trip trip);
        Task<bool> DeleteTripAsync(Trip trip);
        Task <List<TripView>> GetTripListAsync();
        IEnumerable<Trip> SearchTrips(string searchTerm, int days);

        //New
        IEnumerable<Trip> GetTripsFromLastDays(int days);

    }
}
