namespace finjob_backend.Models.DTO
{
    public class UserCreateDTO
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public int CompanyId { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public string Role { get; set; }
    }
}
