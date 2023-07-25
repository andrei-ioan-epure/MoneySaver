using DomainLayer.Models;
using RepositoryLayer;
using ServiceLayer.Contracts;
using ServiceLayer.DtoModels;
using System.Globalization;

namespace ServiceLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Article> _articleRepository;
 

        public UserService(IRepository<User> userRepository, IRepository<Article> articleRepository)
        {
            _userRepository = userRepository;
            _articleRepository = articleRepository;
        }

        public IEnumerable<UserDto> GetAll()
        {
            return _userRepository.GetAll().Select(u => new UserDto(u.Id, u.UserName, u.FullName,u.Email,u.Password,u.IsCreator));
        }

        public UserDto? Get(int id)
        { 
            var user = _userRepository.GetWithLinkedEntities(id, "FavoriteArticles");


            return user != null ? new UserDto(user.Id, user.UserName, user.FullName, user.Email, user.Password, user.IsCreator) : null;
        }

        public void Delete(int entityId)
        {
            _userRepository.Delete(entityId);
        }

        public void Insert(UserDto entity)
        {
            var user = new User(entity.UserName, entity.FullName,entity.Email,entity.Password,entity.IsCreator);
            _userRepository.Insert(user);
        }

        public void InsertFavoriteArticle(FavoriteArticleDto favoriteArticle)
        {
            var article = _articleRepository.Get(favoriteArticle.articleId);
            var user = _userRepository.GetWithLinkedEntities(favoriteArticle.userId, "FavoriteArticles");

            user.FavoriteArticles.Add(article);
            _userRepository.Update(user);
        }

        public void Update(int id, UserDto entity)
        {
            var user = new User(entity.UserName, entity.FullName, entity.Email, entity.Password, entity.IsCreator)
            {
                Id = id
            };
            _userRepository.Update(user);
        }
        public UserLoginDto? GetUserByEmail(string email)
        {
            var userList = _userRepository.GetAll();
            foreach (var user in userList)
            {
                if (user.Email == email)
                {
                    return new UserLoginDto(user.Email, user.Password);
                }
            }
            return null;
        }
        public int? GetUserIdByEmail(string email)
        {
            var userList = _userRepository.GetAll();
            foreach (var user in userList)
            {
                if (user.Email == email)
                {
                    return user.Id; // Assuming the user object has an "Id" property that represents the user's unique identifier.
                }
            }
            return null;
        }

        


        public int? Login(UserLoginDto user)
        {
            var existingUser = GetUserByEmail(user.Email);
            var id = GetUserIdByEmail(user.Email);
            if(existingUser!=null)
            {
                //if (VerifyPass(user.Password, existingUser.Password)) ;
                return id;
            }
            return null;
        }
        
    }
}
