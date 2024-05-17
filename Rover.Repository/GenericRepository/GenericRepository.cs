using Microsoft.EntityFrameworkCore;
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
    public class GenericRepository<T> :IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _dbContext;

        public GenericRepository(StoreContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task SaveTripAsync(Trip trip)
        {
            await _dbContext.Trips.AddAsync(trip);
            await _dbContext.SaveChangesAsync();
        }



        public void Delete(T entity)
        { 
              _dbContext.Remove(entity);
            _dbContext.SaveChanges();
        }
        public void Edit(T entity)
        {
            _dbContext.Update(entity);
            _dbContext.SaveChanges();
        }

        public async Task SaveAsync(T entity)
        {
            _dbContext.AddAsync(entity);
            _dbContext.SaveChanges();
        }
    }
        
    }


