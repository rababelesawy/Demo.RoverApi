using System.ComponentModel.DataAnnotations;

namespace Demo.RoverApi.Dtos
{
    public class DriverDto
    {
        [Required]
        public int Id { get; set; }
            
        [Required]
        public int Age { get; set; }
        [Required]
        public long Phone { get; set; }
        [Required]
        public string Picture_License { get; set; }
        [Required]
        public int UserId { get; set; }

    }
}
