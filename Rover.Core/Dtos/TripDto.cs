using Rover.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Rover.Core.Dtos
{
    public class TripDto
    {
        [Required]
        public int Id { get; set; }
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
        public DateTime? Expected_Arrivale { get; set; }
        [Required]
        public int SeatsAvaliable { get; set; }
        [Required]
        public string CarNumber { get; set; }
        [Required]
        public int Gender { get; set; }  // male =0 , female = 1, other = 2

        public string DriverId { get; set; }
        
       

    }
}
