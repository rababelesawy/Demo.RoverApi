﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rover.Core.Entities
{
    public  class TripStatus:BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Trip> Trips { get; set;}


    }
}
