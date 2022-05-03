using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace lmsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin,superadmin,user")]
    [EnableCors]
    public class UsersByRole : ControllerBase
    {
        private readonly DataContext context;

        public UsersByRole(DataContext context)
        {
            this.context = context;
        }

        [HttpGet("{role_id}")]
        public async Task<ActionResult<activities>> GetUsersByRole(int role_id)
        {
            var users = await this.context.user.Where(p => p.role_id == role_id).Include(e => e.role_).Include(j => j.jobtitle_).ToListAsync();
            if (users == null)
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Tidak ada user"
                });

            return Ok(users);
        }
    }
}
