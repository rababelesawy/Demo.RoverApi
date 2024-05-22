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
        public string? CarNumber { get; set; }
        public string? Driver_License_Picture { get; set;}
        public  string? Model { get; set; }
        public string? Description { get; set; }
        public DateTime? Birth_Driver { get; set;}
        public string? DriverId { get; set; }
        public User? Driver { get; set; }



        public ICollection<Trip> Trips { get; set;}
    }
}
