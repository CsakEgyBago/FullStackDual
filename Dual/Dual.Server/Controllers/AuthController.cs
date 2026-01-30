using Dual.Server.Dtos.Auth;
using Dual.Server.Dtos.Options;
using Eszi.Demo.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Dual.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IOptions<JwtOptions> options;
        private readonly CoreDbContext coreDbContext;

        public AuthController(IOptions<JwtOptions>options, CoreDbContext coreDbContext)
        {
            this.options = options;
            this.coreDbContext = coreDbContext;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public ActionResult Login(LoginRequest request)
        {
            var user = coreDbContext.Users.SingleOrDefault(u => u.Email == request.Email && u.Password == request.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            var accessToken = GenerateJwtToken(request.Email);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTime.UtcNow.AddMinutes(options.Value.ExpiresInMinutes),
                SameSite = SameSiteMode.Lax
            };

            HttpContext.Response.Cookies.Append(Constans.AccessTokenCookieKey,accessToken, cookieOptions);

            return Ok(accessToken);
        }

        [Authorize]
        [HttpGet("cookietoken")]
        public async Task<ActionResult<string>> GetTokenFromCookie()
        {
            Request.Cookies.TryGetValue(Constans.AccessTokenCookieKey, out string? accessToken);

            if (string.IsNullOrEmpty(accessToken))
            {
                return Unauthorized();
            }
            return Ok(accessToken);
        }

        [Authorize]
        [HttpPost("Logout")]
        public ActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete(Constans.AccessTokenCookieKey);

            return Ok();
        }

        private string GenerateJwtToken(string email)
        {
            var jwt = options.Value;

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, email),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, "User"),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwt.Issuer,
                audience: jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(jwt.ExpiresInMinutes),
                signingCredentials: creds );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
