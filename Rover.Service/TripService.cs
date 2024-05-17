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

namespace Rover.Service
{
    public class TripService :ITripService

    {
        private readonly IGenericRepository<Trip> _genericRepo;
        

        public TripService(IGenericRepository<Trip> genericrepo )
        {
            _genericRepo = genericrepo;
            
        }


        
        public async Task<int> CreateTripAsync(Trip trip)
        {
           await _genericRepo.SaveTripAsync(trip);



            return (trip.Id);
        }

       
    }
}
 