using System.ComponentModel.DataAnnotations;

namespace Rover.Core.Dtos
{
    public class PassengerDto
    {

        [Required]
        public int Id { get; set; }
        [Required]
        public string Picture_Passanger { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public long Phone { get; set; }
        [Required]
        public int UserId { get; set; }



    }
}
