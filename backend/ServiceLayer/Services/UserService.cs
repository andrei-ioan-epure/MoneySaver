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

        private const int keySize = 32;
        private const int iterations = 7;
        private HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA256;
        private byte[] passwordSalt;
        private JwtUtils jwtUtils = new JwtUtils();


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
            var hashedPassword = CreatePasswordHash(entity.Password, out passwordSalt);

            var user = new User(entity.UserName, entity.FullName,entity.Email,hashedPassword,entity.IsCreator, passwordSalt);
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
            if (user != null && VerifyPasswordHash(userDto.Password,user.Password,user.Salt))
            {
                token = new TokenDto(jwtUtils.CreateToken(user), user.Id, user.IsCreator? "admin" : "user");
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

    }
}
