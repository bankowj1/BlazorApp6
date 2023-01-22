using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Konscious.Security.Cryptography;
using System.Security.Cryptography;
using System.Text;
using BlazorApp6.Server.Models;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using BlazorApp6.Shared.Models;
using Ganss.Xss;
using Microsoft.AspNetCore.Authorization;
using BlazorApp6.Server.Services.UserService;

namespace BlazorApp6.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly appdbContext _context;
        private readonly AuthSettings _appIdentitySettings;
        private readonly IUserService _userService;
        public static User user = new ();

        public AuthController(appdbContext context, IOptions<AuthSettings> appAuthSettingsAccessor, IUserService userService)
        {
            _context = context;
            _appIdentitySettings = appAuthSettingsAccessor.Value;
            _userService = userService;
        }
        [HttpGet("id"), Authorize]
        public ActionResult<int> GetMe()
        {
            var userName = _userService.GetMyId();
            return Ok(userName);
        }
        [HttpGet("role"), Authorize(Roles ="admin")]
        public ActionResult<string> GetRole()
        {
            var role = User?.FindFirstValue(ClaimTypes.Role);
            return Ok(role);
        }
        [HttpPost("Register")]
        public async Task<ActionResult<string>> Register(RegUserDTO regUserDTO)
        {
            regUserDTO.SanitizeInput();
            User lognUser = await _context.Users
                        .Where(u => u.Username == regUserDTO.Username)
                        .FirstOrDefaultAsync();
            if(lognUser != null)
            {
                return BadRequest("username in use");
            }
            if(regUserDTO.Pass.Length < 8)
            {
                return BadRequest("to easy password");
            }
            foreach (string line in System.IO.File.ReadLines("CommonPasses.txt"))
            {
                if(regUserDTO.Pass.Contains(line))
                {
                    return BadRequest("to easy password");
                }
            }

            user.Username = regUserDTO.Username;
            
            user.Email = Encoding.UTF8.GetBytes(regUserDTO.Email); 

            user.Userlogin = Encoding.UTF8.GetBytes(regUserDTO.Userlogin);

            user.Pass = CreatePasswordHash(regUserDTO.Pass);

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return Ok("");
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
        private bool VerifyPasswordHash(string password, byte[] pass)
        {
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password));

            argon2.DegreeOfParallelism = _appIdentitySettings.Passinfo.DegreeOfParallelism;
            argon2.MemorySize = _appIdentitySettings.Passinfo.MemorySize;
            argon2.Iterations = _appIdentitySettings.Passinfo.Iterations;
            argon2.Salt = pass[0.._appIdentitySettings.Passinfo.SaltLen];

            return pass[(_appIdentitySettings.Passinfo.SaltLen)..].SequenceEqual(argon2.GetBytes(_appIdentitySettings.Passinfo.PassLen));
        }
        private string CreateToken(User user)
        {
            string role = "user";
            if (user.Iduser > 5 && user.Iduser < 10 || user.Iduser == 16)
            {
                role = "admin";
            }
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,""+user.Iduser),
                new Claim(ClaimTypes.Role, role )
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_appIdentitySettings.Jwtoken.Token));
            var cred = new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(claims: claims, expires: DateTime.UtcNow.AddDays(2),signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        [HttpGet("TestLog")]
        public async Task<ActionResult<User>> TestLog()
        {
            return Ok(user);
        }
        [HttpPost("Login")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult<string>> Login(LogUserDTO logUserDTO)
        {
            logUserDTO.SanitizeInput();
            User lognUser = await _context.Users
                        .Where(u => u.Userlogin == Encoding.UTF8.GetBytes(logUserDTO.Userlogin))
                        .FirstOrDefaultAsync();
            Thread.Sleep(1000);
            if (lognUser == null)
                return BadRequest("");
            if (!VerifyPasswordHash(logUserDTO.Pass, lognUser.Pass)) { return BadRequest(""); }
            return Ok(CreateToken(lognUser));
        }
    }
}
