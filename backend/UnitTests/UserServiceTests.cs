using System;
using DomainLayer.Models;
using FluentAssertions;
using Moq;
using RepositoryLayer;
using ServiceLayer.Contracts;
using ServiceLayer.DtoModels;
using ServiceLayer.Services;

namespace UnitTests
{
	public class UserServiceTests
	{
		private UserService _systemUnderTest;
		private Mock<IRepository<User>> _userRepositoryMock;

		public UserServiceTests()
		{
			_userRepositoryMock = new Mock<IRepository<User>>();
			_systemUnderTest = new UserService(_userRepositoryMock.Object);
		}

		[Fact]
		public void GetAll_ShouldReturnAllUsers()
		{
			// Arrange
			var users = new List<User>
			{
				new User("test", "test", "test", "test", false, null)
				{
					Id=1
				},
				new User("test1", "test1", "test1", "test1", true, null)
				{
					Id=2
				}
			};

			var usersDto = new List<UserDto>
			{
				new UserDto(1,"test","test","test","test",false),
                new UserDto(2,"test1","test1","test1","test1",true)
            };

			_userRepositoryMock.Setup(r => r.GetAll()).Returns(users);

            //Act
            var result = _systemUnderTest.GetAll();

			//Assert
			result.Should().BeEquivalentTo(usersDto);

			_userRepositoryMock.Verify(r => r.GetAll(), Times.Once);
		}

		[Fact]
		public void Get_ShouldReturnUser_WhenIdIsValid()
		{
            //Arrange
            var id = 1;

            var user = new User("test", "test", "test", "test", false, null)
			{
				Id = id
			};

			var userDto = new UserDto(1, "test", "test", "test", "test", false);

			_userRepositoryMock.Setup(r => r.GetWithLinkedEntities(id, "FavoriteArticles")).Returns(user);

			//Act
			var result = _systemUnderTest.Get(id);

			//Assert
			result.Should().BeEquivalentTo(userDto);

            _userRepositoryMock.Verify(r => r.GetWithLinkedEntities(id, "FavoriteArticles"), Times.Once);
        }

        [Fact]
        public void Get_ShouldReturnUser_WhenIdIsNotValid()
        {
            //Arrange
            var id = 10;

            _userRepositoryMock.Setup(r => r.GetWithLinkedEntities(id, "FavoriteArticles")).Returns((User)null);

            //Act
            var result = _systemUnderTest.Get(id);

            //Assert
            result.Should().BeNull();

            _userRepositoryMock.Verify(r => r.GetWithLinkedEntities(id, "FavoriteArticles"), Times.Once);
        }

		[Fact]
		public void GetFavoriteList_ShouldReturnNull_WhenUserIsNull()
		{
			//Arrange
			_userRepositoryMock.Setup(r => r.GetWithLinkedEntities(1, "FavoriteArticles")).Returns((User)null);

			//Act
			var result = _systemUnderTest.GetFavoriteList(1);

			//Assert
			result.Should().BeNull();

            _userRepositoryMock.Verify(r => r.GetWithLinkedEntities(1, "FavoriteArticles"), Times.Once);
        }

		[Fact]
        public void GetFavoriteList_ShouldReturnList_WhenUserIsValid()
		{
			//Arrange
			var user = new User("test", "test", "test", "test", true, null)
			{
				Id = 1
			};

			user.FavoriteArticles = new List<Article>
			{
				new Article("test",new DateTime(), new DateTime(), "test", "test", "test", "test", "test", "test", 1)
				{
					Id= 1
				},
				new Article("test",new DateTime(), new DateTime(), "test", "test", "test", "test", "test", "test", 2)
				{
					Id= 2
				}
			};

			var favorites = new List<ArticleDto>
			{
				new ArticleDto(1,"test",new DateTime(), new DateTime(), "test", "test", "test", "test", "test", "test", 1),
				new ArticleDto(2,"test",new DateTime(), new DateTime(), "test", "test", "test", "test", "test", "test", 2)
			};

			_userRepositoryMock.Setup(r => r.GetWithLinkedEntities(1, "FavoriteArticles")).Returns(user);

			//Act

			var response = _systemUnderTest.GetFavoriteList(1);

			//Assert
			response.Should().BeEquivalentTo(favorites);
		}

