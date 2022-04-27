using Microsoft.EntityFrameworkCore;

namespace lmsAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

       /* protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<activities_owned>()
                .HasKey(g => new { g.user_, g.activities_ });
        } */
        public DbSet<job_titles> job_titles { get; set; }
        public DbSet<roles> roles { get; set; }
        public DbSet<admin> admin { get; set; }
        public DbSet<user> user { get; set; }
        public DbSet<categories> categories { get; set; }
        public DbSet<activities> activities { get; set; }
        public DbSet<activity_details> activity_details { get; set; }
        public DbSet<activities_owned> activities_owned { get; set; }
    }
}
