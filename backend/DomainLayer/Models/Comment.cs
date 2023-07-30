namespace DomainLayer.Models
{
    public class Comment:BaseEntity
    {
        public Comment(string message, DateTime posted, string creatorName, int creatorId, int articleId)
        {
            Message = message;
            Posted = posted;
            CreatorName = creatorName;
            CreatorId = creatorId;
            ArticleId = articleId;
        }

        public string Message { get; set; }

        public DateTime Posted { get; set; }

        public string CreatorName { get; set; }

        public User Creator { get; set; }

        public int CreatorId { get; set; }

        public Article Article { get; set; }

        public int ArticleId { get; set; }

        public ICollection<User> LikedBy { get; set; }


    }
}
