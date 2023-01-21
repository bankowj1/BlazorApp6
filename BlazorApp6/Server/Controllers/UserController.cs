using BlazorApp6.Server.Models;
using BlazorApp6.Server.Services.UserService;
using BlazorApp6.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BlazorApp6.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly appdbContext _context;
        private readonly IUserService _userService;

        public UserController(appdbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            if(users != null)
                return Ok(users);
            return BadRequest();
        }

        [HttpGet("{id}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return BadRequest("404");
                return Ok(user);

        }
        [HttpGet("me/"), Authorize]
        public async Task<ActionResult<User>> GetMyUser()
        {
            var user = await _context.Users.Where(c => c.Iduser == _userService.GetMyId())
                .ToListAsync();
            if (user == null) return BadRequest("404");
            return Ok(user);

        }
    }
}