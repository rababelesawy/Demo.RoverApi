using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rover.Core.Entities
{
    public class Deleted_Trips:BaseEntity
    {
        public string? UserId { get; set; }
        public User? User { get; set; }
        public int? TripId { get; set; }
        public Trip? Trip { get; set; }
    }
}
