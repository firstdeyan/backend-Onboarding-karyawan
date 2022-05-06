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
    public class ActivitiesOwnedByCategory : ControllerBase
    {
        private readonly DataContext context;

        public ActivitiesOwnedByCategory(DataContext context)
        {
            this.context = context;
        }

        [HttpGet("{user_email}/{category_id}")]
        public async Task<ActionResult<activities_owned>> GetActivityOwnedByCategory(string user_email, int category_id)
        {
            var owned = await this.context.activities_owned.Where(u => u.user_email == user_email).Where(p => p.category_id == category_id).Include(e => e.activities_).Include(u => u.user_).Include(c => c.category_).ToListAsync();
            if (owned == null)
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Tidak ada activity owned"
                });

            return Ok(owned);
        }

        [HttpGet("{user_email}/{category_id}/{status}")]
        public async Task<ActionResult<activities_owned>> GetAOByCategoryByStatus(string user_email, int category_id, string status)
        {
            var activities_owned = await this.context.activities_owned.Where(u => u.user_email == user_email).Where(p => p.category_id == category_id).Where(f => f.status == status).Include(e => e.user_).Include(f => f.activities_).Include(c => c.category_).ToListAsync();
            if (activities_owned == null)
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Tidak ada activities owned"
                });

            return Ok(activities_owned);
        }
    }
}
