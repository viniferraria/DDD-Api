using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Reader.Tests
{
    public class ZooControllerTest : IClassFixture<ServerFixture>
    {
        private readonly ServerFixture _fixture;

        public ZooControllerTest(ServerFixture fixture)
        {
            this._fixture = fixture;
        }

        [Theory]
        [InlineData("Get")]
        public async Task ZooGetAllTestAsync(string method)
        {
            // arrange
            var request = new HttpRequestMessage(new HttpMethod(method), "/zoo");

            //act
            var response = await _fixture.client.SendAsync(request);

            //assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("Post")]
        public async Task ZooCreateNewTestAsync(string method)
        {
            // arrange
            var request = new HttpRequestMessage(new HttpMethod(method), "/zoo/add");
            using (var stringContent = new StringContent("{\"name\": \"ZooTest\", \"specie\": \"SpecieTest\"}", Encoding.UTF8, "application/json"))
                request.Content = stringContent;

            //act
            var response = await _fixture.client.SendAsync(request);
            var responseBody = await response.Content.ReadAsStringAsync();

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
            var response = await _fixture.client.SendAsync(request);

            //assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("Put", "2168")]
        public async Task ZooUpdateByIdTestAsync(string method, string id)
        {
            // arrange
            var request = new HttpRequestMessage(new HttpMethod(method), $"/zoo/update/{id}");
            using (var stringContent = new StringContent("{\"id\": " + id + ", \"name\": \"ZooTest\", \"specie\": \"SpecieTest\"}", Encoding.UTF8, "application/json"))
                request.Content = stringContent;

            //act
            var response = await _fixture.client.SendAsync(request);

            //assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Theory]
        [InlineData("Delete", 2169)]
        public async Task ZooDeleteByIdTestAsync(string method, int id)
        {
            // arrange
            var request = new HttpRequestMessage(new HttpMethod(method), $"/zoo/{id}");
            //act
            var response = await _fixture.client.SendAsync(request);

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
            var response = await _fixture.client.SendAsync(request);

            //assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}