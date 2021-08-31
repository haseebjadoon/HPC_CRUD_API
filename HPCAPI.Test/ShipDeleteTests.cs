using HPCAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HPCAPI.Test.Utilities
{
    public class ShipDeleteTests : ShipTestsBase
    {
        public ShipDeleteTests(ShipTestsDbWAF<Startup> factory) : base(factory)
        {

        }

        [Theory]
        [InlineData(20, HttpStatusCode.NotFound)]
        [InlineData(1, HttpStatusCode.OK)]
        public async Task DeleteShipTestsAsync(int shipId, HttpStatusCode responseCode)
        {
            var scoreFactory = _factory.Services;
            using (var scope = scoreFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ShipContext>();
                await DBUtility.InitilizeDBWithDataAsync(context);

                var request = new HttpRequestMessage(HttpMethod.Delete, $"api/ships/{shipId}");
                var response = await _httpClient.SendAsync(request);


                Ship ship = await context.Ship.FirstOrDefaultAsync(s => s.ShipId == shipId);
                
                if(responseCode == HttpStatusCode.OK)
                    Assert.Null(ship);
                
                Assert.Equal(responseCode, response.StatusCode);
            }
        }

    }
}
