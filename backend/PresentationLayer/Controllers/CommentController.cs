using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Contracts;
using ServiceLayer.DtoModels;

namespace PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("get")]
        public IActionResult GetAll()
        {
            return Ok(_commentService.GetAll());
        }

        [HttpGet("get/{id}")]
        public IActionResult Get(int id)
        {
            var comment = _commentService.Get(id);
            return comment != null ? Ok(comment) : NotFound("Comment not found.");
        }

        [HttpPost("add")]
        public IActionResult Add(CommentDto comment)
        {
            _commentService.Insert(comment);
            return Ok();
        }

        [HttpPut("update/{id}")]
        public IActionResult Update(int id, CommentDto comment)
        {
            _commentService.Update(id, comment);
            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            _commentService.Delete(id);
            return Ok();
        }

        [HttpGet("getCommentsFromArticle/{id}")]
        public IActionResult GetCommsArticle(int id)
        {
            var list = _commentService.GetCommentsFromArticle(id);
            return Ok(list);
        }
    }
}
