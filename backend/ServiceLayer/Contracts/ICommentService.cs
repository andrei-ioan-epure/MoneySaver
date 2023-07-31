
using ServiceLayer.DtoModels;

namespace ServiceLayer.Contracts
{
    public interface ICommentService
    {
        IEnumerable<CommentDto> GetAll();

        CommentDto? Get(int id);

        void Delete(int entityId);

        CommentDto Insert(CommentDto entity);

        CommentDto Update(int id, CommentDto entity);

        void DeleteByCreatorId(int entityId);

        IEnumerable<CommentDto> GetCommentsFromArticle(int articleID);

        CommentDto AddLikedComment(TargetDto likedComment);
        void RemoveLikedComment(int userId, int commentId);


    }
}
