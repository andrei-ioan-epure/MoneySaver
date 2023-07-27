using DomainLayer.Models;
using RepositoryLayer;
using ServiceLayer.Contracts;
using ServiceLayer.DtoModels;

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

        public void DeleteFavoriteListItem(int userId,int articleId)
        {
            var user = _userRepository.GetWithLinkedEntities(userId, "FavoriteArticles");
            var article = _articleRepository.Get(articleId);
            if (user != null && article !=null)
            {
               user.FavoriteArticles.Remove(article);
               _userRepository.Update(user);


            }

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
    }
}
