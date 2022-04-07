using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lmsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin,superadmin")]
    [EnableCors]

    public class RolesController : ControllerBase
    {
        private readonly DataContext context;

        public RolesController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<roles>>> Get()
        {
            return Ok(await this.context.roles.ToListAsync());
        }

        /* [HttpGet("{id}")]
        public async Task<ActionResult<roles>> Get(int id)
        {
            var role = await this.context.roles.FindAsync(id);
            if (role == null)
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Role tidak ditemukan"
                });
            return Ok(role);
        } */

        [HttpPost]
        public async Task<ActionResult<List<roles>>> AddRole(roles role)
        {
            var dbrole = await this.context.roles.FindAsync(role.id);
            if (dbrole != null)
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Role tidak ditemukan"
                });
            this.context.roles.Add(role);
            await this.context.SaveChangesAsync();

            return Ok(await this.context.roles.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<roles>>> UpdateRole(roles request)
        {
            var dbrole = await this.context.roles.FindAsync(request.id);
            if (dbrole == null)
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Role tidak ditemukan"
                });
            dbrole.role_name = request.role_name;
            dbrole.role_description = request.role_description;
            dbrole.role_platform = request.role_platform;

            await this.context.SaveChangesAsync();

            return Ok(await this.context.roles.ToListAsync());
        }

        [HttpGet("{role_platform}")]
        public async Task<ActionResult<roles>> Get(string role_platform)
        {
            var platform = await this.context.roles.Where(p => p.role_platform == role_platform).ToListAsync();
            if (platform == null)
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "role tidak ditemukan"
                });
            return Ok(platform);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<roles>> Delete(int id)
        {
            var dbrole = await this.context.roles.FindAsync(id);
            if (dbrole == null)
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Role tidak ditemukan"
                });

            this.context.roles.Remove(dbrole);
            await this.context.SaveChangesAsync();

            return Ok(await this.context.roles.ToListAsync());
        }

    }
}
