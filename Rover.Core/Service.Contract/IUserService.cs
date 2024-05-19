using Rover.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rover.Core.Service.Contract
{
    public interface IUserService
    {
            Task<bool> RegisterUserAsync(UserRegistrationDto registrationDto);
            Task<UserDetailsDto> GetUserDetailsAsync(string userId);
    }
}

