using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace finjob_backend.Models
{
    public class Job
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int CompanyID { get; set; }
        public Company Company { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Requirement { get; set; }
        public string Benefits { get; set; }
        public string Salary { get; set; }
        public ICollection<Location> Locations { get; set; }
        public ICollection<Position> Positions { get; set; }
        public ICollection<UserApplyJob> UserApplyJobs { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
