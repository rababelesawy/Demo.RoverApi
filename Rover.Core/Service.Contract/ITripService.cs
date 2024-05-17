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


    
        
    }
}
