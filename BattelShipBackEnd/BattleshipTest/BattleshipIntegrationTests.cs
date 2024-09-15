using BattelShipBackEnd;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipTest
{
    public class BattleshipIntegrationTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public BattleshipIntegrationTests()
        {
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());  // Use your actual Startup class here
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task StartNewGame_ShouldReturnGameBoard()
        {
            // Act
            var response = await _client.PostAsync("/api/battleship/start", null);
            response.EnsureSuccessStatusCode();

            // Assert
            var content = await response.Content.ReadAsStringAsync();
            Assert.NotNull(content);  // Check if content exists
                                      // Additional assertions can be made based on your response structure
        }
    }
}
