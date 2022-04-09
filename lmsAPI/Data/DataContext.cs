using Microsoft.EntityFrameworkCore;

namespace lmsAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<job_titles> job_titles { get; set; }
        public DbSet<roles> roles { get; set; }
        public DbSet<admin> admin { get; set; }
        public DbSet<user> user { get; set; }
        public DbSet<categories> categories { get; set; }
    }
}
