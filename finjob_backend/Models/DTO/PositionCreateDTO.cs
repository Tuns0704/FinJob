using System.ComponentModel.DataAnnotations;

namespace finjob_backend.Models.DTO
{
    public class PositionCreateDTO
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
