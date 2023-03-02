using System.ComponentModel.DataAnnotations;

namespace finjob_backend.Models.DTO
{
    public class JobDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        [Required]
        [MaxLength(30)]
        public string Position { get; set; }
        [Required]
        public string Requirement { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string Benefits { get; set; }
        [Required]
        public string Salary { get; set; }
    }
}
