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
        private readonly IRepository<Comment> _commentRepository;

        public UserService(IRepository<User> userRepository, IRepository<Article> articleRepository, IRepository<Comment> commentRepository)
        {
            _userRepository = userRepository;
            _articleRepository = articleRepository;
            _commentRepository = commentRepository;
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

        public void InsertFavoriteArticle(TargetDto favoriteArticle)
        {
            var article = _articleRepository.Get(favoriteArticle.targetId);
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



        public void InsertLikedComment(TargetDto likedComment)
        {
            var comment = _commentRepository.Get(likedComment.targetId);
            var user = _userRepository.GetWithLinkedEntities(likedComment.userId, "LikedComments");

            user.LikedComments.Add(comment);
            _userRepository.Update(user);
        }


        public void DeleteLikedCommentItem(int userId, int commentId)
        {
            var user = _userRepository.GetWithLinkedEntities(userId, "LikedComments");
            var comment = _commentRepository.Get(commentId);
            if (user != null && comment != null)
            {
                user.LikedComments.Remove(comment);
                _userRepository.Update(user);


            }

        }

        public List<CommentDto>? GetLikedComment(int id)
        {
            var user = _userRepository.GetWithLinkedEntities(id, "LikedComments");

            if (user != null)
            {
                List<CommentDto> liked = new List<CommentDto>();
                foreach (var comment in user.LikedComments)
                {
                    liked.Add(new CommentDto(comment.Id,comment.Message,comment.Posted,comment.CreatorId,comment.ArticleId));
                }
                return liked;
            }
            return null;
        }

    }
}
