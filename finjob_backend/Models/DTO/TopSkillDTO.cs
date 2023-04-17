using System.ComponentModel.DataAnnotations;

namespace finjob_backend.Models.DTO
{
    public class TopSkillDTO
    {
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public string Level { get; set; }
    }
}
