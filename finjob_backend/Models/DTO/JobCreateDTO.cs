using System.ComponentModel.DataAnnotations;

namespace finjob_backend.Models.DTO
{
    public class JobCreateDTO
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public List<int> PositionIds { get; set; }
        public string Requirement { get; set; }
        [Required]
        public List<int> LocationIds { get; set; }
        public string Benefits { get; set; }
        public string Salary { get; set; }
        [Required]
        public int CompanyID { get; set; }
    }
}
