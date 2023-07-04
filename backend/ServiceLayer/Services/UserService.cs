using DomainLayer.Models;
using RepositoryLayer;
using ServiceLayer.Contracts;
using ServiceLayer.DtoModels;

namespace ServiceLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<UserDto> GetAll()
        {
            return _userRepository.GetAll().Select(u => new UserDto(u.Id, u.UserName, u.FullName));
        }

        public UserDto? Get(int id)
        {
            var user = _userRepository.Get(id);

            return user != null ? new UserDto(user.Id, user.UserName, user.FullName): null;
        }

        public void Delete(int entityId)
        {
            _userRepository.Delete(entityId);
        }

        public void Insert(UserDto entity)
        {
            var user = new User(entity.UserName, entity.FullName);
            _userRepository.Insert(user);
        }

        public void Update(int id, UserDto entity)
        {
            var user = new User(entity.UserName, entity.FullName)
            {
                Id = id
            };
            _userRepository.Update(user);
        }
    }
}
