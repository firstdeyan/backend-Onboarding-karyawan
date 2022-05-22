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
    public class ActivitiesByType : ControllerBase
    {
        private readonly DataContext context;

        public ActivitiesByType(DataContext context)
        {
            this.context = context;
        }

        [HttpGet("{type}")]
        public async Task<ActionResult<activities>> GetActivityByType(string type)
        {
            var activity = await this.context.activities.Where(p => p.type == type).Include(e => e.category_).ToListAsync();
            if (activity == null) { 
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Tidak ada activity"
                });
            }
            return Ok(activity);
        }
    }
}
