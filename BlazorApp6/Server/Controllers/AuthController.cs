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

namespace BlazorApp6.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly appdbContext _context;
        private readonly AuthSettings _appIdentitySettings;
        public static User user = new ();

        public AuthController(appdbContext context, IOptions<AuthSettings> appAuthSettingsAccessor)
        {
            _context = context;
            _appIdentitySettings = appAuthSettingsAccessor.Value;
        }
        [HttpPost("Register")]
        public async Task<ActionResult<bool>> Register(RegUserDTO regUserDTO)
        {
            
            user.Username = regUserDTO.Username;
            
            user.Email = CreatePasswordHash(regUserDTO.Email); 

            user.Userlogin = CreatePasswordHash(regUserDTO.Userlogin);

            user.Pass = CreatePasswordHash(regUserDTO.Pass);

            return Ok(true);
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
            if (user.Iduser > 5 && user.Iduser < 10)
            {
                role = "admin";
            }
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.Username),
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
            if(!VerifyPasswordHash(logUserDTO.Userlogin,user.Userlogin)) { return BadRequest(""); }
            return CreateToken(user);
        }
    }
}
