using Microsoft.EntityFrameworkCore;
using Rover.Core.Dtos;
using Rover.Core.Entities;
using Rover.Core.Interfaces;
using Rover.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rover.Repository.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _dbContext;

        public object User => throw new NotImplementedException();

        public GenericRepository(StoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveTripAsync(Trip trip)
        {
            await _dbContext.Trips.AddAsync(trip);
            await _dbContext.SaveChangesAsync();
        }



        public void Delete(T entity)
        {
            _dbContext.Update(entity);
            _dbContext.SaveChanges();
        }
        public async Task Edit(T entity)
        {
            _dbContext.Update(entity);
            _dbContext.SaveChanges();
        }

        public async Task SaveAsync(T entity)
        {
            _dbContext.AddAsync(entity);
            _dbContext.SaveChanges();
        }

        public async Task<T?> GetAsync(int id)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(i => i.Id == id);
        }

        public IQueryable<T> GetAllAsync()
        {
            return _dbContext.Set<T>();
        }
        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> RegisterUserAsync(UserRegistrationDto registrationDto)
        {
            throw new NotImplementedException();
        }

        public Task<UserDetailsDto> GetUserDetailsAsync(string userId)
        {
            throw new NotImplementedException();
        }
     

        public async Task AddAsync(T entity)
        {
            await _dbContext.AddAsync(entity);
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public IQueryable<T> GetAll()
        {
            return _dbContext.Set<T>();
        }
    }
}
    