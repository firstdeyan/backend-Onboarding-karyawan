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
    public class ActivityCategoryController : ControllerBase
    {
        private readonly DataContext context;

        public ActivityCategoryController(DataContext context)
        {
            this.context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<categories>>> Get()
        {
            return Ok(await this.context.categories.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<categories>> Get(int id)
        {
            var category = await this.context.categories.FindAsync(id);
            if (category == null)
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Activity Category tidak ditemukan"
                });
            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<List<categories>>> AddCategory(categories job)
        {
            var dbcategory = await this.context.categories.FindAsync(job.id);
            if (dbcategory != null)
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Activity Category tidak ditemukan"
                });
            this.context.categories.Add(job);
            await this.context.SaveChangesAsync();

            return Ok(await this.context.categories.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<categories>>> Updatecategory(categories request)
        {
            var dbcategory = await this.context.categories.FindAsync(request.id);
            if (dbcategory == null)
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Activity Category tidak ditemukan"
                });
            dbcategory.category_name = request.category_name;
            dbcategory.category_description = request.category_description;

            await this.context.SaveChangesAsync();

            return Ok(await this.context.categories.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<categories>> Delete(int id)
        {
            var dbcategory = await this.context.categories.FindAsync(id);
            if (dbcategory == null)
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Activity Category tidak ditemukan"
                });
            var activity = await this.context.activities.Include(c => c.category_).Where(i => i.category_id == id).FirstOrDefaultAsync();
            if (activity != null)
                return BadRequest(new Response
                {
                    Status = "error",
                    ErrorCode = "400",
                    ErrorMessage = "Activity Category tidak dapat dihapus karena dimiliki Activity"
                });

            this.context.categories.Remove(dbcategory);
            await this.context.SaveChangesAsync();

            return Ok(await this.context.categories.ToListAsync());
        }
    }
}
