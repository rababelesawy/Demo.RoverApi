﻿using Rover.Core.Dtos;
using Rover.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rover.Core.Interfaces
{
    public  interface IGenericRepository<T> where T : BaseEntity
    {
      

        Task SaveTripAsync(Trip trip);
        Task Edit(T entity);
        void Delete(T entity);
       Task SaveAsync(T entity);
        Task<T?> GetAsync(int id);
        IQueryable<T> GetAllAsync();
      
        Task AddAsync(T entity);
        Task<T?> GetByIdAsync(int id);
        IQueryable<T> GetAll();
        Task SaveChangesAsync();

    }
}
