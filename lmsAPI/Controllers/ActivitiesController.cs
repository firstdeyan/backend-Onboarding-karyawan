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
    public class ActivitiesController : ControllerBase
    {
        private readonly DataContext context;
        public static activities activities = new activities();
        public static admin admin = new admin();
        public static user user = new user();

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
        public async Task<ActionResult<List<activities>>> AddActivity([FromForm] activitiesForm request)
        {
            var dbactivity = await this.context.activities.FindAsync(request.id);
            var category = await this.context.categories.FindAsync(request.category_id);
            if (request.files == null)
            {
                string url = "api/ShowImage/noimage.png";
                activities.cover = url;
            }
            else if (request.files != null && request.files.Count() > 0)
            {
                foreach (var file in request.files)
                {
                    if (file != null)
                    {
                        string Filename = DateTime.Now.ToString("yyyyMMddHHmmss") + file.FileName;
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "../lmsAPI/File", Filename);
                        var stream = new FileStream(path, FileMode.Create);
                        file.CopyToAsync(stream);
                        string url = "api/ShowImage/" + Filename;
                        activities.cover = url;
                    }
                }
            }
            activities.id = request.id;
            activities.activity_name = request.activity_name;
            activities.activity_description = request.activity_description;
            activities.category_ = category;
            activities.type = request.type;
            this.context.activities.Add(activities);
            await this.context.SaveChangesAsync();
            if (request.type == "home")
            {
                return Ok(await this.context.activities.Where(c => c.type == "home").Include(e => e.category_).ToListAsync());
            }
            else
            {
                return Ok(await this.context.activities.Where(c => c.type == "activity").Include(e => e.category_).ToListAsync());
            }
        }

        [HttpPut]
        public async Task<ActionResult<List<activities>>> UpdateActivity([FromForm] activitiesForm request)
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
            if (request.files == null)
            {
                string url = "api/ShowImage/noimage.png";
                dbactivity.cover = url;
            }
            else if (request.files != null && request.files.Count() > 0)
            {
                foreach (var file in request.files)
                {
                    if (file != null)
                    {
                        string Filename = DateTime.Now.ToString("yyyyMMddHHmmss") + file.FileName;
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "../lmsAPI/File", Filename);
                        var stream = new FileStream(path, FileMode.Create);
                        file.CopyToAsync(stream);
                        string url = "api/ShowImage/" + Filename;
                            dbactivity.cover = url;
                        
                    }
                }
            }
            dbactivity.activity_name = request.activity_name;
            dbactivity.activity_description = request.activity_description;
            dbactivity.category_ = category;
            dbactivity.type = request.type;
            await this.context.SaveChangesAsync();
            if (request.type == "home")
            {
                return Ok(await this.context.activities.Where(c => c.type == "home").Include(e => e.category_).ToListAsync());
            }
            else
            {
                return Ok(await this.context.activities.Where(c => c.type == "activity").Include(e => e.category_).ToListAsync());
            }
            
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<activities>>> Delete(int id)
        {
            var dbactivity = await this.context.activities.Where(c => c.type == "activity").Include(f => f.category_).FirstOrDefaultAsync(g => g.id == id);
            if (dbactivity == null)
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Activity tidak ditemukan"
                });
            var dbdetail = await this.context.activity_details.Where(p => p.activity_id == id).Include(e => e.activity_).ToListAsync();
            this.context.activities.Remove(dbactivity);
            this.context.activity_details.RemoveRange(dbdetail);
            await this.context.SaveChangesAsync();
            return Ok(await this.context.activities.Where(c => c.type == "activity").Include(e => e.category_).ToListAsync());
        }

    }
}
