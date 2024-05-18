using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rover.Core.Entities
{
    public class User : BaseEntity
    {
        public string? UserId { get; set; }
        public string? First_Name { get; set; }
        public string?Last_Name{ get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public int? Age { get; set; }
        public int? Gender { get; set; }
        public int? Type { get; set; }  // 1=passenger 2=driver
        public string? Phone { get; set; }
        public string? Picture_License { get; set; }
        public string? Picture_User { get; set; }
        public ICollection<Passenger_Trips> PassengerTrips { get; set; }
        public ICollection<Trip> Trips { get; set;}

    }
}
