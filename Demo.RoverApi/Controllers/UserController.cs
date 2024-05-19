using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rover.Core.Dtos;
using Rover.Core.Entities;
using Rover.Core.Interfaces;
using Rover.Core.Service.Contract;
using Rover.Service;

namespace Demo.RoverApi.Controllers
{ 
    public class UserService : BaseApiController
{
        private readonly IGenericRepository<User> _userService;


        public UserService(IGenericRepository<User> userService)
        {
            _userService = userService;

        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationDto registrationDto)
        {
            var result = await _userService.RegisterUserAsync(registrationDto);
            if (result)
            {
                return Ok("User registered successfully.");
            }
            else
            {
                return BadRequest("User registration failed. User ID may already exist.");
            }
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserDetails(string userId)
        {
            var userDetails = await _userService.GetUserDetailsAsync(userId);
            if (userDetails != null)
            {
                return Ok(userDetails);
            }
            else
            {
                return NotFound("User not found.");
            }
        }
    }
}
