using DomainLayer.Context;
using DomainLayer.Models;
using RepositoryLayer;
using ServiceLayer.Contracts;
using ServiceLayer.DtoModels;

namespace ServiceLayer.Services
{
    public class ArticleService : IArticleService
    {

        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Article> _articleRepository;
        private readonly IRepository<Comment> _commentRepository;
        private readonly IFilterService _filterService;

        public ArticleService(IRepository<Article> articleRepository, IFilterService filterService, IRepository<User> userRepository, IRepository<Comment> commentRepository)
        {
            _articleRepository = articleRepository;
            _filterService = filterService;
            _userRepository = userRepository;
            _commentRepository = commentRepository;
        }
        public void Delete(int entityId)
        {
            _articleRepository.Delete(entityId);
        }

        public ArticleDto? Get(int id)
        {
            var article = _articleRepository.Get(id);
            return article == null ? null :
                new ArticleDto(article.Id, article.Title, article.Posted, article.Expiration, article.City, article.Category, article.Code, article.Store, article.Author, article.Content, article.CreatorId);
        }

        public IEnumerable<ArticleDto> GetAll()
        {
            return _articleRepository.GetAll().Select(article => new ArticleDto(article.Id, article.Title, article.Posted, article.Expiration, article.City, article.Category, article.Code, article.Store, article.Author, article.Content, article.CreatorId));

        }

        public void Insert(ArticleDto entity)
        {
            var article = new Article(entity.Title, entity.Posted, entity.Expiration, entity.City, entity.Category, entity.Code, entity.Store, entity.Author, entity.Content, entity.CreatorId);
            _articleRepository.Insert(article);
        }

        public void Update(int id, ArticleDto entity)
        {
            var article = new Article(entity.Title, entity.Posted, entity.Expiration, entity.City, entity.Category, entity.Code, entity.Store, entity.Author, entity.Content, entity.CreatorId)
            {
                Id = id
            };
            _articleRepository.Update(article);
        }


        public void DeleteByCreatorId(int entityId)
        {
            List<int> list = new List<int>();
            foreach (var article in _articleRepository.GetAll())
            {
                if (article.CreatorId == entityId)
                {
                    list.Add(article.Id);
                }
            }
            foreach (var id in list)
            {
                _articleRepository.Delete(id);
            }
        }

        public IEnumerable<ArticleDto> GetFiltered(string authors, string category, string city, string store, string posted, string expiration)
        {

            List<Article> articles = _articleRepository.GetAll().ToList();

            List<ArticleDto> filteredArticles = new List<ArticleDto>();
            foreach (var article in articles)
            {
         
                var author = article.Author;

                if (_filterService.IsAuthorMatch(article, authors) &&
                    _filterService.IsCategoryMatch(article, category) &&
                    _filterService.IsCityMatch(article, city) &&
                    _filterService.IsStoreMatch(article, store) &&
                    _filterService.IsPostedMatch(article, posted) &&
                    _filterService.IsExpirationMatch(article, expiration))
                {
                    var articleDto = new ArticleDto(article.Id, article.Title, article.Posted, article.Expiration,
                        article.City, article.Category, article.Code, article.Store, author, article.Content, article.CreatorId);

                    filteredArticles.Add(articleDto);
              
                }
            }
            return filteredArticles;
        }


        public void InsertLikedComment(TargetDto likedComment)
        {
            var comment = _commentRepository.Get(likedComment.targetId);
            var user = _userRepository.GetWithLinkedEntities(likedComment.userId, "LikedComments");

            user.LikedComments.Add(comment);
            _userRepository.Update(user);
        }

        public ArticleDto RemoveFavoriteListItem(TargetDto favoriteArticle)
        {
             var article = _articleRepository.GetWithLinkedEntities(favoriteArticle.targetId, "FavoriteUsers"); 
            var user = _userRepository.Get(favoriteArticle.userId);
            article.FavoriteUsers.Remove(user);
            var responseArticle = _articleRepository.Update(article);

            return new ArticleDto(responseArticle.Id, responseArticle.Title, responseArticle.Posted, responseArticle.Expiration,
               responseArticle.City, responseArticle.Category, responseArticle.Code, responseArticle.Store,
               responseArticle.Author, responseArticle.Content, responseArticle.CreatorId);
        }

        public ArticleDto InsertFavoriteArticle(TargetDto favoriteArticle)
        {
            var article = _articleRepository.GetWithLinkedEntities(favoriteArticle.targetId, "FavoriteUsers"); 
            var user = _userRepository.Get(favoriteArticle.userId);

            article.FavoriteUsers.Add(user);
            var responseArticle = _articleRepository.Update(article);
            return new ArticleDto(responseArticle.Id,responseArticle.Title,responseArticle.Posted,responseArticle.Expiration,
                responseArticle.City,responseArticle.Category,responseArticle.Code,responseArticle.Store,
                responseArticle.Author,responseArticle.Content,responseArticle.CreatorId);

        }
    }
}
