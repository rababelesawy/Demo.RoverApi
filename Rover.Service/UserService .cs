using Microsoft.EntityFrameworkCore;
using Rover.Core.Dtos;
using Rover.Core.Entities;
using Rover.Core.Interfaces;
using Rover.Core.Service.Contract;
using Rover.Repository.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rover.Service
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _genericrepo;

        public UserService(IGenericRepository<User> genericrepo)
        {
            _genericrepo = genericrepo;
        }

       public async Task<bool> RegisterUserAsync(UserRegistrationDto registrationDto)
        {
            var exists = await _genericrepo.GetAll().AnyAsync(u => u.UserId == registrationDto.UserId);
            if (exists)
            {
                return false;
            }

            var newUser = new User
            {
                UserId = registrationDto.UserId,
                Picture_User = registrationDto.Picture_User,
                First_Name = registrationDto.First_Name,
                Last_Name = registrationDto.Last_Name,
                Password = registrationDto.Password,
                Email = registrationDto.Email,
                Phone = registrationDto.Phone,
                Gender = registrationDto.Gender
            };

            await _genericrepo.AddAsync(newUser);
            await _genericrepo.SaveChangesAsync();

            return true;
        }

        public async Task<UserDetailsDto> GetUserDetailsAsync(string userId)
        {
            var user = await _genericrepo.GetAll().FirstOrDefaultAsync(u => u.UserId == userId);
            if (user == null) return null;

            return new UserDetailsDto
            {
                UserId = user.UserId,
                Picture_User = user.Picture_User,
                First_Name = user.First_Name,
                Last_Name = user.Last_Name,
                Email = user.Email,
                Phone = user.Phone,
                Gender = (int)user.Gender
            };
        }
    }
}