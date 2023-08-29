using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DtoModels
{
    public class CommentDto
    {
        public CommentDto(int? id, string message, DateTime posted, string creatorName, int creatorId, int articleId, int numberOfLikes = 0, IEnumerable<int> likedByUsers = null)
        {
            Id = id;
            Message = message;
            Posted = posted;
            CreatorName = creatorName;
            ArticleId = articleId;
            CreatorId = creatorId;
            NumberOfLikes = numberOfLikes;
            LikedByUsers = likedByUsers;
        }

        public int? Id { get; set; }
        public string Message { get; set; }
        public DateTime Posted { get; set; }
        public int CreatorId { get; set; }
        public int ArticleId { get; set; }
        public string CreatorName { get; set; }

        public int NumberOfLikes { get; set; }

        public IEnumerable<int> LikedByUsers { get; set; }
    }
}
