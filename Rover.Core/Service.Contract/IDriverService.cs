using Rover.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rover.Core.Service.Contract
{
    public  interface IDriverService
    {
        Task<int> CreateDriverAsync(Driver driver);
    }
}
