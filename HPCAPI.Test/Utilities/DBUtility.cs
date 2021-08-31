using HPCAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HPCAPI.Test.Utilities
{
    public class DBUtility
    {
        public static async Task InitilizeDBWithDataAsync(ShipContext db)
        {
            db.Ship.RemoveRange(db.Ship);
            var ship = new Ship
            {
                Code = "ABCD-1234-E5",
                Name = "InitialShip1",
                Length = 10.5,
                Width = 20.2
            };
            await db.Ship.AddAsync(ship);

            var ship1 = new Ship
            {
                Code = "FGHI-6789-E0",
                Name = "InitialShip2",
                Length = 4.5,
                Width = 9.2
            };
            await db.Ship.AddAsync(ship1);
            await db.SaveChangesAsync();
        }
    }
}
