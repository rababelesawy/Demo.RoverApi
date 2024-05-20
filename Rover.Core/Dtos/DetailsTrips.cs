using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rover.Core.Dtos
{
    public class DetailsTrips
    {

        public string? From { get; set; }

        public string? To { get; set; }

        public decimal? Price { get; set; }

        public DateTime? Date { get; set; }

        public DateTime? Time { get; set; }

        public DateTime? Expected_Arrivale { get; set; }
        
        public int? SeatsAvaliable { get; set; }
        
        public string? CarNumber { get; set; }

        public int? Gender { get; set; }


    }
}
