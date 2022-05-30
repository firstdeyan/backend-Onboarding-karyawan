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
    public class EditPasswordUserByAdmin : ControllerBase
    {
        private readonly DataContext context;

        public EditPasswordUserByAdmin(DataContext context)
        {
            this.context = context;
        }

        [HttpPut("edit-password")]
        public async Task<ActionResult<List<user>>> UpdatePassWordUser(editPasswordByAdmin request)
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
            
            CreatePasswordHash(request.new_password, out byte[] passwordHash, out byte[] passwordSalt);
            dbuser.passwordHash = passwordHash;
            dbuser.passwordSalt = passwordSalt;

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
