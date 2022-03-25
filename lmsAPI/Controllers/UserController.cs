using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace lmsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
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
            return Ok(await this.context.user.ToListAsync());
        }

        [HttpGet("{email}")]
        public async Task<ActionResult<user>> Get(string email)
        {
            var user = await this.context.user.FindAsync(email);
            if (user == null)
                return BadRequest("User not found.");
            return Ok(user);
        }

        [HttpPut]
        public async Task<ActionResult<List<user>>> UpdateUser(editUser request)
        {
            var role = await this.context.roles.FindAsync(request.role_id);
            var job = await this.context.job_titles.FindAsync(request.jobtitle_id);
            var dbuser = await this.context.user.FindAsync(request.email);
            if (dbuser == null)
                return BadRequest("User not found.");
            dbuser.name = request.name;
            dbuser.role_ = role;
            dbuser.jobtitle_ = job;
            dbuser.gender = request.gender;
            dbuser.birthdate = request.birthdate;
            dbuser.phone_number = request.phone_number;

            await this.context.SaveChangesAsync();

            return Ok("User edited successfully");
        }

        [HttpPut("edit-password")]
        public async Task<ActionResult<List<user>>> UpdateUser(editPassword request)
        {
            var dbuser = await this.context.user.FindAsync(request.email);

            if (dbuser == null)
            {
                return BadRequest("Wrong Email");
            }

            if (!VerifyPasswordHash(request.password, dbuser.passwordHash, dbuser.passwordSalt))
            {
                return BadRequest("Wrong password.");
            }

            CreatePasswordHash(request.new_password, out byte[] passwordHash, out byte[] passwordSalt);
            dbuser.passwordHash = passwordHash;
            dbuser.passwordSalt = passwordSalt;

            await this.context.SaveChangesAsync();

            return Ok("Password edited successfully");
        }

        [HttpDelete("{email}")]
        public async Task<ActionResult<user>> Delete(string email)
        {
            var dbuser = await this.context.user.FindAsync(email);
            if (dbuser == null)
                return BadRequest("user not found.");

            this.context.user.Remove(dbuser);
            await this.context.SaveChangesAsync();

            return Ok("User deleted successfully");
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
