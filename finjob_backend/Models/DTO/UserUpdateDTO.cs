using Microsoft.Build.Framework;

namespace finjob_backend.Models.DTO
{
    public class UserUpdateDTO
    {
        [Required]
        public string ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Avatar { get; set; }
    }

}