namespace finjob_backend.Models
{
    public class UserApplyJob
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int JobId { get; set; }
        public Job Job { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public int PositionId { get; set; }
        public Position Position { get; set; }

    }
}
