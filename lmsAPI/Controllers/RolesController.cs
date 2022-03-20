using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace lmsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    
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

        [HttpGet("{id}")]
        public async Task<ActionResult<roles>> Get(int id)
        {
            var role = await this.context.roles.FindAsync(id);
            if (role == null)
                return BadRequest("Role not found.");
            return Ok(role);
        }

        [HttpPost]
        public async Task<ActionResult<List<roles>>> AddRole(roles role)
        {
           var dbrole = await this.context.roles.FindAsync(role.id);
            if (dbrole != null)
                return BadRequest("id already exits");
            this.context.roles.Add(role);
            await this.context.SaveChangesAsync();

            return Ok("Role added successfully");
        }

        [HttpPut]
        public async Task<ActionResult<List<roles>>> UpdateRole(roles request)
        {
            var dbrole = await this.context.roles.FindAsync(request.id);
            if (dbrole == null)
                return BadRequest("Role not found.");
            dbrole.role_name = request.role_name;
            dbrole.role_description = request.role_description;
            dbrole.role_platform = request.role_platform;

            await this.context.SaveChangesAsync();

            return Ok("Role edited successfully");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<roles>> Delete(int id)
        {
            var dbrole = await this.context.roles.FindAsync(id);
            if (dbrole == null)
                return BadRequest("Role not found.");

            this.context.roles.Remove(dbrole);
            await this.context.SaveChangesAsync();

            return Ok("Role deleted successfully");
        }

    }
}
