using finjob_backend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace finjob_backend.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<TopSkill> TopSkills { get; set; }
        public DbSet<WorkExprience> WorkExpriences { get; set; }
        public DbSet<UserApplyJob> UserApplyJobs { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Company>()
                .HasMany<Location>(c => c.Locations)
                .WithMany(l => l.Companies);


            modelBuilder.Entity<Job>()
                .HasMany<Position>(l => l.Positions)
                .WithMany(c => c.Jobs);

            modelBuilder.Entity<Company>()
                .HasMany<ApplicationUser>(u => u.Users)
                .WithOne(c => c.Company);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany<TopSkill>(t => t.TopSkills)
                .WithOne(u => u.User);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany<WorkExprience>(u => u.WorkExpriences)
                .WithOne(u => u.User);
        }
    }
}