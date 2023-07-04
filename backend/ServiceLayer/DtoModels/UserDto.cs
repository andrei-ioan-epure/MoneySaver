namespace ServiceLayer.DtoModels
{
    public class UserDto
    {
        public UserDto(int? id, string userName, string fullName)
        {
            Id = id;
            UserName = userName;
            FullName = fullName;
        }

        public int? Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
    }
}
