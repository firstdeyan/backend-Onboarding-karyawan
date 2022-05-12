using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace lmsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin,superadmin,user,mentor")]
    [EnableCors]
    public class ActivitiesOwnedByStatus : ControllerBase
    {
        private readonly DataContext context;

        public ActivitiesOwnedByStatus(DataContext context)
        {
            this.context = context;
        }

        [HttpGet("{status}")]
        public async Task<ActionResult<activities_owned>> GetActivityOwnedByStatus(string status)
        {
            var owned = await this.context.activities_owned.Where(f => f.status == status).Include(e => e.activities_).Include(u => u.user_).Include(c => c.category_).ToListAsync();
            if (owned == null)
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Tidak ada activity owned"
                });

            return Ok(owned);
        }
    }
}
