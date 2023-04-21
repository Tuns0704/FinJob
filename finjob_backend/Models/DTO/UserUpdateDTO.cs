using Microsoft.Build.Framework;

namespace finjob_backend.Models.DTO
{
    public class UserUpdateDTO
    {
        [Required]
        public string ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public string CompanyId { get; set; }

    }
}
