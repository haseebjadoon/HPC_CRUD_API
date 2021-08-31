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
using System.Threading.Tasks;
using Xunit;

namespace HPCAPI.Test
{
    public class ShipGetTests : ShipTestsBase
    {
        public ShipGetTests(ShipTestsDbWAF<Startup> factory) : base(factory)
        {

        }

        [Theory]
        [InlineData(20, HttpStatusCode.NotFound)]
        [InlineData(1, HttpStatusCode.OK)]
        public async Task GetShipTestsAsync(int shipId, HttpStatusCode responseCode)
        {
            var scoreFactory = _factory.Services;
            using (var scope = scoreFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ShipContext>();
                await DBUtility.InitilizeDBWithDataAsync(context);

                var request = new HttpRequestMessage(HttpMethod.Get, $"api/ships/{shipId}");
                var response = await _httpClient.GetAsync(request.RequestUri);
                Ship ship = await context.Ship.FirstOrDefaultAsync(s => s.ShipId == shipId);

                if(responseCode == HttpStatusCode.OK)
                    Assert.NotNull(ship);
                Assert.Equal(responseCode, response.StatusCode);
            }
        }

        [Fact]
        public async Task GetShipsTest()
        {
            var scoreFactory = _factory.Services;
            using (var scope = scoreFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ShipContext>();
                await DBUtility.InitilizeDBWithDataAsync(context);

                var request = new HttpRequestMessage(HttpMethod.Get, $"api/ships");
                var response = await _httpClient.GetAsync(request.RequestUri);

                //Assert.True(response.Content.Equals(shipAdded));
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task GetShipsVerifyDataTestAsync()
        {
            var scoreFactory = _factory.Services;
            using (var scope = scoreFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ShipContext>();
                await DBUtility.InitilizeDBWithDataAsync(context);

                var request = new HttpRequestMessage(HttpMethod.Get, $"api/ships");
                var response = await _httpClient.GetAsync(request.RequestUri);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        //[Theory]
        //[InlineData(1, HttpStatusCode.OK)]
        //public async Task GetShipVerifyDataTestsAsync(int shipId, HttpStatusCode responseCode)
        //{
        //    var scoreFactory = _factory.Services;
        //    using (var scope = scoreFactory.CreateScope())
        //    {
        //        var context = scope.ServiceProvider.GetService<ShipContext>();

        //        //await DBUtility.InitilizeDBWithDataAsync(context);
        //        var shipAdded = new Ship
        //        {
        //            Code = "TEST-1234-E5",
        //            Name = "TestVerify",
        //            Length = 10.5,
        //            Width = 20.2
        //        };
        //        await context.Ship.AddAsync(shipAdded);
        //        await context.SaveChangesAsync();

        //        var request = new HttpRequestMessage(HttpMethod.Get, $"api/ships/{shipId}");
        //        var response = await _httpClient.GetAsync(request.RequestUri);

        //        //Check data here
        //        Assert.Equal(responseCode, response.StatusCode);
        //    }
        //}
    }
}
