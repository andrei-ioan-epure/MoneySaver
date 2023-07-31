using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer;
using ServiceLayer.Contracts;
using ServiceLayer.DtoModels;

namespace ServiceLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Article> _articleRepository;
        private readonly IRepository<Comment> _commentRepository;

        private const int keySize = 32;
        private const int iterations = 7;
        private HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA256;
        private byte[] passwordSalt;
        private JwtUtils jwtUtils = new JwtUtils();

        public UserService(IRepository<User> userRepository, IRepository<Article> articleRepository, IRepository<Comment> commentRepository)
        {
            _userRepository = userRepository;
            _articleRepository = articleRepository;
            _commentRepository = commentRepository;
        }

        public IEnumerable<UserDto> GetAll()
        {
            return _userRepository.GetAll().Select(u => new UserDto(u.Id, u.UserName, u.FullName, u.Email, u.Password, u.IsCreator));
        }

        public UserDto? Get(int id)
        {
            var user = _userRepository.GetWithLinkedEntities(id, "FavoriteArticles");


            return user != null ? new UserDto(user.Id, user.UserName, user.FullName, user.Email, user.Password, user.IsCreator) : null;
        }

        public List<ArticleDto>? GetFavoriteList(int id)
        {
            var user = _userRepository.GetWithLinkedEntities(id, "FavoriteArticles");

            if (user != null)
            {
                List<ArticleDto> favorites = new List<ArticleDto>();
                foreach (var favorite in user.FavoriteArticles)
                {
                    favorites.Add(new ArticleDto(favorite.Id, favorite.Title, favorite.Posted, favorite.Expiration, favorite.City, favorite.Category,
                        favorite.Code, favorite.Store, favorite.Author, favorite.Content, favorite.CreatorId));
                }
                return favorites;
            }
            return null;
        }

        public void DeleteFavoriteListItem(int userId, int articleId)
        {
            var user = _userRepository.GetWithLinkedEntities(userId, "FavoriteArticles");
            var article = _articleRepository.Get(articleId);
            if (user != null && article != null)
            {
                user.FavoriteArticles.Remove(article);
                _userRepository.Update(user);


            }

        }

        public void Delete(int entityId)
        {
            _userRepository.Delete(entityId);
        }

        private string CreatePasswordHash(string password, out byte[] passwordSalt)
        {
            passwordSalt = RandomNumberGenerator.GetBytes(keySize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                passwordSalt,
                iterations,
                hashAlgorithm,
                keySize);

            return Convert.ToHexString(hash);
        }

        private bool VerifyPasswordHash(string password, string hash, byte[] salt)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm,
                keySize);

            var goodHash = Convert.FromHexString(hash);

            return CryptographicOperations.FixedTimeEquals(hashToCompare, goodHash);
        }

        public void Insert(UserDto entity)
        {

            var hashedPassword = CreatePasswordHash(entity.Password, out passwordSalt);

            var user = new User(entity.UserName, entity.FullName, entity.Email, hashedPassword, entity.IsCreator, passwordSalt);



            _userRepository.Insert(user);
        }

        public void InsertFavoriteArticle(TargetDto favoriteArticle)
        {
            var article = _articleRepository.Get(favoriteArticle.targetId);
            var user = _userRepository.GetWithLinkedEntities(favoriteArticle.userId, "FavoriteArticles");

            user.FavoriteArticles.Add(article);
            _userRepository.Update(user);
        }

        public void Update(int id, UserDto entity)
        {
            var salt = _userRepository.Get(id).Salt;
            var user = new User(entity.UserName, entity.FullName, entity.Email, entity.Password, entity.IsCreator, salt)
            {
                Id = id
            };
            _userRepository.Update(user);
        }

        public TokenDto? Login(UserLoginDto userDto)
        {
            TokenDto token = null;
            var user = GetUserByEmail(userDto.Email);
            if (user != null && VerifyPasswordHash(userDto.Password, user.Password, user.Salt))
            {
                token = new TokenDto(jwtUtils.CreateToken(user), user.Id, user.IsCreator ? "admin" : "user", user.FullName);
            }
            return token;
        }

        public bool IsCreator(int id)
        {
            var user = _userRepository.Get(id);
            return user.IsCreator;
        }

        private User GetUserByEmail(string email)
        {
            var users = _userRepository.GetAll();
            foreach (var user in users)
            {
                if (user.Email == email)
                {
                    return user;
                }
            }
            return null;
        }


    }
}
