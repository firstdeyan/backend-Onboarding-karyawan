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
    public class deleteToken : ControllerBase
    {
        private readonly DataContext context;
        private readonly IConfiguration configuration;
        public static admin admin = new admin();
        public static user user = new user();

        public deleteToken(DataContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        [HttpPut]
        public async Task<ActionResult<List<admin>>> delete(lmsAPI.deleteToken request)
        {
            var dbadmin = await this.context.admin.FindAsync(request.email);
            var dbuser = await this.context.user.FindAsync(request.email);
            if (dbadmin == null && dbuser == null)
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Email tidak ditemukan"
                });

            var admin = await this.context.admin.Include(r => r.role_).Where(t => t.role_id == 1 || t.role_id == 2).FirstOrDefaultAsync(e => e.email == request.email);
            if (admin != null)
            {
                dbadmin.token = null;
                await this.context.SaveChangesAsync();
                return Ok(await this.context.admin.Include(e => e.role_).Include(f => f.jobtitle_).ToListAsync());
            }
            else
            {
                dbuser.token = null;
                await this.context.SaveChangesAsync();
                return Ok(await this.context.user.Include(e => e.role_).Include(f => f.jobtitle_).ToListAsync());
            }
            
        }
    }
}
