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
        public CommentDto(int? id, string message, DateTime posted, int creatorId, int articleId)
        {
            Id = id;
            Message = message;
            Posted = posted;
            ArticleId = articleId;
            CreatorId = creatorId;
     
        }

        public int? Id { get; set; }
        public string Message { get; set; }
        public DateTime Posted { get; set; }
        public int CreatorId { get; set; }
        public int ArticleId { get; set; }


    }
}
