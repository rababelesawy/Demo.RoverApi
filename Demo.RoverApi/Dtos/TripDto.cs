using Rover.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Demo.RoverApi.Dtos
{
    public class TripDto
    {

        [Required]
        public string From { get; set; }
        [Required]
        public string To { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public DateTime Time { get; set; }
        [Required]
        public int SeatsAvaliable { get; set; }
        [Required]
        public string CarNumber { get; set; }
        [Required]
        public int Gender { get; set; }  // male =0 , female = 1, other = 2

        public int UserId { get; set; }
        
       

    }
}
