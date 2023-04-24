using System.ComponentModel.DataAnnotations;

namespace finjob_backend.Models.DTO
{
    public class JobDTO
    {
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        [MaxLength(30)]
        public List<string> Positions { get; set; }
        public List<int> PositionIds { get; set; }
        [Required]
        public string Requirement { get; set; }
        [Required]
        public List<string> Locations { get; set; }
        [Required]
        public string Benefits { get; set; }
        [Required]
        public string Salary { get; set; }
        [Required]
        public int CompanyID { get; set; }
    }
}
