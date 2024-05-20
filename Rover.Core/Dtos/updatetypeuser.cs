using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rover.Core.Dtos
{
    public class updatetypeuser
    {
        [Required]
        public int Type { get; set; }

        public string CarDetails { get; set; }
    }
}
