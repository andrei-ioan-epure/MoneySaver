using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Contracts;
using ServiceLayer.DtoModels;
using ServiceLayer.Services;

namespace PresentationLayer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IArticleService _articleService;
        private readonly ICommentService _commentService;

        public UserController(IUserService userService, IArticleService articleService,ICommentService commentService)
        {
            _userService = userService;
            _articleService = articleService;
            _commentService = commentService;
        }

        [HttpGet("get")]
        public IActionResult GetAll()
        {
            return Ok(_userService.GetAll());
        }

        [HttpGet("get/{id}")]
        public IActionResult Get(int id)
        {
            var user = _userService.Get(id);
            return user != null ? Ok(user) : NotFound("User not found.");
        }

        [HttpPost("add")]
        public IActionResult Add(UserDto user)
        {
            _userService.Insert(user);
            return Ok();
        }

        [HttpPut("update/{id}")]
        public IActionResult Update(int id, UserDto user)
        {
            _userService.Update(id, user);
            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            _commentService.DeleteByCreatorId(id);
            _articleService.DeleteByCreatorId(id);
            _userService.Delete(id);
            return Ok();
        }

        [HttpPut("addFavorite")]
        public IActionResult AddFavorites(FavoriteArticleDto favoriteArticle)
        {
            _userService.InsertFavoriteArticle(favoriteArticle);
            return Ok();
        }
        [HttpGet("getFavorite")]
        public IActionResult GetFavorites([FromQuery]  int id)
        {
            return Ok(_userService.GetFavoriteList(id));
        }
        [HttpDelete("deleteFavorite")]
        public IActionResult DeleteFavoriteItem([FromQuery] int userId, [FromQuery] int articleId)
        {
            _userService.DeleteFavoriteListItem(userId, articleId);
            return Ok();
        }
    }
}