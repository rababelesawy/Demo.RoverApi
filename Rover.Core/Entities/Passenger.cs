
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rover.Core.Entities
{
    public class Passenger:BaseEntity
    {
        public string? Picture_Passanger { get; set; }
        public int? Age { get; set; }
       
        public long? Phone { get; set; }
        public int? UserId { get; set; }

        public ICollection<Passenger_Trips> PassengerTrips { get; set; }

    }
}
