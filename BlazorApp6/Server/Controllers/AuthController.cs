using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Konscious.Security.Cryptography;
using System.Security.Cryptography;
using System.Text;
using BlazorApp6.Server.Models;
using Microsoft.Extensions.Options;

namespace BlazorApp6.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly appdbContext _context;
        private readonly AuthSettings _appIdentitySettings;
        User user = new User();

        public AuthController(appdbContext context, IOptions<AuthSettings> appAuthSettingsAccessor)
        {
            _context = context;
            _appIdentitySettings = appAuthSettingsAccessor.Value;
        }
        [HttpPost("Register")]
        public async Task<ActionResult<User>> Register(RegUserDTO regUserDTO)
        {
            
            user.Username = regUserDTO.Username;
            
            user.Email = CreatePasswordHash(regUserDTO.Email); 

            user.Userlogin = CreatePasswordHash(regUserDTO.Userlogin);

            user.Pass = CreatePasswordHash(regUserDTO.Pass);
            return Ok(user);
        }
        private byte[] CreatePasswordHash(string password)
        {
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password));
            
            argon2.DegreeOfParallelism = _appIdentitySettings.Passinfo.DegreeOfParallelism;
            argon2.MemorySize = _appIdentitySettings.Passinfo.MemorySize;
            argon2.Iterations = _appIdentitySettings.Passinfo.Iterations;
            var salt = CreateSalt();
            argon2.Salt = salt;
            byte[] passwo = new byte[_appIdentitySettings.Passinfo.PassLen];
            byte[] pass = new byte[_appIdentitySettings.Passinfo.PassLen+_appIdentitySettings.Passinfo.SaltLen];
            passwo = argon2.GetBytes(_appIdentitySettings.Passinfo.PassLen);
            try
            {
                
                Buffer.BlockCopy(salt, 0, pass, 0, salt.Length);
                Buffer.BlockCopy(passwo, 0, pass, salt.Length, passwo.Length);
            }
            catch (ArgumentException ex)
            {
                // Handle the exception
                Console.WriteLine(ex.Message);
            }

            return pass;
        }
        private byte[] CreateSalt()
        {
            byte[] salt = new byte[_appIdentitySettings.Passinfo.SaltLen];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }
        [HttpPost("login")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult<string>> Login(LogUserDTO logUserDTO)
        {
            if(user.Userlogin != logUserDTO.Userlogin) { return BadRequest("MIA"); }
            return Ok(user.Username);
        }
    }
}
