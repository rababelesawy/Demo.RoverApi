using System.ComponentModel.DataAnnotations;

namespace Demo.RoverApi.Dtos
{
    public class TripView
    {
        public string From { get; set; }
       
        public string To { get; set; }
    
        public decimal? Price { get; set; }
        
        public DateTime Date { get; set; }
    
        public DateTime Time { get; set; }


        
    }
}
