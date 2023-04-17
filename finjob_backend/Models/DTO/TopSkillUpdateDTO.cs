using System.ComponentModel.DataAnnotations;

namespace finjob_backend.Models.DTO
{
    public class TopSkillUpdateDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Level { get; set; }
    }
}
