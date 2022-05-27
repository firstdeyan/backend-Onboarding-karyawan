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
    public class ActivityDetailController : ControllerBase
    {
        private readonly DataContext context;
        public static activity_details details = new activity_details();

        public ActivityDetailController(DataContext context)
        {
            this.context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<activity_details>>> Get()
        {
            return Ok(await this.context.activity_details.Include(e => e.activity_).ToListAsync());
        }

        [HttpGet("{activity_id}")]
        public async Task<ActionResult<activity_details>> Get(int activity_id)
        {
            var detail = await this.context.activity_details.Where(p => p.activity_id == activity_id).Include(e => e.activity_).ToListAsync();
            if (detail == null)
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Activity detail tidak ditemukan"
                });

            return Ok(detail);
        }
        

        [HttpPost]
        public async Task<ActionResult<List<activity_details>>> AddDetails([FromForm] activity_detailForm request)
        {
            int add = 1;
            int idMax = context.activity_details.Max(i => i.id);
            int newid = idMax + 1;
            details.id = newid;
            
            var activity = await this.context.activities.FindAsync(request.activity_id);
            if (request.files != null && request.files.Count() > 0)
            {
                foreach (var file in request.files)
                {
                    if (file != null)
                    {
                        string Filename = DateTime.Now.ToString("yyyyMMddHHmmss") + file.FileName;
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "../lmsAPI/File", Filename);
                        var stream = new FileStream(path, FileMode.Create);
                        file.CopyToAsync(stream);
                        if (request.detail_type == "pdf")
                        {
                            string url = "api/ShowPdf/" + Filename;
                            details.detail_link = url;
                        }
                        else if (request.detail_type == "image")
                        {
                            string url = "api/ShowImage/" + Filename;
                            details.detail_link = url;
                        }
                        else if (request.detail_type == "video")
                        {
                            string url = "api/ShowVideo/" + Filename;
                            details.detail_link = url;
                        }
                        else
                        {
                            details.detail_link = null;
                        }

                    }
                }
            }
            else
            {
                details.detail_link = null;
            }
            details.activity_ = activity;
            details.detail_name = request.detail_name;
            details.detail_desc = request.detail_desc;
            details.detail_type = request.detail_type;
            details.detail_urutan = request.detail_urutan;
            this.context.activity_details.Add(details);
            await this.context.SaveChangesAsync();
            return Ok(await this.context.activity_details.Where(a => a.activity_ == activity).Include(e => e.activity_).ToListAsync());
           
        }

        [HttpPut]
        public async Task<ActionResult<List<activity_details>>> UpdateActivityDetail([FromForm] activity_EditDetailForm request)
        {
            var dbdetail = await this.context.activity_details.FindAsync(request.id);
            var activity = await this.context.activities.FindAsync(request.activity_id);
            if (dbdetail == null)
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Activity Detail tidak ditemukan"
                });
            if (request.files != null && request.files.Count() > 0)
            {
                foreach (var file in request.files)
                {
                    if (file != null)
                    {
                        string Filename = DateTime.Now.ToString("yyyyMMddHHmmss") + file.FileName;
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "../lmsAPI/File", Filename);
                        var stream = new FileStream(path, FileMode.Create);
                        file.CopyToAsync(stream);
                        if(request.detail_type == "pdf")
                        {
                            string url = "api/ShowPdf/" + Filename;
                            dbdetail.detail_link = url;
                        }else if (request.detail_type == "image")
                        {
                            string url = "api/ShowImage/" + Filename;
                            dbdetail.detail_link = url;
                        }
                        else if (request.detail_type == "video")
                        {
                            string url = "api/ShowVideo/" + Filename;
                            dbdetail.detail_link = url;
                        }
                        else
                        {
                            dbdetail.detail_link = null;
                        }
                    }
                }
            }
            else
            {
                details.detail_link = null;
            }
            dbdetail.activity_ = activity;
            dbdetail.detail_name = request.detail_name;
            dbdetail.detail_desc = request.detail_desc;
            dbdetail.detail_type = request.detail_type;
            dbdetail.detail_urutan = request.detail_urutan;

            await this.context.SaveChangesAsync();

            return Ok(await this.context.activity_details.Include(e => e.activity_).ToListAsync());
        }
       
        [HttpDelete("{id}")]
        public async Task<ActionResult<activity_details>> Delete(int id)
        {
            var dbdetail = await this.context.activity_details.FindAsync(id);
            if (dbdetail == null)
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Activity Detial tidak ditemukan"
                });

            this.context.activity_details.Remove(dbdetail);
            await this.context.SaveChangesAsync();

            return Ok(await this.context.activity_details.Include(e => e.activity_).ToListAsync());
        }

    }
}
