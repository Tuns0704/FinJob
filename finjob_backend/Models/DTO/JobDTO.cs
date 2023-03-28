using System.ComponentModel.DataAnnotations;

namespace finjob_backend.Models.DTO
{
    public class JobDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        [Required]
        [MaxLength(30)]
        public List<int> PositionIds { get; set; }
        [Required]
        public string Requirement { get; set; }
        [Required]
        public List<int> LocationIds { get; set; }
        [Required]
        public string Benefits { get; set; }
        [Required]
        public string Salary { get; set; }
        [Required]
        public int CompanyID { get; set; }
    }
}
