using DomainLayer.Models;
using FluentAssertions;
using Moq;
using RepositoryLayer;
using ServiceLayer.Contracts;
using ServiceLayer.DtoModels;
using ServiceLayer.Services;
using System.ComponentModel;

namespace UnitTests
{
    public class ArticleServiceTests
    {
        private ArticleService _systemUnderTest;
        private Mock<IRepository<Article>> _articleRepositoryMock;
        private Mock<IRepository<User>> _userRepositoryMock;
        private Mock<IRepository<Comment>> _commentRepositoryMock;
        private IFilterService _filterService;
        private IUserService _userService;
        public ArticleServiceTests()
        {
            _articleRepositoryMock = new Mock<IRepository<Article>>();
            _commentRepositoryMock = new Mock<IRepository<Comment>>();
            _userRepositoryMock = new Mock<IRepository<User>>();
            _filterService = new FilterService(_userRepositoryMock.Object);
            _userService = new UserService(_userRepositoryMock.Object);
            _systemUnderTest = new ArticleService(_articleRepositoryMock.Object, _filterService, _userRepositoryMock.Object, _commentRepositoryMock.Object,_userService);
        }

  
        [Fact]
        public void GetById_ShouldBeNull_WhenIdNotExist()
        {
            // Arrange
            int articleId = 10;


            _articleRepositoryMock.Setup(repo => repo.Get(articleId))
               .Returns((Article)null);


            // Act 
            var result = _systemUnderTest.Get(articleId);

            //Assert

            result.Should().BeNull();
        }

        [Fact]
        public void GetById_ShouldNotBeNull_WhenIdExist()
        {
            // Arrange

            int productIdToRetrieve = 1;
            var expectedArticle = new Article("test", new DateTime(), new DateTime(), "test", "test", "test", "test", "test", "test", 1)
            {
                Id = productIdToRetrieve,
            };

            var expectedArticleDto = new ArticleDto(productIdToRetrieve, "test", expectedArticle.Posted, expectedArticle.Expiration, "test", "test", "test", "test", "test", "test", 1);
            _articleRepositoryMock.Setup(r => r.Get(productIdToRetrieve)).Returns(expectedArticle);


            // Act
            var actualProduct = _systemUnderTest.Get(productIdToRetrieve);

            // Assert
            actualProduct.Should().BeEquivalentTo(expectedArticleDto);
        }
        [Fact]
        public void Insert_ShouldSucced_WhenNewArticleIsNotNull()
        {
            // Arrange
            var articleDto = new ArticleDto(1, "test", new DateTime(), new DateTime(), "test", "test", "test", "test", "test", "test", 1);

            // Act
            _systemUnderTest.Insert(articleDto);

            // Assert
            _articleRepositoryMock.Verify(r => r.Insert(It.Is<Article>(a =>
             a.Title == articleDto.Title &&
             a.Posted == articleDto.Posted &&
             a.Expiration == articleDto.Expiration &&
             a.City == articleDto.City &&
             a.Category == articleDto.Category &&
             a.Code == articleDto.Code &&
             a.Store == articleDto.Store &&
             a.Author == articleDto.Author &&
             a.Content == articleDto.Content &&
             a.CreatorId == articleDto.CreatorId
         )), Times.Once);
        }
        [Fact]
        public void Update_ShouldUpdate_WhenArticleInRepository()
        {
            // Arrange

            int articleIdToUpdate = 1;
            var articleDto = new ArticleDto(
            articleIdToUpdate,
            "Updated Article",
            DateTime.Now,
            DateTime.Now.AddDays(14),
            "Updated City",
            "Updated Category",
            "Updated Code",
            "Updated Store",
            "Updated Author",
            "Updated Content",
            2
        );
            // Act
            _systemUnderTest.Update(articleIdToUpdate, articleDto);

            // Assert
            _articleRepositoryMock.Verify(r => r.Update(It.Is<Article>(a =>
                a.Id == articleIdToUpdate &&
                a.Title == articleDto.Title &&
                a.Posted == articleDto.Posted &&
                a.Expiration == articleDto.Expiration &&
                a.City == articleDto.City &&
                a.Category == articleDto.Category &&
                a.Code == articleDto.Code &&
                a.Store == articleDto.Store &&
                a.Author == articleDto.Author &&
                a.Content == articleDto.Content &&
                a.CreatorId == articleDto.CreatorId
            )), Times.Once);
        }
        [Fact]
        public void GetAll_ShouldReturnAllArticles()
        {
            // Arrange
            var articles = new List<Article>
        {
            new Article ("Article 1", DateTime.Now, DateTime.Now.AddDays(7), "City 1", "Category 1", "Code 1", "Store 1", "Author 1", "Content 1", 1),
            new Article( "Article 2", DateTime.Now, DateTime.Now.AddDays(14), "City 2", "Category 2", "Code 2", "Store 2", "Author 2", "Content 2", 2)
        };


            _articleRepositoryMock.Setup(r => r.GetAll()).Returns(articles);

            // Act
            var result = _systemUnderTest.GetAll();

            // Assert
            _articleRepositoryMock.Verify(r => r.GetAll(), Times.Once);


            Assert.Equal(articles.Count, result.Count());


            for (int i = 0; i < articles.Count; i++)
            {
                var expectedArticle = articles[i];
                var actualArticleDto = result.ElementAt(i);

                expectedArticle.Should().BeEquivalentTo(actualArticleDto);
            }
        }


