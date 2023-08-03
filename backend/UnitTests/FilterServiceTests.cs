using DomainLayer.Models;
using FluentAssertions;
using Moq;
using RepositoryLayer;
using ServiceLayer.Services;

namespace UnitTests
{
    public class FilterServiceTests
    {
        private Mock<IRepository<User>> _userRepository;
        private FilterService _systemUnderTest;

        public FilterServiceTests() {
            _userRepository = new Mock<IRepository<User>>();
            _systemUnderTest = new FilterService(_userRepository.Object);
        }

        [Fact]
        public void ParsePostedDate_ShouldReturnNull_WhenDateNotValid() {

            //Arange
            string posted = "last-20h";
            //Act
            var result=_systemUnderTest.ParsePostedDate(posted);
            //Assert
            result.Should().BeNull();
        }

         [Fact]
         public void ParseExpirationDate_ShouldReturnNull_WhenDateNotValid()
         {
             //Arange
             string posted = "in-4-months";
             //Act
             var result = _systemUnderTest.ParseExpirationDate(posted);
             //Assert
             result.Should().BeNull();
         }

        
        [Fact]
        public void AuthorMatch_ShouldMatch_WhenAuthorIsValid() {

            //Arange
            var article = new Article("test", new DateTime(), new DateTime(), "test", "test", "test", "test", "author1", "test", 1)
            {
                Id = 1,
            };
            string authors = "author1,author2";

            var user = new User("test", "test", "test", "test", false, null)
            {
                Id=1
            };

            _userRepository.Setup((r) => r.Get(article.Id)).Returns(user);

            //Act

            var result=_systemUnderTest.IsAuthorMatch(article, authors);

            //Assert

            result.Should().BeTrue();
        }

        [Fact]
        public void AuthorMatch_ShouldBeNull_WhenAuthorNotValid()
        {
            //Arange
            var article = new Article("test", new DateTime(), new DateTime(), "test", "test", "test", "test", "test", "test", 1)
            {
                Id = 1,
            };
            string authors = "author1,author2";

            _userRepository.Setup((r) => r.Get(article.Id)).Returns((User)null);

            //Act
            var result = _systemUnderTest.IsAuthorMatch(article, authors);

            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void AuthorMatch_ShouldNotMatch_WhenAuthorNotValid()
        {
            //Arange
            var article = new Article("test", new DateTime(), new DateTime(), "test", "test", "test", "test", "test", "test", 1)
            {
                Id = 1,
            };
            string authors = "author1,author2";

            var user = new User("test", "test", "test", "test", false, null)
            {
                Id = 1
            };

            _userRepository.Setup((r) => r.Get(article.Id)).Returns(user);

            //Act
            var result = _systemUnderTest.IsAuthorMatch(article, authors);

            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void CategoryMatch_ShouldMatch_WhenCategoryIsValid()
        {
            //Arange
            var article = new Article("test", new DateTime(), new DateTime(), "test", "category", "test", "test", "test", "test", 1)
            {
                Id = 1,
            };
            string category = "category";
            
            //Act
            var result=_systemUnderTest.IsCategoryMatch(article, category);

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void CategoryMatch_ShouldNotMatch_WhenCategoryNotValid()
        {
            //Arange
            var article = new Article("test", new DateTime(), new DateTime(), "test", "category", "test", "test", "test", "test", 1)
            {
                Id = 1,
            };
            string category = "test";

            //Act
            var result = _systemUnderTest.IsCategoryMatch(article, category);

            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void CategoryMatch_ShouldMatchAll_WhenCategoryIsValid()
        {
            //Arange
            var article = new Article("test", new DateTime(), new DateTime(), "test", "test", "test", "test", "test", "test", 1)
            {
                Id = 1,
            };
            string category = "all";
            //Act
            var result = _systemUnderTest.IsCategoryMatch(article, category);
            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void CityMatch_ShouldMatch_WhenCityIsValid()
        {
            //Arange
            var article = new Article("test", new DateTime(), new DateTime(), "city", "test", "test", "test", "test", "test", 1)
            {
                Id = 1,
            };
            string city = "city";
            //Act

            var result = _systemUnderTest.IsCityMatch(article, city);
            //Assert

            result.Should().BeTrue();
        }

        [Fact]
        public void CityMatch_ShouldNotMatch_WhenCityNotValid()
        {
            //Arange
            var article = new Article("test", new DateTime(), new DateTime(), "test", "test", "test", "test", "test", "test", 1)
            {
                Id = 1,
            };
            string city = "city";
            //Act

            var result = _systemUnderTest.IsCityMatch(article, city);
            //Assert

            result.Should().BeFalse();
        }

        [Fact]
        public void CityMatch_ShouldMatchAll_WhenCityIsValid()
        {
            //Arange
            var article = new Article("test", new DateTime(), new DateTime(), "test", "test", "test", "city", "test", "test", 1)
            {
                Id = 1,
            };
            string city = "all";
            //Act
            var result = _systemUnderTest.IsCityMatch(article, city);
            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void StoreMatch_ShouldMatch_WhenStoreIsValid()
        {
            //Arange
            var article = new Article("test", new DateTime(), new DateTime(), "test", "test", "test", "store", "test", "test", 1)
            {
                Id = 1,
            };
            string store = "store";
            //Act
            var result = _systemUnderTest.IsStoreMatch(article, store);
            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void StoreMatch_ShouldNotMatch_WhenStoreNotValid()
        {
            //Arange
            var article = new Article("test", new DateTime(), new DateTime(), "test", "test", "test", "test", "test", "test", 1)
            {
                Id = 1,
            };
            string store = "store";
            //Act
            var result = _systemUnderTest.IsStoreMatch(article, store);
            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void StoreMatch_ShouldMatchAll_WhenStoreIsValid()
        {
            //Arange
            var article = new Article("test", new DateTime(), new DateTime(), "test", "test", "test", "Stradivarius", "test", "test", 1)
            {
                Id = 1,
            };
            string store = "all";
            //Act
            var result = _systemUnderTest.IsStoreMatch(article, store);
            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void PostedMatch_ShouldMatch_WhenDateValid()
        {
            //Arange
            var article = new Article("test", new DateTime(2023,4,15), new DateTime(), "test", "test", "test", "test", "test", "test", 1)
            {
                Id = 1,
            };
            string posted = null;
            //Act
            var result = _systemUnderTest.IsPostedMatch(article, posted);
            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void PostedMatch_ShouldMatch_WhenDateValid1()
        {
            //Arange
            var article = new Article("test", new DateTime(2023, 8, 7), new DateTime(), "test", "test", "test", "test", "test", "test", 1)
            {
                Id = 1,
            };
            string posted = "last-3-days";
            //Act
            var result = _systemUnderTest.IsPostedMatch(article, posted);
            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void ExpirationMatch_ShouldMatch_WhenDateNull()
        {
            //Arange
            var article = new Article("test", new DateTime(), new DateTime(2023, 9, 1), "test", "test", "test", "test", "test", "test", 1)
            {
                Id = 1,
            };
            string posted = null;
            //Act
            var result = _systemUnderTest.IsExpirationMatch(article, posted);
            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void ExpirationMatch_ShouldMatch_WhenDateValid1Month()
        {
            //Arange
            var article = new Article("test", new DateTime(), new DateTime(2023,9,3), "test", "test", "test", "test", "test", "test", 1)
            {
                Id = 1,
            };
            string posted = "in-1-month";
            //Act
            var result = _systemUnderTest.IsExpirationMatch(article, posted);
            //Assert
            result.Should().BeTrue();
        }

    }
}
