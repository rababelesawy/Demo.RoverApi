using Microsoft.EntityFrameworkCore;
using Rover.Core.Entities;
using Rover.Core.Service.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rover.Service
{
    public class UsersServices : IUsersServices
    {


        private readonly DbContext _context;

        public UsersServices(DbContext context)
        {
            _context = context;
        }
        public Task<User> ChangeUserType(string userId, int newType)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserById(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task RegisterUser(User user)
        {
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }
}
