using Microsoft.VisualBasic;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rover.Core.Entities
{
    public class Driver :BaseEntity
    {
        public int? Age { get; set; }
        public long? Phone { get; set; }
        public string? Picture_License { get; set; }
        public int? UserId { get; set; }
        public ICollection<Trip> Trips { get; set;}

    }
}