		[Fact]
		public void Update_ShouldUpdateUser_WhenIdIsValid()
		{
			//Arrange
			var user = new User("test", "test", "test", "test", true, null)
			{
				Id = 1
			};

			var userDto = new UserDto(1, "test", "test", "test", "test", true);

			byte[] salt = null;
			

			_userRepositoryMock.Setup(r => r.Get(1)).Returns(user);
            salt=user.Salt;
            _userRepositoryMock.Setup(r => r.Update(It.IsAny<User>())).Returns(user);
			
			//Act

			_systemUnderTest.Update(1, userDto);

			//Assert
			_userRepositoryMock.Verify(r => r.Get(1), Times.Once);
            _userRepositoryMock.Verify(r => r.Update(It.IsAny<User>()), Times.Once);
        }

		[Fact]
		public void Delete_ShouldSucced_WhenIdValid()
		{
			//Arange
			var id = 1;

			_userRepositoryMock.Setup(r => r.Delete(id));

			//Act
			_systemUnderTest.Delete(id);

            //Assert
            _userRepositoryMock.Verify(r => r.Delete(id), Times.Once);
        }

		[Fact]
		public void Insert_ShouldInsertUser_WhenTransmitedUserDto()
		{
			//Arrage
			var userDto = new UserDto(1, "test", "test", "test", "test", true);

			_userRepositoryMock.Setup(r => r.Insert(It.IsAny<User>()));

			//Act
			_systemUnderTest.Insert(userDto);

			//Assert
			_userRepositoryMock.Verify(r => r.Insert(It.IsAny<User>()), Times.Once);

		}

		//[Fact]
		//public void Login_ShouldReturnToken_WhenCredentialsValid()
		//{
		//	//Arrange
		//	var userLoginDto = new UserLoginDto("jale@gmail.com","parolatare");

		//	var token = new TokenDto("testToken", 1, "admin", "test");

  //          var user = new User("test","test","test","test",true, null)
  //          {
  //              Id = 1,
  //          };

  //          var mockJwtUtils = new Mock<IJwtUtils>();
  //          mockJwtUtils.Setup(j => j.CreateToken(It.IsAny<User>())).Returns("testToken");

  //          //Act
  //          var response = _systemUnderTest.Login(userLoginDto);

		//	//Assert
		//	Assert.NotNull(response);
  //          Assert.Equal("testToken", response.Token);
  //      }

        [Fact]
        public void Login_ShouldReturnNull_WhenCredentialsInvalid()
        {
            //Arrange
            var userLoginDto = new UserLoginDto("test", "test");

            //Act
            var response = _systemUnderTest.Login(userLoginDto);

            //Assert
            Assert.Null(response);
        }

		[Fact]
		public void IsCreator_ShouldReturnTrue_WhenIdIsCreator()
		{
			//Arrange
            var user = new User("test", "test", "test", "test", true, null)
            {
                Id = 1,
            };

			_userRepositoryMock.Setup(r => r.Get(It.IsAny<int>())).Returns(user);

			//Act
			var response = _systemUnderTest.IsCreator(1);

			//Assert
			response.Should().BeTrue();
        }

        [Fact]
        public void IsCreator_ShouldReturnFalse_WhenIdIsCreator()
        {
            //Arrange
            var user = new User("test", "test", "test", "test", false, null)
            {
                Id = 1,
            };

            _userRepositoryMock.Setup(r => r.Get(It.IsAny<int>())).Returns(user);

            //Act
            var response = _systemUnderTest.IsCreator(1);

            //Assert
            response.Should().BeFalse();
        }
    }
}

