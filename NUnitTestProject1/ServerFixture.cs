using Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Net.Http;

namespace Reader.Tests
{
    public class ServerFixture : IDisposable
    {
        public readonly HttpClient client;

        public ServerFixture()
        {
            var webHost = new WebHostBuilder()
                .UseEnvironment("Development")
                .UseStartup<Startup>();

            var server = new TestServer(webHost);

            this.client = server.CreateClient();
            this.client.Timeout = TimeSpan.FromMinutes(30);
        }

        public void Dispose()
        {
            this.client.Dispose();
        }
    }
}