        [Fact]
        public void Delete_ShouldSucceed_WhenIdValid()
        {
            // Arrange
            int entityIdToDelete = 1;

            // Act
            _systemUnderTest.Delete(entityIdToDelete);

            // Assert
            _articleRepositoryMock.Verify(r => r.Delete(entityIdToDelete), Times.Once);
        }
        [Fact]
        public void Delete_ShouldThrowException_WhenNonExistentArticleId()
        {
            // Arrange

            int nonExistentArticleId = 1000;

            _articleRepositoryMock.Setup(r => r.Delete(nonExistentArticleId)).Throws<InvalidOperationException>();

            // Act and Assert
            Assert.Throws<InvalidOperationException>(() => _systemUnderTest.Delete(nonExistentArticleId));

            _articleRepositoryMock.Verify(r => r.Delete(nonExistentArticleId), Times.Once);
        }


        [Fact]
        public void DeleteByCreatorId_ShouldDeleteArticles_WhenMatchingCreatorId()
        {
            // Arrange
            int creatorIdToDelete = 1;


            var articles = new List<Article>
            {
                new Article ("Article 1", DateTime.Now, DateTime.Now.AddDays(7), "City 1", "Category 1", "Code 1", "Store 1", "Author 1", "Content 1", 1)
               {
                    Id=1
                },
                 new Article ("Article 2", DateTime.Now, DateTime.Now.AddDays(7), "City 1", "Category 1", "Code 1", "Store 1", "Author 1", "Content 1", 2)
                  {
                    Id=2
                },
                new Article ("Article 3", DateTime.Now, DateTime.Now.AddDays(7), "City 1", "Category 1", "Code 1", "Store 1", "Author 1", "Content 1", 1)
                 {
                    Id=3
                },
                new Article ("Article 4", DateTime.Now, DateTime.Now.AddDays(7), "City 1", "Category 1", "Code 1", "Store 1", "Author 1", "Content 1", 3)
                 {
                    Id=4
                },
             };

            _articleRepositoryMock.Setup(r => r.GetAll()).Returns(articles);

            // Act
            _systemUnderTest.DeleteByCreatorId(creatorIdToDelete);

            // Assert
            _articleRepositoryMock.Verify(r => r.Delete(1), Times.Once);
            _articleRepositoryMock.Verify(r => r.Delete(3), Times.Once);
        }

