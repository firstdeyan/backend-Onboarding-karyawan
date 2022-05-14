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
    public class ActivitiesOwnedById : ControllerBase
    {
        private readonly DataContext context;

        public ActivitiesOwnedById(DataContext context)
        {
            this.context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<activities_owned>> GetActivityOwnedById(int id)
        {
            var owned = await this.context.activities_owned.Where(p => p.id == id).Include(e => e.activities_).Include(u => u.user_).Include(c => c.category_).ToListAsync();
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
