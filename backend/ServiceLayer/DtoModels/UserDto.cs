namespace ServiceLayer.DtoModels
{
    public class UserDto
    {
        public UserDto(int? id, string userName, string fullName, string email, string password, bool isCreator=false)
        {
            Id = id;
            UserName = userName;
            FullName = fullName;
            Email = email;
            Password = password;
            IsCreator = isCreator;
        }

        public int? Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsCreator { get; set; }

    }
}
