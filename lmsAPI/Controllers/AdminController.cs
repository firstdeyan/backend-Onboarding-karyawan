﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace lmsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    [EnableCors]
    public class AdminController : ControllerBase
    {
        private readonly DataContext context;

        public AdminController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<admin>>> Get()
        {
            return Ok(await this.context.admin.ToListAsync());
        }

        [HttpGet("{email}")]
        public async Task<ActionResult<admin>> Get(string email)
        {
            var admin = await this.context.admin.FindAsync(email);
            if (admin == null)
                return BadRequest("Admin not found.");
            return Ok(admin);
        }

        [HttpPut]
        public async Task<ActionResult<List<admin>>> UpdateAdmin(editAdmin request)
        {
            var role = await this.context.roles.FindAsync(request.role_id);
            var dbadmin = await this.context.admin.FindAsync(request.email);
            if (dbadmin == null)
                return BadRequest("Admin not found.");
            dbadmin.admin_name = request.admin_name;
            dbadmin.role_ = role;

            await this.context.SaveChangesAsync();

            return Ok("Admin edited successfully");
        }

        [HttpPut("edit-password")]
        public async Task<ActionResult<List<admin>>> UpdateAdmin(editPassword request)
        {
            var dbadmin = await this.context.admin.FindAsync(request.email);

            if (dbadmin == null)
            {
                return BadRequest("Wrong Email");
            }

            if (!VerifyPasswordHash(request.password, dbadmin.passwordHash, dbadmin.passwordSalt))
            {
                return BadRequest("Wrong password.");
            }

            CreatePasswordHash(request.new_password, out byte[] passwordHash, out byte[] passwordSalt);
            dbadmin.passwordHash = passwordHash;
            dbadmin.passwordSalt = passwordSalt;

            await this.context.SaveChangesAsync();

            return Ok("Password edited successfully");
        }

        [HttpDelete("{email}")]
        public async Task<ActionResult<admin>> Delete(string email)
        {
            var dbadmin = await this.context.admin.FindAsync(email);
            if (dbadmin == null)
                return BadRequest("Admin not found.");

            this.context.admin.Remove(dbadmin);
            await this.context.SaveChangesAsync();

            return Ok("Admin deleted successfully");
        }
        private void CreatePasswordHash(string new_password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(new_password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
