
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateApi.Context;
using RealEstateApi.Models;
using RealEstateApi.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using RealEstateApi.Models.Dto;


namespace RealEstateApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private object jwtTokenHandler;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] User userObj)
        {
            if (userObj == null)
                return BadRequest(new { Message = "Invalid user object" });

            // Fetch the user with the provided email from the database
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == userObj.Email);

            if (user == null)
                return NotFound(new { Message = "User Not Found" });

            // Verify the provided password against the stored hashed password
            if (!PasswordHasher.VerifyPassword(userObj.Password, user.Password))
                return Unauthorized(new { Message = "Invalid Password" });

            user.Token = CreateJwt(user);
            var newAccessToken = user.Token;
            var newRefreshToken = CreateRefreshToken();
            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(5);
            await _context.SaveChangesAsync();

            return Ok(new TokenApiDto()
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost("register")]

        public async Task<IActionResult> RegisterUser([FromBody] User userObj)
        {
            if (userObj == null)
                return BadRequest();

            if(await CheckEmailExistAsync(userObj.Email))
                return BadRequest(new { Message = "Email Already Exist!"});


            userObj.Password = PasswordHasher.HashPassword(userObj.Password);
            userObj.Role = "User";
            userObj.Token = "";
            await _context.Users.AddAsync(userObj);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                Message = "User Registered!"
            });
        }

        private Task<bool> CheckEmailExistAsync(string email)
            => _context.Users.AnyAsync(x => x.Email == email);

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        private string CreateJwt(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("veryverysupersecretkeythatneedstobelongenough");

            var identity = new ClaimsIdentity(new Claim[]
            {
        new Claim(ClaimTypes.Role, user.Role),
        new Claim(ClaimTypes.Name, user.Name),
            });

            var credential = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddSeconds(10),
                SigningCredentials = credential
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }

        private string CreateRefreshToken() 
        {
            var tokenBytes = RandomNumberGenerator.GetBytes(64);
            var refreshToken = Convert.ToBase64String(tokenBytes);

            var tokenInUser = _context.Users
                .Any(a=>a.RefreshToken == refreshToken);
            if (tokenInUser)
            { 
                return CreateRefreshToken(); 
            }
            return refreshToken;
        }
        private ClaimsPrincipal GetPrincipalFromExpireToken(string token)
        {
            var key = Encoding.ASCII.GetBytes("veryverysupersecretkeythatneedstobelongenough");
            var tokenValidationParameters = new TokenValidationParameters


            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateLifetime = false,
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("This is in valid Token");
            {
                return principal;
            }
        }
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(TokenApiDto tokenApiDto)
        {
            if (tokenApiDto == null)
                return BadRequest("Invalid Client Request");
            string accessToken = tokenApiDto.AccessToken;
            string refreshToken = tokenApiDto.RefreshToken;
            var principal = GetPrincipalFromExpireToken(accessToken);
            var name = principal.Identity.Name;
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Name == name);
            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                return BadRequest("Invalid Request");
            var newAccessToken = CreateJwt(user);
            var newRefreshToken = CreateRefreshToken();
            user.RefreshToken = newRefreshToken;
            await _context.SaveChangesAsync();
            return Ok(new TokenApiDto()
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
            });
        }
        

    }
}
