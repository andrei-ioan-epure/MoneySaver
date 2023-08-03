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
	public class ArticleControllerTest : IDisposable
	{
        private BlogDbContext _blogDbContext;
        private HttpClient _httpClient;
        private IList<Article> _articlesToBeRemoved = new List<Article>();

        public ArticleControllerTest()
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

            _blogDbContext.Articles.RemoveRange(_articlesToBeRemoved);
            _blogDbContext.SaveChanges();
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllUsers_WhenRequestAsync()
        {
            var article = new Article("test", new DateTime(), new DateTime(), "test", "test", "test", "test", "test", "test", 1);

            _blogDbContext.Articles.Add(article);
            _blogDbContext.SaveChanges();

            _articlesToBeRemoved.Add(article);

            //Act
            var response = await _httpClient.GetAsync("api/Article/get");

            //Assert
            var responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            List<UserDto> allArticles = new();
            if (!string.IsNullOrWhiteSpace(responseContent))
            {
                allArticles = JsonConvert.DeserializeObject<List<UserDto>>(responseContent);
            }

            Assert.True(response.IsSuccessStatusCode);
            Assert.True(allArticles?.Count() > 0);
        }


        [Fact]
        public async Task GetArticleById_ShouldReturnArticle_WhenRequestValid()
        {
            var article = new Article("test", new DateTime(), new DateTime(), "test", "test", "test", "test", "test", "test", 1);

            _blogDbContext.Articles.Add(article);
            _blogDbContext.SaveChanges();

            _articlesToBeRemoved.Add(article);

            //Act
            var response = await _httpClient.GetAsync("api/Article/get/" + article.Id);

            //Assert
            var responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            ArticleDto articleDto = null;
            if (!string.IsNullOrWhiteSpace(responseContent))
            {
                articleDto = JsonConvert.DeserializeObject<ArticleDto>(responseContent);
            }

            Assert.True(response.IsSuccessStatusCode);
            Assert.NotNull(articleDto);
        }
    }
}

