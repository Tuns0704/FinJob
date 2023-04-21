using System.ComponentModel.DataAnnotations;

namespace finjob_backend.Models.DTO
{
    public class CompanyDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public string Scale { get; set; }
        public List<string> Locations { get; set; }
        public string ImageURL { get; set; }

    }
}
