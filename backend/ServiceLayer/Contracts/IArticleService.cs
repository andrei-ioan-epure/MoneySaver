using ServiceLayer.DtoModels;

namespace ServiceLayer.Contracts
{
    public interface IArticleService
    {

        IEnumerable<ArticleDto> GetAll();

        ArticleDto? Get(int id);

        void Delete(int entityId);

        void Insert(ArticleDto entity);

        void Update(int id, ArticleDto entity);

        void DeleteByCreatorId(int entityId);

        IEnumerable<ArticleDto> GetFiltered(string authors, string category, string city, string store, string posted, string expiration);

        IEnumerable<ArticleDto> GetFilteredFavorite(int id, string authors, string category, string city, string store, string posted, string expiration);
     
        ArticleDto RemoveFavoriteListItem(TargetDto favoriteArticle);

        ArticleDto InsertFavoriteArticle(TargetDto favoriteArticle);
    }
}