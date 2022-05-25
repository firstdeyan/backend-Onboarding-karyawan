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
    public class ActivitiesDelete : ControllerBase
    {
        private readonly DataContext context;

        public ActivitiesDelete(DataContext context)
        {
            this.context = context;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<activities>>> Delete(int id)
        {
            var dbactivity = await this.context.activities.Where(c => c.type == "home").Include(f => f.category_).FirstOrDefaultAsync(g => g.id == id);
            if (dbactivity == null)
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Activity tidak ditemukan"
                });

            this.context.activities.Remove(dbactivity);
            await this.context.SaveChangesAsync();
            return Ok(await this.context.activities.Where(c => c.type == "home").Include(e => e.category_).ToListAsync());
        }
    }
}
