namespace finjob_backend.Models.DTO
{
    public class UserApplyDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Job { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
    }
}
