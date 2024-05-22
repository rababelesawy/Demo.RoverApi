using Microsoft.EntityFrameworkCore;
using Rover.Core.Dtos;
using Rover.Core.Entities;
using Rover.Core.Service.Contract;
using Rover.Repository.Data;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rover.Service
{
    public class UsersServices : IUsersServices
    {
        private readonly StoreContext _context;

        public UsersServices(StoreContext context)
        {
            _context = context;
        }
        #region Insert User Data
        public async Task<bool> InsertUserData(UserDto userData)
        {
            try
            {
                if (string.IsNullOrEmpty(userData.UserId))
                {
                    throw new ArgumentException("User ID cannot be empty or null.");
                }

                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.User_Id == userData.UserId);
                if (existingUser != null)
                {
                    throw new Exception("User already exists.");
                }

                var user = new User()
                {
                    User_Id = userData.UserId,
                    User_Picture = userData.User_Picture,
                    First_Name = userData.First_Name,
                    Last_Name = userData.Last_Name,
                    Password = userData.Password,
                    Email = userData.Email,
                    Phone = userData.Phone,
                    Gender = userData.Gender,
                    Type = 1
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to insert user data.", ex);
            }
        }

        #endregion


        #region Get User Data By Id
        public async Task<UserDto> GetUserData(string userId)
        {
            try
            {

                var user = await _context.Users.FirstOrDefaultAsync(u => u.User_Id == userId);
                if (user != null)
                {
                    return new UserDto()
                    {
                        UserId = user.User_Id,
                        User_Picture = user.User_Picture,
                        First_Name = user.First_Name,
                        Last_Name = user.Last_Name,
                        Password = user.Password,
                        Email = user.Email,
                        Phone = user.Phone,
                        Gender = user.Gender,
                        Type = user.Type,
                    };
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Update User Type
        public async Task<bool> UpdateUserType(string userId, int userType)
        {
            try
            {
                if (string.IsNullOrEmpty(userId))
                {
                    throw new ArgumentException("User ID cannot be empty or null.");
                }

                var user = await _context.Users.FirstOrDefaultAsync(u => u.User_Id == userId);
                if (user != null)
                {
                    user.Type = userType;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false; // User not found
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update user type.", ex);
            }

        }

        #endregion

        #region Delete User
        public async Task<bool> DeleteUser(string userId)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.User_Id == userId);
                if (user != null)
                {
                    _context.Users.Remove(user);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false; // User not found
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region Update User Data
        public async Task<bool> UpdateUserData(UserDto userData)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.User_Id == userData.UserId);
                if (user != null)
                {
                    user.User_Picture = userData.User_Picture;
                    user.First_Name = userData.First_Name;
                    user.Last_Name = userData.Last_Name;
                    user.Password = userData.Password;
                    user.Email = userData.Email;
                    user.Phone = userData.Phone;
                    user.Gender = userData.Gender;
                    user.Type = userData.Type;

                    await _context.SaveChangesAsync();
                    return true;
                }
                return false; // User not found
            }
            catch (Exception)
            {
                return false;
            }

        }


        #endregion
        
        #region  GetUserID

        public async Task<User> GetUserId(string userId)
        {
            try
            {
                if (userId == null)
                {
                    throw new ArgumentException("User ID cannot be null or empty", nameof(userId));
                }

                var user = await _context.Users.FirstOrDefaultAsync(u => u.User_Id == userId);

                if (user == null)
                {
                    throw new ArgumentException("User not found");
                }

                return (user);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}
    