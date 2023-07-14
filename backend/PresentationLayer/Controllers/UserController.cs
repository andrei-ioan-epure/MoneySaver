using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Contracts;
using ServiceLayer.DtoModels;

namespace PresentationLayer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
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
            _userService.Delete(id);
            return Ok();
        }
    }
}