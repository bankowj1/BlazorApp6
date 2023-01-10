using Microsoft.AspNetCore.Mvc;

namespace BlazorApp6.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public List<User> Users { get; set; }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            return Ok(Users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<User>>> GetUser(int id)
        {
            var user = Users.FirstOrDefault(h => h.Iduser == id);
            if (user == null) return BadRequest("404");
            return Ok(user);
        }
    }
}