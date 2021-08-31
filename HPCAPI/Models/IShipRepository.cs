using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPCAPI.Models
{
    public interface IShipRepository
    {
        Task<IEnumerable<Ship>> GetShips();
        Task<Ship> GetShip(int shipId);
        Task<Ship> AddShip(Ship ship);
        Task<Ship> UpdateShip(Ship ship);
        Task DeleteShip(int shipId);
    }
}
