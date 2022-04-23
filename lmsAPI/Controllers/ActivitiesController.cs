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
    public class ActivitiesController : ControllerBase
    {
        private readonly DataContext context;
        public static activities activities = new activities();

        public ActivitiesController(DataContext context)
        {
            this.context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<activities>>> Get()
        {
            return Ok(await this.context.activities.Include(e => e.category_).ToListAsync());
        }

        [HttpGet("{category_id}/{id}")]
        public async Task<ActionResult<activities>> Get(int category_id, int id)
        {
            var activity = await this.context.activities.Where(p => p.category_id == category_id).Where(g => g.id == id).Include(e => e.category_).ToListAsync();
            if (activity == null)
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Activity tidak ditemukan"
                });

            return Ok(activity);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<activities>> Get(int id)
        {
            var activity = await this.context.activities.Include(f => f.category_).FirstOrDefaultAsync(g => g.id == id);
            if (activity == null)
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Activity tidak ditemukan"
                });

            return Ok(activity);
        }

        [HttpPost]
        public async Task<ActionResult<List<activities>>> AddActivity(activitiesForm request)
        {
            var dbactivity = await this.context.activities.FindAsync(request.id);
            var category = await this.context.categories.FindAsync(request.category_id);
            activities.id = request.id;
            activities.activity_name = request.activity_name;
            activities.activity_description = request.activity_description;
            activities.category_ = category;
            this.context.activities.Add(activities);
            await this.context.SaveChangesAsync();

            return Ok(await this.context.activities.Include(e => e.category_).ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<activities>>> UpdateActivity(activitiesForm request)
        {
            var dbactivity = await this.context.activities.FindAsync(request.id);
            var category = await this.context.categories.FindAsync(request.category_id);
            if (dbactivity == null)
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Activity tidak ditemukan"
                });
            dbactivity.activity_name = request.activity_name;
            dbactivity.activity_description = request.activity_description;
            dbactivity.category_ = category;

            await this.context.SaveChangesAsync();

            return Ok(await this.context.activities.Include(e => e.category_).ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<activities>> Delete(int id)
        {
            var dbactivity = await this.context.activities.FindAsync(id);
            if (dbactivity == null)
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Activity tidak ditemukan"
                });

            this.context.activities.Remove(dbactivity);
            await this.context.SaveChangesAsync();

            return Ok(await this.context.activities.Include(e => e.category_).ToListAsync());
        }
    }
}
