
using ServiceLayer.DtoModels;

namespace ServiceLayer.Contracts
{
    public interface ICommentService
    {
        IEnumerable<CommentDto> GetAll();

        CommentDto? Get(int id);

        void Delete(int entityId);

        void Insert(CommentDto entity);

        void Update(int id, CommentDto entity);

        void DeleteByCreatorId(int entityId);

        IEnumerable<CommentDto> GetCommentsFromArticle(int articleID);
    }
}
