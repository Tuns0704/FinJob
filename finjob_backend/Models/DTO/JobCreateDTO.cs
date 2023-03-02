using System.ComponentModel.DataAnnotations;

namespace finjob_backend.Models.DTO
{
    public class JobCreateDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        [Required]
        public string Position { get; set; }
        public string Requirement { get; set; }
        [Required]
        public string Location { get; set; }
        public string Benefits { get; set; }
        public string Salary { get; set; }
        [Required]
        public int CompanyID { get; set; }
    }
}
