using HPCAPI.Models;
using HPCAPI.Test.Utilities;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace HPCAPI.Test
{
    public class ShipTestsBase : IClassFixture<ShipTestsDbWAF<Startup>>
    {
        protected WebApplicationFactory<Startup> _factory;
        protected readonly HttpClient _httpClient;

        public ShipTestsBase(ShipTestsDbWAF<Startup> factory)
        {
            _factory = factory;
            _httpClient = factory.CreateClient();
        }
        
    }
}