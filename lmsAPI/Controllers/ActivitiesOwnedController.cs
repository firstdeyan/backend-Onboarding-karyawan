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
    public class ActivitiesOwnedController : ControllerBase
    {
        private readonly DataContext context;
        public static activities_owned activities_owned = new activities_owned();

        public ActivitiesOwnedController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<activities_owned>>> Get()
        {
            return Ok(await this.context.activities_owned.Include(e => e.user_).Include( f => f.activities_).Include(c => c.category_).ToListAsync());
        }

        [HttpGet("{user_email}")]
        public async Task<ActionResult<activities_owned>> GetAOByUserEmail(string user_email)
        {
            var activities_owned = await this.context.activities_owned.Where(p => p.user_email == user_email).Include(e => e.user_).Include(f => f.activities_).Include(c => c.category_).ToListAsync();
            if (activities_owned == null)
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Tidak ada activities owned"
                });

            return Ok(activities_owned);
        }

        [HttpGet("{user_email}/{status}")]
        public async Task<ActionResult<activities_owned>> GetAOByUserEmailByStatus(string user_email, string status)
        {
            var activities_owned = await this.context.activities_owned.Where(p => p.user_email == user_email).Where(f => f.status == status).Include(e => e.user_).Include(f => f.activities_).Include(c => c.category_).ToListAsync();
            if (activities_owned == null)
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Tidak ada activities owned"
                });

            return Ok(activities_owned);
        }

        [HttpPost]
        public async Task<ActionResult<List<activities_owned>>> AddActivitisOwned(activities_owned_form request)
        {
            var dbactivityowned = await this.context.activities_owned.FindAsync(request.id);
            var dbuser = await this.context.user.FindAsync(request.user_email);
            if (dbuser == null)
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Email user tidak ada"
                });
            var dbactivity = await this.context.activities.FindAsync(request.activity_id);
            var dbcategory = await this.context.categories.FindAsync(request.category_id);
            /* var dbmentor = await this.context.user.Where(g => g.email == request.mentor_email).Where(p => p.role_id == 3).ToListAsync();
            if (dbmentor == null)
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Email mentor tidak ada"
                }); */
            activities_owned.id = request.id;
            activities_owned.user_ = dbuser;
            activities_owned.activities_ = dbactivity;
            activities_owned.category_ = dbcategory;
            activities_owned.start_date = request.start_date;
            activities_owned.end_date = request.end_date;
            activities_owned.status = request.status;
            activities_owned.late = request.late;
            activities_owned.mentor_email = request.mentor_email;
            activities_owned.activity_note = request.activity_note;
            this.context.activities_owned.Add(activities_owned);
            await this.context.SaveChangesAsync();

            return Ok(await this.context.activities_owned.Include(e => e.user_).Include(f => f.activities_).Include(c => c.category_).ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<activities_owned>>> UpdateActivitiesOwned(activities_owned_form request)
        {
            var dbactivityowned = await this.context.activities_owned.FindAsync(request.id);
            if (dbactivityowned == null)
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Activities Owned tidak ditemukan"
                });
            var dbuser = await this.context.user.FindAsync(request.user_email);
            if (dbuser == null)
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Email user tidak ada"
                });
            var activity = await this.context.activities.FindAsync(request.activity_id);
            var category = await this.context.categories.FindAsync(request.category_id);
            /* var dbmentor = await this.context.user.Where(g => g.email == request.mentor_email).Where(p => p.role_id == 3).ToListAsync();
            if (dbmentor == null)
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Email mentor tidak ada"
                }); */
            dbactivityowned.user_ = dbuser;
            dbactivityowned.activities_ = activity;
            dbactivityowned.category_ = category;
            dbactivityowned.start_date = request.start_date;
            dbactivityowned.end_date = request.end_date;
            dbactivityowned.status = request.status;
            dbactivityowned.late = request.late;
            dbactivityowned.mentor_email = request.mentor_email;
            dbactivityowned.activity_note = request.activity_note;

            await this.context.SaveChangesAsync();

            return Ok(await this.context.activities_owned.Include(e => e.user_).Include(f => f.activities_).Include(c => c.category_).ToListAsync());
        }

        [HttpPut("status")]
        public async Task<ActionResult<List<activities_owned>>> UpdateAOStatus(edit_status request)
        {
            var dbactivityowned = await this.context.activities_owned.FindAsync(request.id);
            if (dbactivityowned == null)
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Activities Owned tidak ditemukan"
                });
            var dbuser = await this.context.user.FindAsync(request.user_email);
            if (dbuser == null)
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Email user tidak ada"
                });
            
            dbactivityowned.status = request.status;
            var activity_owned = await this.context.activities_owned.Where(g => g.id == request.id).Include(e => e.user_).Include(f => f.activities_).Include(c => c.category_).ToListAsync();

            await this.context.SaveChangesAsync();

            return Ok(activity_owned);
        }

        [HttpPut("duedate")]
        public async Task<ActionResult<List<activities_owned>>> UpdateAODuedate(edit_duedate request)
        {
            var dbactivityowned = await this.context.activities_owned.FindAsync(request.id);
            if (dbactivityowned == null)
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Activities Owned tidak ditemukan"
                });
            var dbuser = await this.context.user.FindAsync(request.user_email);
            if (dbuser == null)
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Email user tidak ada"
                });

            dbactivityowned.start_date = request.start_date;
            dbactivityowned.end_date = request.end_date;
            var activity_owned = await this.context.activities_owned.Where(g => g.id == request.id).Include(e => e.user_).Include(f => f.activities_).Include(c => c.category_).ToListAsync();
            await this.context.SaveChangesAsync();

            return Ok(activity_owned);
        }

        [HttpPut("mentor-email")]
        public async Task<ActionResult<List<activities_owned>>> UpdateAOMentorEmail (edit_mentor_email request)
        {
            var dbactivityowned = await this.context.activities_owned.FindAsync(request.id);
            if (dbactivityowned == null)
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Activities Owned tidak ditemukan"
                });
            var dbuser = await this.context.user.FindAsync(request.user_email);
            if (dbuser == null)
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Email user tidak ada"
                });

            dbactivityowned.mentor_email = request.mentor_email;
            var activity_owned = await this.context.activities_owned.Where(g => g.id == request.id).Include(e => e.user_).Include(f => f.activities_).Include(c => c.category_).ToListAsync();
            await this.context.SaveChangesAsync();

            return Ok(activity_owned);
        }

        [HttpPut("activity-note")]
        public async Task<ActionResult<List<activities_owned>>> UpdateAOActivityNote(edit_activity_note request)
        {
            var dbactivityowned = await this.context.activities_owned.FindAsync(request.id);
            if (dbactivityowned == null)
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Activities Owned tidak ditemukan"
                });
            var dbuser = await this.context.user.FindAsync(request.user_email);
            if (dbuser == null)
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Email user tidak ada"
                });

            dbactivityowned.activity_note = request.activity_note;
            var activity_owned = await this.context.activities_owned.Where(g => g.id == request.id).Include(e => e.user_).Include(f => f.activities_).Include(c => c.category_).ToListAsync();
            await this.context.SaveChangesAsync();

            return Ok(activity_owned);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<activities_owned>> Delete(int id)
        {
            var dbactivityowned = await this.context.activities_owned.FindAsync(id);
            if (dbactivityowned == null)
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Activity Owned tidak ditemukan"
                });

            this.context.activities_owned.Remove(dbactivityowned);
            await this.context.SaveChangesAsync();

            return Ok(await this.context.activities_owned.Include(e => e.user_).Include(f => f.activities_).Include(c => c.category_).ToListAsync());
        }
    }
}
