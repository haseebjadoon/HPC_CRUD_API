using HPCAPI.Models;
using HPCAPI.Test.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace HPCAPI.Test
{
    class ShipUpdateTests : ShipTestsBase
    {
        public ShipUpdateTests(ShipTestsDbWAF<Startup> factory) : base(factory)
        {

        }


        [Theory]
        [InlineData(100, "AAAA-1234-A1", "Any", 20, 30, HttpStatusCode.Created)]
        [InlineData(1, "UUUU-3434-A1", "Any", 20, 30, HttpStatusCode.Created)]
        public async Task UpdateShipTestBasicAsync(int shipId, string code, string name, double length, double width, HttpStatusCode responseCode)
        {
            var scoreFactory = _factory.Services;
            using (var scope = scoreFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ShipContext>();
                await DBUtility.InitilizeDBWithDataAsync(context);

                var request = new HttpRequestMessage(HttpMethod.Put, $"api/ships/{shipId}");
                request.Content = new StringContent(JsonSerializer.Serialize(new Ship
                {
                    Code = code,
                    Name = name,
                    Length = length,
                    Width = width
                }), Encoding.UTF8, "application/json");
                var response = await _httpClient.SendAsync(request);


                Ship ship = await context.Ship.FirstOrDefaultAsync(s => s.ShipId == shipId);

                Assert.Null(ship);
                Assert.Equal(responseCode, response.StatusCode);
            }

        }

        [Theory]
        [InlineData(100, "AAAA-1234-A1", "Any", 20, 30, HttpStatusCode.Created)]
        [InlineData(1, "UUUU-3434-A1", "Any", 20, 30, HttpStatusCode.Created)]
        public async Task UpdateShipDataTestAsync(int shipId, string code, string name, double length, double width, HttpStatusCode responseCode)
        {
            var scoreFactory = _factory.Services;
            using (var scope = scoreFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ShipContext>();
                await DBUtility.InitilizeDBWithDataAsync(context);

                var request = new HttpRequestMessage(HttpMethod.Put, $"api/ships/{shipId}");

                var shipUpdated = new Ship
                {
                    Code = code,
                    Name = name,
                    Length = length,
                    Width = width
                };
                request.Content = new StringContent(JsonSerializer.Serialize(shipUpdated), Encoding.UTF8, "application/json");
                var response = await _httpClient.SendAsync(request);


                Ship ship = await context.Ship.FirstOrDefaultAsync(s => s.ShipId == shipId);

                Assert.Null(ship);
                Assert.True(ship.Equals(shipUpdated));
                Assert.Equal(responseCode, response.StatusCode);
            }
        }
    }
}