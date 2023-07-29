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

        void InsertFavoriteArticle(TargetDto favoriteArticle);

        List<ArticleDto>? GetFavoriteList(int id);

        void DeleteFavoriteListItem(int userId, int articleId);
        void InsertLikedComment(TargetDto likedComment);
        void DeleteLikedCommentItem(int userId, int commentId);
        List<CommentDto>? GetLikedComment(int id);

    }
}
