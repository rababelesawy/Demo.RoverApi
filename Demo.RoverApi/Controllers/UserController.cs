using ApiRover.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rover.Core.Dtos;
using Rover.Core.Entities;
using Rover.Core.Service.Contract;

namespace Demo.RoverApi.Controllers
{
    
    public class UserController : BaseApiController
    {
        private readonly IUsersServices _usersServices;

        public UserController(IUsersServices usersServices)
        {
            _usersServices = usersServices;
        }


        #region Insert Data

        [HttpPost]
        public async Task<IActionResult> InsertUserData( UserDto userData)
        {
            var result = await _usersServices.InsertUserData(userData);
            if (result)
            {
                return Ok("User data inserted successfully.");
            }
            else
            {
                return BadRequest("Failed to insert user data.");
            }
        }

        #endregion
        #region GetUser user By ID

        [HttpGet("{userId}")]
        public async Task<ActionResult> GetUserData(string userId)
        {
            var userData = await _usersServices.GetUserData(userId);
            if (userData != null)
            {
                return Ok(userData);
            }
            else
            {
                return NotFound("User data not found.");
            }
        }
        #endregion

        #region Update Type of User
        [HttpPost("{userId}/UpdateType")]
        public async Task<IActionResult> UpdateUserType(string userId, int userType)
        {
            var result = await _usersServices.UpdateUserType(userId, userType);
            if (result)
            {
                return Ok("User type updated successfully.");
            }
            else
            {
                return NotFound("User not found.");
            }
        }
        #endregion

        #region Delete User Data 
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var result = await _usersServices.DeleteUser(userId);
            if (result)
            {
                return Ok("User deleted successfully.");
            }
            else
            {
                return NotFound("User not found.");
            }
        }

        #endregion
        #region data

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUserData( UserDto userData)
        {
            var result = await _usersServices.UpdateUserData(userData);
            if (result)
            {
                return Ok("User data updated successfully.");
            }
            else
            {
                return NotFound("User not found.");
            }


        }

        #endregion

    }
}
 