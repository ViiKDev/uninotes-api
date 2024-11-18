using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using UniNotesAPI.Configurations;
using UniNotesAPI.Models;

namespace UniNotesAPI.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly JwtSettings _jwtSettings;
        public UsersController(ApiDbContext context, IOptions<JwtSettings> jwtSettings)
        {
            _context = context;
            _jwtSettings = jwtSettings.Value;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            return await _context.Users.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) return NotFound();
            return Ok(user);
        }
        [HttpGet("fullInfo")]
        public async Task<ActionResult<IEnumerable<User>>> GetFullInfo()
        {
            return await _context.Users.Include(u => u.Folders).Include(u => u.Documents).ToListAsync();
        }
        [HttpPost("login")]
        public async Task<ActionResult> Login(User userObj)
        {
            if (userObj == null) return BadRequest();
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == userObj.Username);
            if (user == null) return BadRequest(new { Message = "Invalid username or password!" });
            if (!VerifyPassword(userObj.Password, user.Password)) return BadRequest(new { Message = "Invalid username or password!" });
            user.Token = CreateJwt(user);
            return Ok(new { Message = "Login successful!", user.Token, user.Id });
        }
        [HttpPost("register")]
        public async Task<ActionResult> Register(User userObj)
        {
            if (userObj == null) return BadRequest();
            if (UsernameExists(userObj.Username)) return BadRequest(new { Message = "Username already in use!" });
            _context.Users.Add(userObj);
            await _context.SaveChangesAsync();
            return Ok(new { Message = "Register successful!" });
        }
        private bool UsernameExists(string? username)
        {
            return _context.Users.Any(u => u.Username == username);
        }
        private static bool VerifyPassword(string? pass, string? hashedPass)
        {
            return pass == hashedPass;
        }
        private string CreateJwt(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret));
            var identity = new ClaimsIdentity(new Claim[] { new(ClaimTypes.Name, user.Username) });
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}