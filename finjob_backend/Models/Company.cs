using System.ComponentModel.DataAnnotations;

namespace finjob_backend.Models
{
    public class Company
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string ?Name { get; set; }
        public string ?Description { get; set; }
        public string ?Scale { get; set; }
        public string ?Location { get; set; }
    }
}
