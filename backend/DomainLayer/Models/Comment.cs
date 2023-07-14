﻿namespace DomainLayer.Models
{
    public class Comment:BaseEntity
    {
        public Comment(string message, DateTime posted)
        {
            Message = message;
            Posted = posted;
        }

        public string Message { get; set; }
        public DateTime Posted { get; set; }

        public User Creator { get; set; }

        public int CreatorId { get; set; }

        public ICollection<User> LikedBy { get; set; }


    }
}
