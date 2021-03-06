using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace lmsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin,superadmin")]
    [EnableCors]
    public class AdminController : ControllerBase
    {
        private readonly DataContext context;

        public AdminController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<admin>>> Get()
        {
            return Ok(await this.context.admin.Include(e => e.role_).Include(f => f.jobtitle_).ToListAsync());
        }

        [HttpGet("{email}")]
        public async Task<ActionResult<admin>> Get(string email)
        {
            var admin = await this.context.admin.Include(e => e.role_).Include(f => f.jobtitle_).FirstOrDefaultAsync(g => g.email == email);
            if (admin == null)
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Admin tidak ditemukan"
                });
            return Ok(admin);
        }

        [HttpPut]
        public async Task<ActionResult<List<admin>>> UpdateAdmin(editAdmin request)
        {
            var role = await this.context.roles.FindAsync(request.role_id);
            var job = await this.context.job_titles.FindAsync(request.jobtitle_id);
            var dbadmin = await this.context.admin.FindAsync(request.email);
            if (dbadmin == null)
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Admin tidak ditemukan"
                });
            dbadmin.admin_name = request.admin_name;
            dbadmin.role_ = role;
            dbadmin.jobtitle_ = job;
            dbadmin.gender = request.gender;
            dbadmin.birthdate = request.birthdate;
            dbadmin.phone_number = request.phone_number;

            await this.context.SaveChangesAsync();

            return Ok(await this.context.admin.Include(e => e.role_).Include(f => f.jobtitle_).ToListAsync());
        }

        [HttpPut("active")]
        public async Task<ActionResult<List<admin>>> UpdateActive(edit_active request)
        {
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
            var condition = false;
            var dbusercondition = await this.context.admin.Where(a => a.active == true).FirstOrDefaultAsync(e => e.email == request.email);
            if (dbusercondition != null)
            {
                dbadmin.active = condition;
                var adminf = await this.context.admin.Include(e => e.role_).Include(f => f.jobtitle_).ToListAsync();
                await this.context.SaveChangesAsync();

                return Ok(adminf);
            }
            else
            {
                dbadmin.active = true;
                var admin = await this.context.admin.Include(e => e.role_).Include(f => f.jobtitle_).ToListAsync();
                await this.context.SaveChangesAsync();

                return Ok(admin);
            }
        }

        [HttpPut("edit-password")]
        public async Task<ActionResult<List<admin>>> UpdateAdmin(editPassword request)
        {
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

            CreatePasswordHash(request.new_password, out byte[] passwordHash, out byte[] passwordSalt);
            dbadmin.passwordHash = passwordHash;
            dbadmin.passwordSalt = passwordSalt;

            await this.context.SaveChangesAsync();

            return Ok(new editPasswordSucces
            {
                Status = "Success",
                Code = "200",
                Message = "Password berhasil diubah"
            });
        }

        [HttpPut("photo")]
        public async Task<ActionResult<List<admin>>> UpdatePhotoAdmin([FromForm] editPhoto request)
        {
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
            if (request.files == null)
            {
                string url = "api/ShowImage/userimage.png";
                dbadmin.photo = url;
            }
            else if (request.files != null && request.files.Count() > 0)
            {
                foreach (var file in request.files)
                {
                    if (file != null)
                    {
                        string Filename = DateTime.Now.ToString("yyyyMMddHHmmss") + file.FileName;
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "../lmsAPI/File", Filename);
                        var stream = new FileStream(path, FileMode.Create);
                        file.CopyToAsync(stream);
                        string url = "api/ShowImage/" + Filename;
                        dbadmin.photo = url;
                    }
                }
            }
            await this.context.SaveChangesAsync();
            
            return Ok(await this.context.admin.Include(e => e.role_).Include(f => f.jobtitle_).FirstOrDefaultAsync(g => g.email == request.email));
        }

        [HttpDelete("{email}")]
        public async Task<ActionResult<admin>> Delete(string email)
        {
            var dbadmin = await this.context.admin.FindAsync(email);
            if (dbadmin == null)
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Admin tidak ditemukan"
                });

            this.context.admin.Remove(dbadmin);
            await this.context.SaveChangesAsync();

            return Ok(await this.context.admin.Include(e => e.role_).Include(f => f.jobtitle_).ToListAsync());
        }
        private void CreatePasswordHash(string new_password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(new_password));
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
