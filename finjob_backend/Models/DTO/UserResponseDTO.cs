namespace finjob_backend.Models.DTO
{
    public class UserResponseDTO
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int CompanyId { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public string Role { get; set; }
    }
}
