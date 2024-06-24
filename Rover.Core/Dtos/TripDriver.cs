using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rover.Core.Dtos
{
    public class TripDriver
    {
     
        public int Id { get; set; }
        public string From { get; set; }
    
        public string To { get; set; }

        public decimal? Price { get; set; }
  
        public DateTime? Date { get; set; }
     
        public DateTime? Time { get; set; }
    
        public int? SeatsAvaliable { get; set; }

        public string? Driver_Picture { get; set; }
    
        public string? Driver_Name { get; set; }
   


    }
}
