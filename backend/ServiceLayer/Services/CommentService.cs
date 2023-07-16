using DomainLayer.Models;
using RepositoryLayer;
using ServiceLayer.Contracts;
using ServiceLayer.DtoModels;

namespace ServiceLayer.Services
{
    public class CommentService : ICommentService
    {
        private readonly IRepository<Comment> _commentRepository;

        public CommentService(IRepository<Comment> commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public void Delete(int entityId)
        {
            _commentRepository.Delete(entityId);
        }

        public CommentDto? Get(int id)
        {
           var comment = _commentRepository.Get(id);
            return comment == null ? null : new CommentDto(comment.Id, comment.Message, comment.Posted,comment.CreatorId,comment.ArticleId);
        }

        public IEnumerable<CommentDto> GetAll()
        {
            return _commentRepository.GetAll().Select(u => new CommentDto(u.Id, u.Message, u.Posted, u.CreatorId, u.ArticleId));

        }

        public void Insert(CommentDto entity)
        {
            var comment = new Comment(entity.Message, entity.Posted, entity.CreatorId, entity.ArticleId);
            _commentRepository.Insert(comment);
        }

        public void Update(int id, CommentDto entity)
        {
            var comment = new Comment(entity.Message, entity.Posted, entity.CreatorId, entity.ArticleId)
            {
                Id=id
            };
            _commentRepository.Update(comment);

        }

        public void DeleteByCreatorId(int entityId)
        {
            List<int> list = new List<int>();
            foreach (var article in _commentRepository.GetAll())
            {
                if (article.CreatorId == entityId)
                {
                    list.Add(article.Id);
                }
            }
            foreach (var id in list)
            {
                _commentRepository.Delete(id);
            }
        }

    }
}

