using System;
using DomainLayer.Context;
using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PresentationLayer;
using ServiceLayer.DtoModels;

namespace IntegrationTests
{
    public class CommentControllerTest : IDisposable
    {
        private BlogDbContext _blogDbContext;
        private HttpClient _httpClient;
        private IList<Comment> _commentsToBeRemoved = new List<Comment>();
        private IList<Article> _articlesToBeRemoved = new List<Article>();
        private IList<User> _usersToBeRemoved = new List<User>();

        public CommentControllerTest()
        {
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

        private void CleanupDatabase()
        {

            _blogDbContext.Comments.RemoveRange(_commentsToBeRemoved);
            _blogDbContext.Articles.RemoveRange(_articlesToBeRemoved);
            _blogDbContext.Users.RemoveRange(_usersToBeRemoved);
            _blogDbContext.SaveChanges();
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllComments_WhenRequestAsync()
        {
            var user = new User("test", "test", "test231231231", "test", false, new byte[] { 0x12 })
           ;
        
            _blogDbContext.Users.Add(user);
            _blogDbContext.SaveChanges();

            _usersToBeRemoved.Add(user);

            var article = new Article("test", new DateTime(), new DateTime(), "test", "test", "test", "test", "test", "test", user.Id)
           ;
            _blogDbContext.Articles.Add(article);
            _blogDbContext.SaveChanges();

            _articlesToBeRemoved.Add(article);


            var comment = new Comment("Test message", new DateTime(), "Test", user.Id, article.Id)
            ;

            _blogDbContext.Comments.Add(comment);
            _blogDbContext.SaveChanges();

            _commentsToBeRemoved.Add(comment);

            //Act
            var response = await _httpClient.GetAsync("api/Comment/get");

            //Assert
            var responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            List<CommentDto> allComments = new();
            if (!string.IsNullOrWhiteSpace(responseContent))
            {
                allComments = JsonConvert.DeserializeObject<List<CommentDto>>(responseContent);
            }

            Assert.True(response.IsSuccessStatusCode);
            Assert.True(allComments?.Count() > 0);
        }


        [Fact]
        public async Task GetCommentById_ShouldReturnComment_WhenRequestValid()
        {

            var user = new User("test", "test", "test231231231", "test", false, new byte[] { 0x12 })
          ;

            _blogDbContext.Users.Add(user);
            _blogDbContext.SaveChanges();

            _usersToBeRemoved.Add(user);

            var article = new Article("test", new DateTime(), new DateTime(), "test", "test", "test", "test", "test", "test", user.Id)
           ;
            _blogDbContext.Articles.Add(article);
            _blogDbContext.SaveChanges();

            _articlesToBeRemoved.Add(article);


            var comment = new Comment("Test message", new DateTime(), "Test", user.Id, article.Id)
          ;

            _blogDbContext.Comments.Add(comment);
            _blogDbContext.SaveChanges();

            _commentsToBeRemoved.Add(comment);

            //Act
            var response = await _httpClient.GetAsync("api/Comment/get/" + comment.Id);

            //Assert
            var responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            CommentDto commentDto = null;
            if (!string.IsNullOrWhiteSpace(responseContent))
            {
                commentDto = JsonConvert.DeserializeObject<CommentDto>(responseContent);
            }

            Assert.True(response.IsSuccessStatusCode);
            Assert.NotNull(commentDto);
        }
    }
}

