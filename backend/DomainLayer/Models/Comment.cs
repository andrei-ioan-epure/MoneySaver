namespace DomainLayer.Models
{
    public class Comment:BaseEntity
    {
        public Comment(string message, DateTime posted,int creatorId, int articleId)
        {
            Message = message;
            Posted = posted;
            CreatorId = creatorId;
            ArticleId = articleId;
        }

        public string Message { get; set; }
        public DateTime Posted { get; set; }

        public User Creator { get; set; }

        public int CreatorId { get; set; }

        public Article Article { get; set; }

        public int ArticleId { get; set; }

        public ICollection<User> LikedBy { get; set; }


    }
}
