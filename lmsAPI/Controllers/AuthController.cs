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
    public class AuthController : ControllerBase
    {
        private readonly DataContext context;
        private readonly IConfiguration configuration;
        public static admin admin = new admin();
        public static user user = new user();
        



        public AuthController(DataContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        [HttpPost("register-admin"), Authorize(Roles = "superadmin")]

        public async Task<ActionResult<List<admin>>> AddAdmin(registerAdmin request)
        {
            
            CreatePasswordHash(request.password, out byte[] passwordHash, out byte[] passwordSalt);
            var role = await this.context.roles.FindAsync(request.role_id);
            var dbadmin = await this.context.admin.FindAsync(request.email);
            if (dbadmin != null)
                return BadRequest("email already exists");
            admin.email = request.email;
            admin.admin_name = request.admin_name;
            admin.passwordHash = passwordHash;
            admin.passwordSalt = passwordSalt;
            admin.role_ = role;

            this.context.admin.Add(admin);
            await this.context.SaveChangesAsync();

            return Ok(await this.context.admin.ToListAsync());
        }


        [HttpPost("register-user"), Authorize(Roles = "admin,superadmin")]
        public async Task<ActionResult<List<admin>>> AddUser(registerUser request)
        {

            CreatePasswordHash(request.password, out byte[] passwordHash, out byte[] passwordSalt);
            var role = await this.context.roles.FindAsync(request.role_id);
            var job = await this.context.job_titles.FindAsync(request.jobtitle_id);
            var dbadmin = await this.context.admin.FindAsync(request.email);
            if (dbadmin != null)
                return BadRequest("email already exists");
            user.email = request.email;
            user.name = request.name;
            user.passwordHash = passwordHash;
            user.passwordSalt = passwordSalt;
            user.role_ = role;
            user.jobtitle_ = job;
            user.gender = request.gender;
            user.birthdate = request.birthdate;
            user.phone_number = request.phone_number;
            user.progress = 0;

            this.context.user.Add(user);
            await this.context.SaveChangesAsync();

            return Ok(await this.context.user.ToListAsync());
        }

        [HttpPost("login-admin")]
        public async Task<ActionResult<string>> LoginAdmin(login request)
        {
            if (!request.email.Contains("@"))
            {
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Format email salah"
                });
            }
            var dbadmin = await this.context.admin.FindAsync(request.email);

            if (dbadmin == null)
            {
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Email salah"
                });
            }

             if (!VerifyPasswordHash(request.password, dbadmin.passwordHash, dbadmin.passwordSalt))
            {
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Password salah"
                });
            }
            string expiresIn = "31536000";
            string token = CreateTokenAdmin(dbadmin);
            return Ok(new
            {
                token = token,
                expiresIn = expiresIn,
                email = request.email
            });
        }

        [HttpPost("login-super-admin")]
        public async Task<ActionResult<string>> LoginSuperAdmin(login request)
        {
            if (!request.email.Contains("@"))
            {
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Format email salah"
                });
            }
            var dbadmin = await this.context.admin.FindAsync(request.email);

            if (dbadmin == null)
            {
                return BadRequest(new Response { Status = "error",
                                                 ErrorCode = "400",
                                                 ErrorMessage = "Email salah" } );
            }

            if (!VerifyPasswordHash(request.password, dbadmin.passwordHash, dbadmin.passwordSalt))
            {
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Password salah"
                });
            }
            string expiresIn = "31536000";
            string token = CreateTokenSuperAdmin(dbadmin);
            return Ok(new
            {
                token = token,
                expiresIn = expiresIn,
                email = request.email
            });
        }

        [HttpPost("login-user")]
        public async Task<ActionResult<string>> LoginUser(login request)
        {
            if (!request.email.Contains("@"))
            {
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Format email salah"
                });
            }
            var dbuser = await this.context.user.FindAsync(request.email);

            if (dbuser == null)
            {
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Email salah"
                });
            }

            if (!VerifyPasswordHash(request.password, dbuser.passwordHash, dbuser.passwordSalt))
            {
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Password salah"
                });
            }
            
            string expiresIn = "31536000";
            string token =  CreateTokenUser(dbuser);
            return Ok( new { token = token,
                             expiresIn = expiresIn,
                             email = request.email});
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
                expires: DateTime.Now.AddDays(365),
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
                expires: DateTime.Now.AddDays(365),
                signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
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
                expires: DateTime.Now.AddDays(365),
                signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}