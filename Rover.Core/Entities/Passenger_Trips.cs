using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rover.Core.Entities
{
    public  class Passenger_Trips:BaseEntity
    {
        public int? PassengerId { get; set; }

        public User Passenger { get; set; }
        public int? TripId { get; set; }
        public Trip Trips { get; set; }

    }
}