        [Fact]
        public void DeleteByCreatorId_ShouldNotDeleteAnyArticles_WhenNegativeId()
        {
            // Arrange
            int negativeCreatorId = -1;

            // Act
            _systemUnderTest.DeleteByCreatorId(negativeCreatorId);

            // Assert

            _articleRepositoryMock.Verify(r => r.Delete(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public void DeleteByCreatorId_ShouldNotDeleteAnyArticles_WhenIdZero()
        {
            // Arrange
            int zeroCreatorId = 0;

            // Act
            _systemUnderTest.DeleteByCreatorId(zeroCreatorId);

            // Assert
           
            _articleRepositoryMock.Verify(r => r.Delete(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public void RemoveFavorite_ShouldRemove_WhenIDsAreValid() {

            //Arrange
    
            var articleId = 1;
            var userId = 1;
            var targetDto = new TargetDto(userId,articleId);

            var favoriteUsers = new List<User> {
                new User("test","test","test","test",false,null)
                {
                    Id=userId
                }
                };
            var article = new Article("Article 1", DateTime.Now, DateTime.Now.AddDays(7), "City 1", "Category 1", "Code 1", "Store 1", "Author 1", "Content 1", 1)
            {
                Id = 1
            };
            article.FavoriteUsers = favoriteUsers;

            _articleRepositoryMock.Setup((r) => r.GetWithLinkedEntities(articleId, "FavoriteUsers")).Returns(article);

            _userRepositoryMock.Setup((r) => r.Get(userId)).Returns(favoriteUsers[0]);

            _articleRepositoryMock.Setup(r => r.Update(article)).Returns(article);

            article.FavoriteUsers.Remove(favoriteUsers[0]);
            //Act
            var response= _systemUnderTest.RemoveFavoriteListItem(targetDto);

            //Assert

            var articleDto = new ArticleDto(
                 article.Id,
                 article.Title,
                  article.Posted,
                 article.Expiration,
                  article.City,
                 article.Category,
                  article.Code,
                  article.Store,
                  article.Author,
                  article.Content,
                  article.CreatorId
             );
            response.Should().BeEquivalentTo( articleDto );


        }

        [Fact]
        public void AddFavorite_ShouldAdd_WhenIDsAreValid()
        {
            //Arrange

            var articleId = 1;
            var userId = 1;
            var targetDto = new TargetDto(userId, articleId);

            var favoriteUsers = new List<User> {
                new User("test","test","test","test",false,null)
                {
                    Id=userId
                }
                };
            var article = new Article("Article 1", DateTime.Now, DateTime.Now.AddDays(7), "City 1", "Category 1", "Code 1", "Store 1", "Author 1", "Content 1", 1)
            {
                Id = 1
            };
            article.FavoriteUsers = favoriteUsers;

            _articleRepositoryMock.Setup((r) => r.GetWithLinkedEntities(articleId, "FavoriteUsers")).Returns(article);

            _userRepositoryMock.Setup((r) => r.Get(userId)).Returns(favoriteUsers[0]);

            _articleRepositoryMock.Setup(r => r.Update(article)).Returns(article);

            article.FavoriteUsers.Add(favoriteUsers[0]);
            //Act
            var response = _systemUnderTest.InsertFavoriteArticle(targetDto);

            //Assert

            var articleDto = new ArticleDto(
                 article.Id,
                 article.Title,
                  article.Posted,
                 article.Expiration,
                  article.City,
                 article.Category,
                  article.Code,
                  article.Store,
                  article.Author,
                  article.Content,
                  article.CreatorId
             );
            response.Should().BeEquivalentTo(articleDto);


        }


        [Fact]
        public void GetFiltered_ShouldReturnList_WhenMatchValid()
        {

            //Arrange

            string authors = "All";
            string category = "All";
            string city = "All";
            string store = "All";
            string posted= "All";
            string expiration = "All";

            var articles = new List<Article>
            {
                new Article ("Article 1", DateTime.Now, DateTime.Now.AddDays(7), "City 1", "Category 1", "Code 1", "Store 1", "Author 1", "Content 1", 1)
               {
                    Id=1
                },
                 new Article ("Article 2", DateTime.Now, DateTime.Now.AddDays(7), "City 1", "Category 1", "Code 1", "Store 1", "Author 1", "Content 1", 2)
                  {
                    Id=2
                },
                new Article ("Article 3", DateTime.Now, DateTime.Now.AddDays(7), "City 1", "Category 1", "Code 1", "Store 1", "Author 1", "Content 1", 1)
                 {
                    Id=3
                },
                new Article ("Article 4", DateTime.Now, DateTime.Now.AddDays(7), "City 1", "Category 1", "Code 1", "Store 1", "Author 1", "Content 1", 3)
                 {
                    Id=4
                },
             };

            var articleDtos = new List<ArticleDto>
            {
                new ArticleDto (1,"Article 1", articles[0].Posted,articles[0].Expiration, "City 1", "Category 1", "Code 1", "Store 1", "Author 1", "Content 1", 1)
              ,
                 new ArticleDto (2,"Article 2", articles[1].Posted,articles[1].Expiration, "City 1", "Category 1", "Code 1", "Store 1", "Author 1", "Content 1", 2)
                ,
                new ArticleDto (3,"Article 3", articles[2].Posted,articles[2].Expiration, "City 1", "Category 1", "Code 1", "Store 1", "Author 1", "Content 1", 1)
                ,
                new ArticleDto (4,"Article 4", articles[3].Posted,articles[3].Expiration, "City 1", "Category 1", "Code 1", "Store 1", "Author 1", "Content 1", 3)
               
             };

            _articleRepositoryMock.Setup((r) => r.GetAll()).Returns(articles);

            //Act
            var result = _systemUnderTest.GetFiltered(authors, category, city,
             store, posted, expiration);

            //Assert

            result.Should().BeEquivalentTo(articleDtos);
            }

    }

}