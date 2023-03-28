using System.ComponentModel.DataAnnotations;

namespace finjob_backend.Models.DTO
{
    public class CompanyUpdateDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Scale { get; set; }
        [Required]
        public List<int> LocationIds { get; set; }

        public string ImageURL { get; set; }
    }
}
