using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace lmsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin,superadmin,user,mentor")]
    [EnableCors]
    public class UserController : ControllerBase
    {
        private readonly DataContext context;

        public UserController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<user>>> Get()
        {
            return Ok(await this.context.user.Include(e => e.role_).Include(f => f.jobtitle_).ToListAsync());
        }

        [HttpGet("{email}")]
        public async Task<ActionResult<user>> Get(string email)
        {
            var user = await this.context.user.Include(e => e.role_).Include(f => f.jobtitle_).FirstOrDefaultAsync(g=>g.email == email);
            if (user == null)
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "user tidak ditemukan"
                });
            return Ok(user);
        }

        [HttpPut]
        public async Task<ActionResult<List<user>>> UpdateUser(editUser request)
        {
            var role = await this.context.roles.FindAsync(request.role_id);
            var job = await this.context.job_titles.FindAsync(request.jobtitle_id);
            var dbuser = await this.context.user.FindAsync(request.email);
            if (dbuser == null)
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "user tidak ditemukan"
                });
            dbuser.name = request.name;
            dbuser.role_ = role;
            dbuser.jobtitle_ = job;
            dbuser.gender = request.gender;
            dbuser.birthdate = request.birthdate;
            dbuser.phone_number = request.phone_number;

            await this.context.SaveChangesAsync();

            return Ok(await this.context.user.Include(e => e.role_).Include(f => f.jobtitle_).ToListAsync());
        }

        [HttpPut("edit-password")]
        public async Task<ActionResult<List<user>>> UpdateUser(editPassword request)
        {
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

            CreatePasswordHash(request.new_password, out byte[] passwordHash, out byte[] passwordSalt);
            dbuser.passwordHash = passwordHash;
            dbuser.passwordSalt = passwordSalt;

            await this.context.SaveChangesAsync();

            return Ok(new editPasswordSucces { 
                Status = "Success",
                Code = "200",
                Message = "Password berhasil diubah"
            });
        }

        [HttpPut("finishedActivities")]
        public async Task<ActionResult<List<user>>> UpdateFinishedActivties(edit_finishedActivities request)
        {
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

            dbuser.finishedActivities = request.finishedActivities;
            var user = await this.context.user.Where(e => e.email == request.email).Include(e => e.role_).Include(f => f.jobtitle_).ToListAsync();
            await this.context.SaveChangesAsync();

            return Ok(user);
        }

        [HttpPut("assignedActivities")]
        public async Task<ActionResult<List<user>>> UpdateAssignedActivties(edit_assignedActivities request)
        {
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

            dbuser.assignedActivities = request.assignedActivities;
            var user = await this.context.user.Where(e => e.email == request.email).Include(e => e.role_).Include(f => f.jobtitle_).ToListAsync();
            await this.context.SaveChangesAsync();

            return Ok(user);
        }

        [HttpPut("progress")]
        public async Task<ActionResult<List<user>>> UpdateProgress(edit_progress request)
        {
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

            dbuser.progress = request.progress;
            var user = await this.context.user.Where(e => e.email == request.email).Include(e => e.role_).Include(f => f.jobtitle_).ToListAsync();
            await this.context.SaveChangesAsync();

            return Ok(user);
        }

        [HttpPut("active")]
        public async Task<ActionResult<List<user>>> UpdateActive(edit_active request)
        {
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
            var condition = false;
            var dbusercondition = await this.context.user.Where(a => a.active == true).FirstOrDefaultAsync(e => e.email == request.email);
            if (dbusercondition != null)
            {
                dbuser.active = condition;
                await this.context.SaveChangesAsync();
                var userf = await this.context.user.Include(e => e.role_).Include(f => f.jobtitle_).ToListAsync();
                return Ok(userf);
            }
            else
            {
                dbuser.active = true;
                var user = await this.context.user.Include(e => e.role_).Include(f => f.jobtitle_).ToListAsync();
                await this.context.SaveChangesAsync();

                return Ok(user);
            }
        }

        [HttpDelete("{email}")]
        public async Task<ActionResult<user>> Delete(string email)
        {
            var dbuser = await this.context.user.FindAsync(email);
            if (dbuser == null)
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "user tidak ditemukan"
                });

            this.context.user.Remove(dbuser);
            await this.context.SaveChangesAsync();

            return Ok(await this.context.user.Include(e => e.role_).Include(f => f.jobtitle_).ToListAsync());
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
