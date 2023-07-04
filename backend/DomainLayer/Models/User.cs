namespace DomainLayer.Models
{
    public class User : BaseEntity
    {
        public User(string userName, string fullName)
        {
            UserName = userName;
            FullName = fullName;
        }

        public string UserName { get; set; }
        public string FullName { get; set; }
    }
}
