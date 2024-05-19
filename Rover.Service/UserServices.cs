using Microsoft.EntityFrameworkCore;
using Rover.Core.Entities;
using Rover.Core.Interfaces;
using Rover.Core.Service.Contract;
using Rover.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rover.Service
{
    public class UserServices : IUserServices
    {
        private readonly IUserServices _userServices;
        private readonly StoreContext _dbContext;

        public UserServices(StoreContext dbContext, IUserServices userServices) {
            _dbContext = dbContext;
            _userServices = userServices;
            
        }

       

        public async Task SaveUserAsync(User user)
        {
            await  _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }


        async Task<string> IUserServices.CreateUserAsync(User user)
        {
            await _userServices.SaveUserAsync(user);



            return (user.User_Id);
        }


      

    }
}
