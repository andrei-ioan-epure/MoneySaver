using Microsoft.EntityFrameworkCore;
using System.Net.Mail;

namespace DomainLayer.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class User : BaseEntity
    {
        public User(string userName, string fullName,string email,string password,bool isCreator, byte[] salt)
        {
            UserName = userName;
            FullName = fullName;
            Email = email;
            Password = password;    
            IsCreator = isCreator;
            Salt = salt;
        }

        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsCreator { get; set; }
        public byte[] Salt { get; set; }

        public ICollection<Article> CreatedArticles { get; set; }
        public ICollection<Comment> Comments { get; set; }

        public ICollection<Article> FavoriteArticles { get; set; }
        public ICollection<Comment> LikedComments { get; set; }



    }
}
