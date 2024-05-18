using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rover.Core.Entities
{
    public class Car:BaseEntity
    {
        public string? Picture_License { get; set; }
        public string? Picture_Car { get; set; }
        public long? License_Car { get; set; }
        public  string? Model { get; set; }
        public string? Description { get; set; }

        public int? DriverId { get; set; }
        public User Driver { get; set; }
        // Navigation properity [one ]
        public ICollection<Trip> Trips { get; set;}
    }
}
