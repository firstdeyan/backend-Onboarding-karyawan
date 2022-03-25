using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace lmsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin,superadmin,user")]
    [EnableCors]
 
    public class JobtitleController : ControllerBase
    {
       
        private readonly DataContext context;

        public JobtitleController(DataContext context)
        {
            this.context = context;
        }

      
        [HttpGet]
        public async Task<ActionResult<List<job_titles>>> Get()
        {
            return Ok(await this.context.job_titles.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<job_titles>> Get(int id)
        {
            var job = await this.context.job_titles.FindAsync(id);
            if (job == null)
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Job title tidak ditemukan"
                });
            return Ok(job);
        }

        [HttpPost]
        public async Task<ActionResult<List<job_titles>>> AddJob(job_titles job)
        {
            var dbjob = await this.context.job_titles.FindAsync(job.id);
            if (dbjob != null)
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Job title tidak ditemukan"
                });
            this.context.job_titles.Add(job);
            await this.context.SaveChangesAsync();

            return Ok(await this.context.job_titles.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<job_titles>>> UpdateJob(job_titles request)
        {
            var dbjob = await this.context.job_titles.FindAsync(request.id);
            if (dbjob == null)
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Job title tidak ditemukan"
                });
            dbjob.jobtitle_name = request.jobtitle_name;
            dbjob.jobtitle_description = request.jobtitle_description;

            await this.context.SaveChangesAsync();

            return Ok(await this.context.job_titles.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<job_titles>> Delete(int id)
        {
            var dbjob = await this.context.job_titles.FindAsync(id);
            if (dbjob == null)
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Job title tidak ditemukan"
                });

            this.context.job_titles.Remove(dbjob);
            await this.context.SaveChangesAsync();

            return Ok(await this.context.job_titles.ToListAsync());
        }

    }
}
