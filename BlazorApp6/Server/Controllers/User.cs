using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp6.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class User : ControllerBase
    {
       /* [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(users);
        }*/
    }
}
