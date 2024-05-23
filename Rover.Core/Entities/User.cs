using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rover.Core.Entities
{
    public class User
    {
        [Key]
        public string? User_Id { get; set; }  //from firebase 
        public string? User_Picture { get; set; }
        public string? First_Name { get; set; }
        public string? Last_Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Password { get; set; }
        public int? Gender { get; set; }
        public int? Type { get; set; }  // 1=passenger 2=driver
        public DateTime? Birth_User { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        public ICollection<Passenger_Trips> PassengerTrips { get; set; }
        public ICollection<Trip> Trips { get; set; }

        public ICollection<Deleted_Trips> DeletedTrips { get; set; }

    }
}
