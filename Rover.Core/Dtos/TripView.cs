using System.ComponentModel.DataAnnotations;

namespace Rover.Core.Dtos
{
    public class TripView

    {
        public int Id { get; set; }
        public string From { get; set; }
       
        public string To { get; set; }
    
        public decimal? Price { get; set; }

        public DateTime? Date { get; set; }
        public DateTime? Time { get; set; }
        public string? User_Picture { get; set; }


    }
}
