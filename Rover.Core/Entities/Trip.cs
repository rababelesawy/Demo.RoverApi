using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rover.Core.Entities
{
    [Flags]
      public enum Gender:byte
    {
        male=0,  female= 1, others=2

    }
    public  class Trip :BaseEntity
    {
        public string from { get; set; }
        public string  to { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
         public DateTime Time { get; set; }
        public int SeatsAvaliable { get; set; }
        public Gender Gender { get; set; }

        public int? CarId { get; set; } // Foregin Key Column  => Car 
        public Car Car { get; set; } // Navigational Properity [one]

        public int? DriverId { get; set; }
        public Driver Driver { get; set; }
        public ICollection<Passenger_Trips> PassengerTrips { get; set; }







    }
}
