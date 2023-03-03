using System.ComponentModel.DataAnnotations;

namespace finjob_backend.Models.DTO
{
    public class LocationCreateDTO
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
    }
}
