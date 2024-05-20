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



        [HttpPost("Register")]

        public async Task Register(UserDto userDto)

        {
            User user = new User()
            {
                First_Name = userDto.First_Name,
                Last_Name= userDto.Last_Name,
                Email = userDto.Email,
                Password= userDto.Password,
                Phone= userDto.Phone,
                Gender= userDto.Gender,
                Type= userDto.Type,
                User_Picture= userDto.User_Picture,
                User_Id = userDto.UserId,




            };

           await _usersServices.RegisterUser(user);



        }
    }
}
