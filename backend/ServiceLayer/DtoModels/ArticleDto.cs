using DomainLayer.Models;

namespace ServiceLayer.DtoModels
{
    public  class ArticleDto
    {
        public ArticleDto(int? id,string title, DateTime posted, DateTime expiration, string city, string category, string code, string store,string author, string content,int creatorId)
        {
            Id = id;
            Title = title;
            Posted = posted;
            Expiration = expiration;
            City = city;
            Category = category;
            Code = code;
            Store = store;
            Author = author;
            Content = content;
            CreatorId = creatorId;
        }

        public int? Id { get; set; }
        public string Title { get; set; }
        public DateTime Posted { get; set; }
        public string City { get; set; }
        public DateTime Expiration { get; set; }
        public string Category { get; set; }
        public string Code { get; set; }
        public string Store { get; set; }
        
        public string Author { get; set; }
        public string Content { get; set; }
        public int CreatorId { get; set; }

    }
}
