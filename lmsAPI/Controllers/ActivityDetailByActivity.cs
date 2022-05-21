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
    public class ActivityDetailByActivity : ControllerBase
    {
        private readonly DataContext context;
        public static activity_details details = new activity_details();

        public ActivityDetailByActivity(DataContext context)
        {
            this.context = context;
        }

     [HttpDelete("{activity_id}")]
        public async Task<ActionResult<activity_details>> Delete(int activity_id)
        {
            var dbdetail = await this.context.activity_details.Where(p => p.activity_id == activity_id).Include(e => e.activity_).ToListAsync();
            if (dbdetail == null)
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Activity Detail tidak ditemukan"
                });

            this.context.activity_details.RemoveRange(dbdetail);
            await this.context.SaveChangesAsync();

            return Ok(await this.context.activity_details.Include(e => e.activity_).ToListAsync());
        }

    }
}
