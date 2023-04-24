using Microsoft.Build.Framework;

namespace finjob_backend.Models.DTO
{
    public class UserApplyCreateDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public int JobId { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public int PositionId { get; set; }
    }
}
