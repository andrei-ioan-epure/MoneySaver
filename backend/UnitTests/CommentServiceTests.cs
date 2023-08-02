using DomainLayer.Models;
using FluentAssertions;
using Moq;
using RepositoryLayer;
using ServiceLayer.DtoModels;
using ServiceLayer.Services;
using System.ComponentModel.Design;
using System.Runtime.Intrinsics.X86;

namespace UnitTests
{
    public class CommentServiceTests
    {
        private CommentService _systemUnderTest;
        private Mock<IRepository<User>> _userRepositoryMock;
        private Mock<IRepository<Comment>> _commentRepositoryMock;

        public CommentServiceTests() { 
        
        _commentRepositoryMock = new Mock<IRepository<Comment>>();
        _userRepositoryMock = new Mock<IRepository<User>>();
        _systemUnderTest = new CommentService(_commentRepositoryMock.Object,_userRepositoryMock.Object);

        }

        [Fact]
        public void Delete_ShouldDelete_WhenIDValid()
        {
            // Arrange
            int entityIdToDelete = 123;

            // Act
            _systemUnderTest.Delete(entityIdToDelete);

            // Assert
            _commentRepositoryMock.Verify(repo => repo.Delete(entityIdToDelete), Times.Once);
        }
        [Fact]
        public void Get_ShouldReturnNull_WhenCommentNotFound()
        {
            // Arrange
            int commentId = 123;
            _commentRepositoryMock.Setup(repo => repo.Get(commentId)).Returns((Comment)null);

            // Act
            var result = _systemUnderTest.Get(commentId);

            // Assert
            result.Should().BeNull();   
        }

        [Fact]
        public void Get_ShouldReturn_WhenCommentFound()
        {
            // Arrange
            int commentId = 123;
            var comment = new Comment("Test message", new DateTime(), "Test",1,1)
            {
                Id = commentId,  
            };
            var commentDto = new CommentDto(commentId, "Test message", new DateTime(), "Test", 1, 1);
            _commentRepositoryMock.Setup(repo => repo.Get(commentId)).Returns(comment);

            // Act
            var result = _systemUnderTest.Get(commentId);

            // Assert
           result.Should().BeEquivalentTo(commentDto);
        }

        [Fact]
        public void Get_ShouldReturnAllComments_WhenRequest()
        {
            // Arrange
            var comments = new List<Comment>
            {
              new Comment("Test message", new DateTime(), "Test",1,1)
            {
                Id = 1,
            },
             new Comment("Test message", new DateTime(), "Test",1,1)
            {
                Id = 2,
            },
            };
            var commentDtos = new List<CommentDto>
            {
              new CommentDto(1,"Test message", new DateTime(), "Test",1,1),
             new CommentDto(2,"Test message", new DateTime(), "Test",1,1),
       
            };

            _commentRepositoryMock.Setup(repo => repo.GetAll()).Returns(comments);

            // Act
            var result = _systemUnderTest.GetAll().ToList();

            // Assert

            for (int i = 0; i < commentDtos.Count; i++)
            {
                result[i].Should().BeEquivalentTo(commentDtos[i]);
            }
        }

        [Fact]
        public void Insert_ShouldSucceed_WhenInputValid()
        {
            // Arrange
            var commentDto = new CommentDto(1, "Test message", new DateTime(), "Test", 1, 1);

            var comment = new Comment("Test message", new DateTime(), "Test", 1, 1)
            {
                Id = 1,
            };

            _commentRepositoryMock.Setup(repo => repo.Insert(It.IsAny<Comment>())).Returns(comment);

            // Act
            var result = _systemUnderTest.Insert(commentDto);

            // Assert
            result.Should().BeEquivalentTo(commentDto);
        }

        [Fact]
        public void Update_ShouldSucceed_WhenInputValid()
        {
            // Arrange
            int commentId = 1;
            var commentDto = new CommentDto(commentId, "Test message", new DateTime(), "Test", 1, 1);

            var updatedComment = new Comment("Test message 2", new DateTime(), "Test2", 1, 1)
            {
                Id = 1,
            };
            var updatedCommentDto = new CommentDto(commentId, "Test message 2", new DateTime(), "Test2", 1, 1);
           
            _commentRepositoryMock.Setup(repo => repo.Update(It.IsAny<Comment>())).Returns(updatedComment);

            // Act
            var result = _systemUnderTest.Update(commentId, commentDto);

            // Assert
            result.Should().BeEquivalentTo(updatedCommentDto);

        }

