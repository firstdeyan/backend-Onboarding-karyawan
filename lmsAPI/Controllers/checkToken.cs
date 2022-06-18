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
    public class checkToken : ControllerBase
    {
        private readonly DataContext context;
        private readonly IConfiguration configuration;
        public static admin admin = new admin();
        public static user user = new user();

        public checkToken(DataContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        [HttpGet("{email}")]
        public async Task<ActionResult<string>> Get(string email, string token)
        {

            var dbadmin = await this.context.admin.FindAsync(email);
            var dbuser = await this.context.user.FindAsync(email);
            if (dbadmin == null && dbuser == null)
            {
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Email tidak terdaftar"
                });
            }

            var active = true;
            var dbuserActive = await this.context.user.Where(a => a.active == active).FirstOrDefaultAsync(e => e.email == email);
            var dbadminActive = await this.context.admin.Where(a => a.active == active).FirstOrDefaultAsync(e => e.email == email); ;
            if (dbadminActive == null && dbuserActive == null)
            {
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Email tidak active"
                });
            }
            var user = await this.context.user.Where(t => t.token == token).FirstOrDefaultAsync(g => g.email == email);
            var admin = await this.context.admin.Where(t => t.token == token).FirstOrDefaultAsync(g => g.email == email);
            if (admin == null && user == null)
            {
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Token Invalid"
                });

            }
            else
            {
                return Ok(new ResponseSuccess
                {
                    Status = "success",
                    Code = "200",
                    Message = "Token Valid"
                });
            }
        }
    }
}
