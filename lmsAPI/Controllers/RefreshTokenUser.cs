using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace lmsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class RefreshTokenUser : ControllerBase
    {
        private readonly DataContext context;
        private readonly IConfiguration configuration;
        public static admin admin = new admin();
        public static user user = new user();

        public RefreshTokenUser(DataContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        [HttpPost]
        public async Task<ActionResult<string>> refreshTokenUser(refreshToken request)
        {
            if (request.old_token == null)
            {
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Token tidak dimiliki"
                });
            }
            var dbuser = await this.context.user.FindAsync(request.email);
            if (dbuser == null)
            {
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Email tidak terdaftar"
                });
            }
            var active = true;
            var dbuserActive = await this.context.user.Where(a => a.active == active).FirstOrDefaultAsync(e => e.email == request.email); ;
            if (dbuserActive == null)
            {
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Email tidak active"
                });
            }

            string expiresIn = "10";
            string token = CreateTokenUser(dbuser);
            string role = "user";
            string expiresIns = "10";
            string tokens = CreateTokenMentor(dbuser);
            string roles = "mentor";
            var roleid = 4;
            var dbuserrole = await this.context.user.Include(r => r.role_).Where(g => g.role_id == roleid).FirstOrDefaultAsync(e => e.email == request.email);
            if (dbuserrole == null)
            {
                return Ok(new
                {
                    token = tokens,
                    expiresIn = expiresIns,
                    role = roles,
                    email = request.email
                });
            }
            else
            {
                return Ok(new
                {
                    token = token,
                    expiresIn = expiresIn,
                    role = role,
                    email = request.email
                });
            }
        }

        private string CreateTokenUser(user user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.email),
                new Claim(ClaimTypes.Role, "user")
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                this.configuration.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddSeconds(10),
                signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private string CreateTokenMentor(user user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.email),
                new Claim(ClaimTypes.Role, "mentor")
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                this.configuration.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddSeconds(10),
                signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