        [Fact]
        public void Delete_ShouldSucceed_WhenIdValid ()
        {
            // Arrange
            int creatorId = 1;
            var comments = new List<Comment>
            {
              new Comment("Test message", new DateTime(), "Test",1,creatorId)
            {
                Id = 1,
            },
             new Comment("Test message", new DateTime(), "Test",1,creatorId)
            {
                Id = 2,
            },
                new Comment("Test message", new DateTime(), "Test",1,1)
            {
                Id = 3,
            },
            };

            _commentRepositoryMock.Setup(repo => repo.GetAll()).Returns(comments);

            // Act
            _systemUnderTest.DeleteByCreatorId(creatorId);

            // Assert
            foreach (var comment in comments)
            {
                if (comment.CreatorId == creatorId)
                {
                    _commentRepositoryMock.Verify(repo => repo.Delete(comment.Id), Times.Once);
                }
                else
                {
                    _commentRepositoryMock.Verify(repo => repo.Delete(comment.Id), Times.Never);
                }
            }
        }
        [Fact]
        public void GetCommentsFromArticle_ShoudSucceed_WhenArticleValid()
        {
            // Arrange
            int articleId = 1;
            var user1 = new User("test", "test", "test", "test", false, null)
            {
                Id = 1
            };
            var user2 = new User("test", "test", "test", "test", false, null)
            {
                Id = 2
            };


            var comments = new List<Comment>
            {
              new Comment("Test message", new DateTime(), "Test",1,articleId)
            {
                Id = 1,
                LikedBy = new List<User> { user1 },
            },
             new Comment("Test message", new DateTime(), "Test",1,articleId)
            {
                Id = 2,
                LikedBy = new List<User> { user1, user2 },
            },
            };
            var commentDtos = new List<CommentDto>
            {
              new CommentDto(1,"Test message", new DateTime(), "Test",1,articleId,1,new List<int> { user1.Id }),
             new CommentDto(2,"Test message", new DateTime(), "Test",1,articleId,2,new List<int> { user1.Id, user2.Id }),
          
            };

            _commentRepositoryMock.Setup(repo => repo.GetAllWithLinkedEntities("LikedBy")).Returns(comments);

            // Act
            var result = _systemUnderTest.GetCommentsFromArticle(articleId).ToList();

            // Assert
            result.Should().NotBeNull();
          
            result.Count.Should().Be(comments.Count);

            for (int i = 0; i < commentDtos.Count; i++)
            {
                result[i].Should().BeEquivalentTo(commentDtos[i]);
            }
        }

        [Fact]
        public void AddLikedComment_ShouldAdd_WhenIdsValid()
        {
            // Arrange
            int targetCommentId = 1;
            int userId = 10;

            var targetDto = new TargetDto(userId, targetCommentId);

            var user = new User("test", "test", "test", "test", false, null)
            {
                Id = userId
            };

            var comment =new Comment("Test message", new DateTime(), "Test", 1, 1)
            {
                Id = targetCommentId,
                LikedBy = new List<User> { },
            };
            IEnumerable<int> likedBy =new List<int> { user.Id };    
            var commentDto = new CommentDto(targetCommentId, "Test message", new DateTime(), "Test", 1, 1,1,likedBy);
  
            _commentRepositoryMock.Setup(repo => repo.GetWithLinkedEntities(targetCommentId, "LikedBy")).Returns(comment);
            _userRepositoryMock.Setup(repo => repo.Get(userId)).Returns(user);
            _commentRepositoryMock.Setup(repo => repo.Update(comment)).Returns(comment); 
           

            // Act
             var result = _systemUnderTest.AddLikedComment(targetDto);
          
            // Assert
            result.Should().NotBeNull();
            result.NumberOfLikes.Should().Be(1);
            result.Should().BeEquivalentTo(commentDto);

            
        }

        [Fact]
        public void RemoveLikedComment_ShouldRemove_WhenIdsValid()
        {
            // Arrange
            int targetCommentId = 1;
            int userId = 10;

            var targetDto = new TargetDto(userId, targetCommentId);

            var user = new User("test", "test", "test", "test", false, null)
            {
                Id = userId
            };

            var targetComment = new Comment("Test message", new DateTime(), "Test", 1, 1)
            {
                Id = targetCommentId,
                LikedBy = new List<User> { user },
            };

            var comment = new Comment("Test message", new DateTime(), "Test", 1, 1)
            {
                Id = targetCommentId,
                LikedBy = new List<User> { },
            };
            IEnumerable<int> likedBy = new List<int> { };
            var commentDto = new CommentDto(targetCommentId, "Test message", new DateTime(), "Test", 1, 1, 0, likedBy);

            _commentRepositoryMock.Setup(repo => repo.GetWithLinkedEntities(targetCommentId, "LikedBy")).Returns(comment);
            _userRepositoryMock.Setup(repo => repo.Get(userId)).Returns(user);
            _commentRepositoryMock.Setup(repo => repo.Update(comment)).Returns(comment);
           
            // Act
            var result = _systemUnderTest.RemoveLikedComment(targetDto);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(commentDto);
    
        }

    }

}