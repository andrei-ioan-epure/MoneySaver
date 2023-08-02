using DomainLayer.Context;
using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PresentationLayer;
using ServiceLayer.DtoModels;
using System.Net.Http;

namespace IntegrationTests
{
    public class UserControllerTests : IDisposable
    {
        private BlogDbContext _blogDbContext;
        private HttpClient _httpClient;
        private IList<User> _usersToBeRemoved = new List<User>();

        public UserControllerTests() {
            var connectionString = @"Server=.\SQLExpress;Database=MoneySaverSP23DB;Trusted_Connection=True;TrustServerCertificate=true";
            var options = new DbContextOptionsBuilder<BlogDbContext>()
                .UseSqlServer(new SqlConnection(connectionString))
                .Options;
            _blogDbContext = new BlogDbContext(options);
            var webApplicationFactory = new WebApplicationFactory<Program>();
            _httpClient = webApplicationFactory.CreateDefaultClient();
        }
        public void Dispose()
        {
            CleanupDatabase();
        }
        private void CleanupDatabase() {

            _blogDbContext.Users.RemoveRange(_usersToBeRemoved);
            _blogDbContext.SaveChanges();

            
        }
        [Fact]
        public async Task GetAll_ShouldReturnAllUsers_WhenRequestAsync()
        {
            var user = new User("test", "test", "test", "test", false, null)
            {
                Id = 1
            };
            _blogDbContext.Users.Add(user);
            _blogDbContext.SaveChanges();

            _usersToBeRemoved.Add(user);

            //Act
            var response = await _httpClient.GetAsync("/User/get");

            //Assert
            var responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            List<UserDto> allUsers = new();
            if (!string.IsNullOrWhiteSpace(responseContent))
            {
                allUsers = JsonConvert.DeserializeObject<List<UserDto>>(responseContent);
            }

            Assert.True(response.IsSuccessStatusCode);
            Assert.True(allUsers?.Count() > 0);
        }
    }
}