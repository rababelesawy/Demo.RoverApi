﻿using Rover.Core.Dtos;
using Rover.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rover.Core.Service.Contract
{
    public  interface IUsersServices
    {
        Task<bool> InsertUserData(UserDto userData);
        Task<string> UpdateUserType(string userId, int userType);
        Task<string> DeleteUser(string userId, string password);
        Task<bool> UpdateUserData(UserDto userData);
        Task<UserDto> GetUserData(string userId);
        Task <User> GetUserId(string userId);
        Task<CarUserDto> GetUserProfileAsync(string userId);

    }
}
