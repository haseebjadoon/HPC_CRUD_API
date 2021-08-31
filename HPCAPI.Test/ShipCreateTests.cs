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

namespace HPCAPI.Test.Utilities
{
    public class ShipCreateTests : ShipTestsBase
    {
        public ShipCreateTests(ShipTestsDbWAF<Startup> factory) : base(factory)
        {

        }

        [Theory]
        [InlineData("AAAA-1234-A1", "Any", 20, 30, HttpStatusCode.Created)]
        [InlineData("AÜAA-1123-ö1", "Any", 20, 30, HttpStatusCode.Created)]
        [InlineData("ALAA-1123-ö1", "Any", 20, 30, HttpStatusCode.Created)]
        [InlineData("ALAA-1123-ü1", "Any", 20, 30, HttpStatusCode.Created)]
        [InlineData("AAAA-1123-A1", "Any", 20, 30, HttpStatusCode.Created)]
        [InlineData("", "Any", 20, 30, HttpStatusCode.BadRequest)]
        [InlineData("AAAA-F456-A1", "Any", 20, 30, HttpStatusCode.BadRequest)]
        [InlineData("AAAAA3453AA1", "Any", 20, 30, HttpStatusCode.BadRequest)]
        public async Task CreateShipTestsAsync(string code, string name, double length, double width, HttpStatusCode responseCode)
        {
            var scoreFactory = _factory.Services;
            using (var scope = scoreFactory.CreateScope())
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "api/ships");

                request.Content = new StringContent(JsonSerializer.Serialize(new Ship
                {
                    Code = code,
                    Name = name,
                    Length = length,
                    Width = width
                }), Encoding.UTF8, "application/json");
                var response = await _httpClient.SendAsync(request);

                Assert.Equal(responseCode, response.StatusCode);
            }
        }
    }
}
