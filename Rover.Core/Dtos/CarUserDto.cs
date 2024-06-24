using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rover.Core.Dtos
{
    public class CarUserDto
    {
        public string? UserId { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public int? Gender { get; set; }
        public string? User_Picture { get; set; }
        public string? CarNumber { get; set; }
        public string? Picture_Car { get; set; }
    }
}
