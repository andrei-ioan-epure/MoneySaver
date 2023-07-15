using DomainLayer.Models;
using RepositoryLayer;
using ServiceLayer.Contracts;
using ServiceLayer.DtoModels;

namespace ServiceLayer.Services
{
    public class ArticleService : IArticleService
    {

        private readonly IRepository<Article> _articleRepository;

        public ArticleService(IRepository<Article> articleRepository)
        {
            _articleRepository = articleRepository;
        }
        public void Delete(int entityId)
        {
           _articleRepository.Delete(entityId);
        }

        public ArticleDto? Get(int id)
        {
           var article = _articleRepository.Get(id);
            return article == null ? null :
                new ArticleDto(article.Id,article.Title,article.Expiration,article.Expiration,article.City,article.Category,article.Code,article.Store,article.Author,article.Content,article.CreatorId);
        }

        public IEnumerable<ArticleDto> GetAll()
        {
            return _articleRepository.GetAll().Select(article => new ArticleDto(article.Id, article.Title, article.Expiration, article.Expiration, article.City, article.Category, article.Code, article.Store, article.Author, article.Content, article.CreatorId));

        }

        public void Insert(ArticleDto entity)
        {
            var article = new Article(entity.Title, entity.Expiration, entity.Expiration, entity.City, entity.Category, entity.Code, entity.Store, entity.Author, entity.Content,entity.CreatorId);
            _articleRepository.Insert(article);
        }

        public void Update(int id, ArticleDto entity)
        {
            var article = new Article(entity.Title, entity.Expiration, entity.Expiration, entity.City, entity.Category, entity.Code, entity.Store, entity.Author, entity.Content, entity.CreatorId)
            {
                Id = id
            };
            _articleRepository.Update(article);
        }
    }
}
