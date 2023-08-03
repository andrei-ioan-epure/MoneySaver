using System.Text;
using DomainLayer.Context;
using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PresentationLayer;
using RepositoryLayer;
using ServiceLayer.DtoModels;
using ServiceLayer.Services;

namespace IntegrationTests
{
    public class UserControllerTests : IDisposable
    {
        private BlogDbContext _blogDbContext;
        private HttpClient _httpClient;
        private IList<User> _usersToBeRemoved = new List<User>();

        public UserControllerTests()
        {
            var connectionString = "Server=localhost,1433;Database=MoneySaverSP23DB;User=SA;Password=Admin123;TrustServerCertificate=True;Encrypt=false;";
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
        private void CleanupDatabase()
        {

            _blogDbContext.Users.RemoveRange(_usersToBeRemoved);
            _blogDbContext.SaveChanges();


        }

        [Fact]
        public async Task GetAll_ShouldReturnAllUsers_WhenRequestAsync()
        {
            var user = new User("test", "test", "test", "test", false, new byte[] { 0x12 });

            _blogDbContext.Users.Add(user);
            _blogDbContext.SaveChanges();

            _usersToBeRemoved.Add(user);

            //Act
            var response = await _httpClient.GetAsync("api/User/get");

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

        [Fact]
        public async Task GetUserById_ShouldReturnUser_WhenRequestValid()
        {
            var user = new User("test", "test", "test", "test", false, new byte[] { 0x12 });

            _blogDbContext.Users.Add(user);
            _blogDbContext.SaveChanges();

            _usersToBeRemoved.Add(user);

            //Act
            var response = await _httpClient.GetAsync("api/User/get/" + user.Id);
            var result = await response.Content.ReadAsStringAsync();

            //Assert
            int.TryParse(result, out var petId);

            //Assert
            var responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            UserDto User = null;
            if (!string.IsNullOrWhiteSpace(responseContent))
            {
                User = JsonConvert.DeserializeObject<UserDto>(responseContent);
            }

            Assert.True(response.IsSuccessStatusCode);
            Assert.NotNull(User);
        }

        /*  [Fact]
          public async Task AddUser_ShouldSucceed_WhenInputValid()
                 {
              //Arrange
              var userDto = new UserDto(1, "test", "test", "test", "test", false);

              var objectData = new Dictionary<string, dynamic>
              {
                  {"userName", userDto.UserName },
                  {"fullname", userDto.FullName},
                  {"email", userDto.Email},
                  {"password", userDto.Password },
                  {"iscreator", userDto.IsCreator }
             };

              //Act
              var response = await _httpClient.PostAsync("api/user/add", createdtorequestcontent(objectData));


              var result = await response.Content.ReadAsStringAsync();

              //Assert
              int.TryParse(result, out var petId);

              _usersToBeRemoved.Add(new User(userDto.UserName, userDto.FullName,
                    userDto.Email, userDto.Password, userDto.IsCreator,null )
               {
                  Id =19
                });

              Assert.True(response.IsSuccessStatusCode);
          }


          public StringContent createdtorequestcontent(Dictionary<string, dynamic> objectdata)
          {
              var jsoncreatecustomer = JObject.FromObject(objectdata);
              var createcustomerrequestcontent = new StringContent(jsoncreatecustomer.ToString(), Encoding.UTF8, "application/json");

              return createcustomerrequestcontent;
          }
      */
    }
}