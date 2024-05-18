﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rover.Core.Entities
{
 
    public class Trip : BaseEntity
    {


        public string? From { get; set; }
        public string? To { get; set; }
        public decimal? Price { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? Time { get; set; }
        public int? SeatsAvaliable { get; set; }
        public string? CarNumber { get; set; }
        public int? Gender { get; set; }
        public int? CarId { get; set; } // Foregin Key Column  => Car 
        public Car? Car { get; set; } // Navigational Properity [one]

        public int? DriverId { get; set; }
        public Driver? Driver { get; set; }
        public ICollection<Passenger_Trips> PassengerTrips { get; set; }

        public int? StatusId { get; set; }
        public TripStatus? Status { get; set; }









    }
}
