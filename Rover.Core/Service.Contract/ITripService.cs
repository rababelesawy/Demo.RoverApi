﻿using Rover.Core.Dtos;
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
        Task<int> CreateTripAsync(Trip trip);
        Task<string> UpdateTripAsync(Trip trip);
        Task<bool> DeleteTripAsync(Trip trip);
        Task<List<TripView>> GetTripListAsync();
        Task<List<TripView>> SearchTripsAsync(string searchTerm);
        Task<List<TripView>> SearchTripsDaysAsync(string searchTerm, int days);
        IEnumerable<Trip> GetTripsFromLastDays(int days);
        Task<DetailsTrips> GetTripDetailsAsync(int id);

        // status 
       


        Task<string> UpdateTripStatus(string userId, int tripId, int statusId);
        Task AutoCompleteTripsAsync();


    }
}