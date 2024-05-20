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
        Task RegisterUser(User user);
        Task<User> GetUserById(string userId);
        Task<User> ChangeUserType(string userId, int newType);


    }
}
