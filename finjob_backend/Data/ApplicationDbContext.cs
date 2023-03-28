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
        public DbSet<LocalUser> LocalUsers { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Position> Positions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Company>()
                .HasMany<Location>(c => c.Locations)
                .WithMany(l => l.Companies);



            modelBuilder.Entity<Location>()
                .HasMany<Company>(l => l.Companies)
                .WithMany(c => c.Locations);


            /*            modelBuilder.Entity<Company>().HasData(
                          new Company
                          {
                              Id = 1,
                              Name = "MGM",
                              Description = "None",
                              Scale = "50 - 150 Employees",
                              Location = "Da Nang",
                              ImageURL = "https://itviec.com/rails/active_storage/representations/proxy/eyJfcmFpbHMiOnsibWVzc2FnZSI6IkJBaHBBK1lxREE9PSIsImV4cCI6bnVsbCwicHVyIjoiYmxvYl9pZCJ9fQ==--d01c8dc918133b1b89705dc00ef4f490b78d20e3/eyJfcmFpbHMiOnsibWVzc2FnZSI6IkJBaDdCem9MWm05eWJXRjBTU0lJY0c1bkJqb0dSVlE2RkhKbGMybDZaVjkwYjE5c2FXMXBkRnNIYVFJc0FXa0NMQUU9IiwiZXhwIjpudWxsLCJwdXIiOiJ2YXJpYXRpb24ifX0=--492f60b9aac6e8159e50e72bb289c5feb47a79d4/mgm-technology-partners-vietnam-logo.png",
                              CreatedDate = DateTime.Now
                          },
                          new Company
                          {
                              Id = 2,
                              Name = "Agility",
                              Description = "None",
                              Scale = "50 - 100 Employees",
                              Location = "Da Nang",
                              ImageURL = "https://dsa.org.vn/wp-content/uploads/2018/01/Agility.jpg",
                              CreatedDate = DateTime.Now
                          },
                          new Company
                          {
                              Id = 3,
                              Name = "Orient",
                              Description = "None",
                              Scale = "20 - 50 Employees",
                              Location = "Da Nang",
                              ImageURL = "https://images.glints.com/unsafe/glints-dashboard.s3.amazonaws.com/company-logo/2c47b1dbc75d6d9184be768c39fb9ac7.png",
                              CreatedDate = DateTime.Now
                          },
                          new Company
                          {
                              Id = 4,
                              Name = "KMS",
                              Description = "None",
                              Scale = "40 - 100 Employees",
                              Location = "Da Nang",
                              ImageURL = "https://doanhnghiep.quocgiakhoinghiep.vn/wp-content/uploads/2020/08/images-7.png",
                              CreatedDate = DateTime.Now
                          },
                          new Company
                          {
                              Id = 5,
                              Name = "NFQ",
                              Description = "None",
                              Scale = "20 - 80 Employees",
                              Location = "Da Nang",
                              ImageURL = "https://static.topcv.vn/company_logos/bUsiw4xpTeqvyGecqSrVW3Zuq0dayRwK_1656588043____83f8efe4285d433505271ef261179f41.jpeg",
                              CreatedDate = DateTime.Now
                          });*/
        }
    }
}
