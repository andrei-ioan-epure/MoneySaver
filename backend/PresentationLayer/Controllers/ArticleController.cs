using DomainLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Contracts;
using ServiceLayer.DtoModels;
using ServiceLayer.Services;

namespace PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet("get")]
        [Authorize("admin")]
        public IActionResult GetAll()
        {
            //var currentUser = HttpContext.User;
            //var currentUser1 = HttpContext.Items["User"];
            //if (currentUser.HasClaim(c => c.Type == "role"))
            //{
            //    if (currentUser.Claims.FirstOrDefault(c => c.Type == "role").Value == "Admin")
                    return Ok(_articleService.GetAll());
            //}
            //return NotFound();
        }

        [HttpGet("get/{id}")]
        public IActionResult Get(int id)
        {
            var article = _articleService.Get(id);
            return article != null ? Ok(article) : NotFound("Article not found.");
        }

        [HttpPost("add")]
        //[Authorize]
        public IActionResult Add(ArticleDto article)
        {
            //var currentUser = HttpContext.User;
            //if(currentUser.HasClaim(c => c.Type == "role"))
            //{
                //if(currentUser.Claims.FirstOrDefault(c=> c.Type == "role").Value == "Admin")
                    _articleService.Insert(article);
                    return Ok();
            //}
            //return NotFound();
            
        }

        [HttpPut("update/{id}")]
        public IActionResult Update(int id, ArticleDto article)
        {
            _articleService.Update(id, article);
            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            _articleService.Delete(id);
            return Ok();
        }
        [HttpGet("getFiltered")]
        public IActionResult GetFiltered([FromQuery] string authors, [FromQuery] string category, [FromQuery] string city,
            [FromQuery] string store, [FromQuery] string posted, [FromQuery] string expiration)
        {
          
            return Ok(_articleService.GetFiltered(authors, category, city, store, posted, expiration));
        }
    }
}
