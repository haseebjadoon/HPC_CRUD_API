using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPCAPI.Models
{
    public class ShipRepository : IShipRepository
    {
        private readonly ShipContext _shipContext;

        public ShipRepository(ShipContext shipContext)
        {
            this._shipContext = shipContext;
        }

        public async Task<Ship> AddShip(Ship ship)
        {
            var result = await _shipContext.Ship.AddAsync(ship);
            await _shipContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteShip(int shipId)
        {
            var result = await _shipContext.Ship.FirstOrDefaultAsync(s => s.ShipId == shipId);

            if (result != null)
            {
                _shipContext.Ship.Remove(result);
                await _shipContext.SaveChangesAsync();
            }
        }

        public async Task<Ship> GetShip(int shipId)
        {
            return await _shipContext.Ship.FirstOrDefaultAsync(s => s.ShipId == shipId);
        }

        public async Task<IEnumerable<Ship>> GetShips()
        {
            return await _shipContext.Ship.ToListAsync();
        }

        public async Task<Ship> UpdateShip(Ship ship)
        {
            var result = await _shipContext.Ship.FirstOrDefaultAsync(s => s.ShipId == ship.ShipId);

            if (result != null)
            {
                result.Code = ship.Code;
                result.Name = ship.Name;
                result.Length = ship.Length;
                result.Width = ship.Width;

                await _shipContext.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
