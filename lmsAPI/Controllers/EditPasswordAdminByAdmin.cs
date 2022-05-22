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
    public class EditPasswordAdminByAdmin : ControllerBase
    {
        private readonly DataContext context;

        public EditPasswordAdminByAdmin(DataContext context)
        {
            this.context = context;
        }

        [HttpPut("edit-password")]
        public async Task<ActionResult<List<admin>>> UpdatePassWordAdmin(editPasswordByAdmin request)
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
