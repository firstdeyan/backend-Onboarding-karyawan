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
    public class ActivitiesByCategory : ControllerBase
    {
        private readonly DataContext context;

        public ActivitiesByCategory(DataContext context)
        {
            this.context = context;
        }

        [HttpGet("{category_id}")]
        public async Task<ActionResult<activities>> GetActivityByCategory(int category_id)
        {
            var activity = await this.context.activities.Where(p => p.category_id == category_id).Include(e => e.category_).ToListAsync();
            if (activity == null)
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Tidak ada activity"
                });

            return Ok(activity);
        }
    }
}
