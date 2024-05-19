using Rover.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rover.Core.Service.Contract
{
    public  interface IUserServices
    {

        Task<string> CreateUserAsync(User user);

        Task SaveUserAsync(User user);
    }
}
