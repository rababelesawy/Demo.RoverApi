using System.ComponentModel.DataAnnotations;

namespace Rover.Core.Dtos
{
    public class CarDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Picture_License { get; set; }
        [Required]
        public string Picture_Car { get; set; }
        [Required]
        public long License_Car { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public string Description { get; set; }

        public int UserId { get; set; }
    }
}
