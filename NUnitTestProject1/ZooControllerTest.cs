using Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Domain.Models;
using Xunit;
using System.Text;
using Newtonsoft.Json;

namespace Reader.Tests
{
    public class ZooControllerTest
    {
        private readonly HttpClient _client;
        private Zoo ZooTest;

        public ZooControllerTest()
        {
            var server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Development")
                .UseStartup<Startup>());

            _client = server.CreateClient();
        }

        [Theory]
        [InlineData("Get")]
        public async Task ZooGetAllTestAsync(string method)
        {
            // arrange
            var request = new HttpRequestMessage(new HttpMethod(method), "/zoo");
            
            //act
            var response = await _client.SendAsync(request);

            //assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("Post")]
        public async Task ZooCreateNewTestAsync(string method)
        {
            // arrange
            var request = new HttpRequestMessage(new HttpMethod(method), $"/zoo/add");
            using (var stringContent = new StringContent("{\"name\": \"ZooTest\", \"specie\": \"SpecieTest\"}", Encoding.UTF8, "application/json"))
                request.Content = stringContent;

            //act
            var response = await _client.SendAsync(request);
            var responseBody = await response.Content.ReadAsStringAsync();
            this.ZooTest = JsonConvert.DeserializeObject<Zoo>(responseBody);

            //assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Theory]
        [InlineData("Get", "1")]
        public async Task ZooGetByIdTestAsync(string method, string id)
        {
            // arrange
            var request = new HttpRequestMessage(new HttpMethod(method), $"/zoo/{id}");
            //act
            var response = await _client.SendAsync(request);

            //assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("Put")]
        public async Task ZooUpdateByIdTestAsync(string method)
        {
            // arrange
            var request = new HttpRequestMessage(new HttpMethod(method), $"/zoo/update/{this.ZooTest.Id}");
            this.ZooTest.Name = "Simba";
            using (var stringContent = new StringContent(JsonConvert.SerializeObject(this.ZooTest), Encoding.UTF8, "application/json"))
            request.Content = stringContent;
            
            //act
            var response = await _client.SendAsync(request);

            //assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Theory]
        [InlineData("Delete")]
        public async Task ZooDeleteByIdTestAsync(string method)
        {
            // arrange
            var request = new HttpRequestMessage(new HttpMethod(method), $"/zoo/{this.ZooTest.Id}");
            //act
            var response = await _client.SendAsync(request);

            //assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        
        [Theory]
        [InlineData("Get")]
        public async Task ZooBulkInsert(string method)
        {
            // arrange
            var request = new HttpRequestMessage(new HttpMethod(method), $"/zoo/read");
            //act
            var response = await _client.SendAsync(request);

            //assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}