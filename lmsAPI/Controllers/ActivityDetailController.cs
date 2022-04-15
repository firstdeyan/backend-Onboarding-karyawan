﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace lmsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin,superadmin,user")]
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
        public async Task<ActionResult<List<activity_details>>> AddActivityDetail(activity_detailForm request)
        {
            var dbdetail = await this.context.activity_details.FindAsync(request.id);
            var activity = await this.context.activities.FindAsync(request.activity_id);
            details.id = request.id;
            details.activity_ = activity;
            details.detail_name = request.detail_name;
            details.detail_desc = request.detail_desc;
            details.detail_link = request.detail_link;
            details.detail_type = request.detail_type;
            details.detail_urutan = request.detail_urutan;  
            this.context.activity_details.Add(details);
            await this.context.SaveChangesAsync();

            return Ok(await this.context.activity_details.Include(e => e.activity_).ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<activity_details>>> UpdateActivityDetail(activity_detailForm request)
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
            dbdetail.activity_ = activity;
            dbdetail.detail_name = request.detail_name;
            dbdetail.detail_desc = request.detail_desc;
            dbdetail.detail_link = request.detail_link;
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