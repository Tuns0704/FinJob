using Microsoft.AspNetCore.Identity;

namespace finjob_backend.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string? Avatar { get; set; }
        public int? CompanyId { get; set; }
        public Company? Company { get; set; }
        public ICollection<Job> Jobs { get; set; }
        public ICollection<WorkExprience> WorkExpriences { get; set; }
        public ICollection<TopSkill> TopSkills { get; set; }
        public ICollection<UserApplyJob> UserApplyJobs { get; set; }
    }
}
