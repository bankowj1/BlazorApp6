using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BlazorApp6.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize(Roles = "Admin")]
    public class UserController : ControllerBase
    {
        private readonly appdbContext _context;

        public UserController(appdbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            if(users != null)
                return Ok(users);
            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return BadRequest("404");
                return Ok(user);

        }

    }
}