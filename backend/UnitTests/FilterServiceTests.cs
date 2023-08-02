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
    }
}
