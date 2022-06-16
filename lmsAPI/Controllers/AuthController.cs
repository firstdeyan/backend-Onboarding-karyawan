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

        public async Task<ActionResult<List<admin>>> AddAdmin([FromForm]registerAdmin request)
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
            CreatePasswordHash(request.password, out byte[] passwordHash, out byte[] passwordSalt);
            var role = await this.context.roles.FindAsync(request.role_id);
            var job = await this.context.job_titles.FindAsync(request.jobtitle_id);
            var dbadmin = await this.context.admin.FindAsync(request.email);
            if (dbadmin != null)
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Email telah digunakan"
                });
            if (request.files != null && request.files.Count() > 0)
            {
                foreach (var file in request.files)
                {
                    if (file != null)
                    {
                        string Filename = DateTime.Now.ToString("yyyyMMddHHmmss") + file.FileName;
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "../lmsAPI/File", Filename);
                        var stream = new FileStream(path, FileMode.Create);
                        file.CopyToAsync(stream);
                        if (request.files == null)
                        {
                            string url = "api/ShowImage/userimage.png";
                            admin.photo = url;
                        }
                        else
                        {
                            string url = "api/ShowImage/" + Filename;
                            admin.photo = url;
                        }

                    }
                }
            }
            admin.email = request.email;
            admin.admin_name = request.admin_name;
            admin.passwordHash = passwordHash;
            admin.passwordSalt = passwordSalt;
            admin.role_ = role;
            admin.jobtitle_ = job;
            admin.gender = request.gender;
            admin.birthdate = request.birthdate;
            admin.phone_number = request.phone_number;
            admin.active = true;

            this.context.admin.Add(admin);
            await this.context.SaveChangesAsync();

            return Ok(await this.context.admin.Include(e => e.role_).Include(f => f.jobtitle_).ToListAsync());
        }


        [HttpPost("register-user"), Authorize(Roles = "admin,superadmin")]
        public async Task<ActionResult<List<user>>> AddUser([FromForm] registerUser request)
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
            CreatePasswordHash(request.password, out byte[] passwordHash, out byte[] passwordSalt);
            var role = await this.context.roles.FindAsync(request.role_id);
            var job = await this.context.job_titles.FindAsync(request.jobtitle_id);
            var dbuser = await this.context.user.FindAsync(request.email);
            if (dbuser != null)
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Email telah digunakan"
                });
            if (request.files != null && request.files.Count() > 0)
            {
                foreach (var file in request.files)
                {
                    if (file != null)
                    {
                        string Filename = DateTime.Now.ToString("yyyyMMddHHmmss") + file.FileName;
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "../lmsAPI/File", Filename);
                        var stream = new FileStream(path, FileMode.Create);
                        file.CopyToAsync(stream);
                        if (request.files == null)
                        {
                            string url = "api/ShowImage/userimage.png";
                            user.photo = url;
                        }
                        else
                        {
                            string url = "api/ShowImage/" + Filename;
                            user.photo = url;
                        }

                    }
                }
            }
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
            user.finishedActivities = 0;
            user.assignedActivities = 0;
            user.active = true;

            this.context.user.Add(user);
            await this.context.SaveChangesAsync();

            return Ok(await this.context.user.Include(e => e.role_).Include(f => f.jobtitle_).ToListAsync());
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
                    ErrorMessage = "Email tidak terdaftar"
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
            string expiresIn = "10";
            string token = CreateTokenAdmin(dbadmin);
            string role = "admin";
            return Ok(new
            {
                token = token,
                expiresIn = expiresIn,
                role = role,
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
                                                 ErrorMessage = "Email tidak terdaftar" } );
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
            string expiresIn = "10";
            string token = CreateTokenSuperAdmin(dbadmin);
            string role = "superadmin";
            return Ok(new
            {
                token = token,
                expiresIn = expiresIn,
                role = role,
                email = request.email
            });
        }

        [HttpPost("loginAdmin")]
        public async Task<ActionResult<string>> LoginAdminCombine(login request)
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

            if (!VerifyPasswordHash(request.password, dbadmin.passwordHash, dbadmin.passwordSalt))
            {
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Password salah"
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
            var roleid = 4;
            var dbuser = await this.context.user.Include(r => r.role_).Where(g => g.role_id == roleid).FirstOrDefaultAsync(e => e.email == request.email);
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
            var dbuseractive = await this.context.user.Include(r => r.role_).Where(a => a.active == active).Where(g => g.role_id == roleid).FirstOrDefaultAsync(e => e.email == request.email);
            if (dbuseractive == null)
            {
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Email tidak active"
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
            
            string expiresIn = "10";
            string token =  CreateTokenUser(dbuser);
            string role = "user";
            return Ok( new { token = token,
                             expiresIn = expiresIn,
                             role = role,
                             email = request.email});
        }

        [HttpPost("login-mentor")]
        public async Task<ActionResult<string>> LoginMentor(login request)
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
            var roleid = 3;
            var dbuser = await this.context.user.Include(r => r.role_).Where(g => g.role_id == roleid).FirstOrDefaultAsync(e => e.email == request.email);

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
            var dbuseractive = await this.context.user.Include(r => r.role_).Where(a => a.active == active).Where(g => g.role_id == roleid).FirstOrDefaultAsync(e => e.email == request.email);
            if (dbuseractive == null)
            {
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Email tidak active"
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

            string expiresIn = "10";
            string token = CreateTokenMentor(dbuser);
            string role = "mentor";
            return Ok(new
            {
                token = token,
                expiresIn = expiresIn,
                role = role,
                email = request.email
            });
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