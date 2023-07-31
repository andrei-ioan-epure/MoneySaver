using DomainLayer.Models;
using ServiceLayer.DtoModels;

namespace ServiceLayer.Contracts
{
    public interface IUserService
    {
        IEnumerable<UserDto> GetAll();

        UserDto? Get(int id);

        void Delete(int entityId);

        void Insert(UserDto entity);

        void Update(int id, UserDto entity);


        TokenDto? Login(UserLoginDto entity);

        bool IsCreator(int id);

        void InsertFavoriteArticle(TargetDto favoriteArticle);

        List<ArticleDto>? GetFavoriteList(int id);

        void DeleteFavoriteListItem(int userId, int articleId);
  

    }
}
