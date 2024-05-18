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

        public async Task<List<TripView>> GetTripListAsync()
        {

            
        return await _genericRepo.GetAllAsync().Where(x=>x.DeleteDate == null).Select(x=>new TripView(){
                From = x.From,
                To = x.To,
                Price= x.Price,
                Date = x.Date,
                Time = x.Time,
                
            }).ToListAsync();

            
        }







        public async Task<string> UpdateTripAsync(Trip trip)
        {
            await _genericRepo.Edit(trip);

            return ("Sucssessfull update");
        }

     
    }
}
 