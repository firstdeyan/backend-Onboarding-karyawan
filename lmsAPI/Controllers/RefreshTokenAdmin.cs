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
    public class RefreshTokenAdmin : ControllerBase
    {
        private readonly DataContext context;
        private readonly IConfiguration configuration;
        public static admin admin = new admin();
        public static user user = new user();

        public RefreshTokenAdmin(DataContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        [HttpPost]
        public async Task<ActionResult<string>> refreshTokenAdmin(refreshToken request)
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
            var dbadmin = await this.context.admin.FindAsync(request.email);
            if (dbadmin == null)
            {
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Email tidak terdaftar"
                });
            }
            var active = true;
            var dbadminActive = await this.context.admin.Where(a => a.active == active).FirstOrDefaultAsync(e => e.email == request.email); ;
            if (dbadminActive == null)
            {
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Email tidak active"
                });
            }

            string expiresIn = "10";
            string token = CreateTokenAdmin(dbadmin);
            string role = "admin";
            string expiresIns = "10";
            string tokens = CreateTokenSuperAdmin(dbadmin);
            string roles = "superadmin";
            var roleid = 2;
            var dbadminrole = await this.context.admin.Include(r => r.role_).Where(g => g.role_id == roleid).FirstOrDefaultAsync(e => e.email == request.email);
            if (dbadminrole == null)
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
        private string CreateTokenAdmin(admin admin)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, admin.email),
                new Claim(ClaimTypes.Role, "admin")
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

        private string CreateTokenSuperAdmin(admin admin)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, admin.email),
                new Claim(ClaimTypes.Role, "superadmin")
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
