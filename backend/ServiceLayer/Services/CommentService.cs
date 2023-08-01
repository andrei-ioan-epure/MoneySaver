using DomainLayer.Models;
using RepositoryLayer;
using ServiceLayer.Contracts;
using ServiceLayer.DtoModels;

namespace ServiceLayer.Services
{
    public class CommentService : ICommentService
    {
        private readonly IRepository<Comment> _commentRepository;
        private readonly IRepository<User> _userRepository;

        public CommentService(IRepository<Comment> commentRepository, IRepository<User> userRepository)
        {
            _commentRepository = commentRepository;
            _userRepository = userRepository;
        }

        public void Delete(int entityId)
        {
            _commentRepository.Delete(entityId);
        }

        public CommentDto? Get(int id)
        {
            var comment = _commentRepository.Get(id);
            return comment == null ? null : new CommentDto(comment.Id, comment.Message, comment.Posted, comment.CreatorName, comment.CreatorId, comment.ArticleId);
        }

        public IEnumerable<CommentDto> GetAll()
        {
            return _commentRepository.GetAll().Select(u => new CommentDto(u.Id, u.Message, u.Posted, u.CreatorName, u.CreatorId, u.ArticleId));

        }

        public CommentDto Insert(CommentDto entity)
        {
            var comment = new Comment(entity.Message, entity.Posted, entity.CreatorName, entity.CreatorId, entity.ArticleId);
            var responseComment = _commentRepository.Insert(comment);
            return new CommentDto(responseComment.Id, responseComment.Message, responseComment.Posted, responseComment.CreatorName, responseComment.CreatorId, responseComment.ArticleId);
        }

        public CommentDto Update(int id, CommentDto entity)
        {
            var comment = new Comment(entity.Message, entity.Posted, entity.CreatorName, entity.CreatorId, entity.ArticleId)
            {
                Id = id
            };
            var responseComment = _commentRepository.Update(comment);
            return new CommentDto(responseComment.Id, responseComment.Message, responseComment.Posted, responseComment.CreatorName, responseComment.CreatorId, responseComment.ArticleId,
                entity.NumberOfLikes, entity.LikedByUsers);

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

        public IEnumerable<CommentDto> GetCommentsFromArticle(int articleID)
        {
            var comments = _commentRepository.GetAllWithLinkedEntities("LikedBy").Where(c => c.ArticleId == articleID);
            List<CommentDto> list = new List<CommentDto>();
            foreach (var comment in comments)
            {

                list.Add(new CommentDto(comment.Id, comment.Message, comment.Posted, comment.CreatorName, comment.CreatorId, comment.ArticleId, comment.LikedBy.Count(),comment.LikedBy.Select((s)=> s.Id)));

            }
            return list;
        }

        public CommentDto AddLikedComment(TargetDto likedComment)
        {
            var comment = _commentRepository.GetWithLinkedEntities(likedComment.targetId, "LikedBy"); 
            var user = _userRepository.Get(likedComment.userId);

            comment.LikedBy.Add(user);
            var responseComment=_commentRepository.Update(comment);

            return new CommentDto(responseComment.Id, responseComment.Message, responseComment.Posted, responseComment.CreatorName, responseComment.CreatorId, responseComment.ArticleId,responseComment.LikedBy.Count(), responseComment.LikedBy.Select((u)=>u.Id));

        }


        public void RemoveLikedComment(int userId, int commentId)
        {
            var user = _userRepository.GetWithLinkedEntities(userId, "LikedComments");
            var comment = _commentRepository.Get(commentId);
            if (user != null && comment != null)
            {
                user.LikedComments.Remove(comment);
                _userRepository.Update(user);


            }

        }

    }
}

