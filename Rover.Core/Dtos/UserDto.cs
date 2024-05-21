﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rover.Core.Dtos
{
    public class UserDto
    {

        [Required]
        public string? UserId { get; set; }
        [Required]
        public string? User_Picture { get; set; }
        [Required]
        public string? First_Name { get; set; }
        [Required]
        public string? Last_Name { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Phone { get; set; }
        [Required]
        public int? Gender { get; set; }

        public int? Type { get; set; } = 1;  // By Default value  Passenger = 1
    }
}
