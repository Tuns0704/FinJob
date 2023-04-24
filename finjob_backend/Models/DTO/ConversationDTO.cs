using System.ComponentModel.DataAnnotations;

namespace finjob_backend.Models.DTO
{
    public class ConversationDTO
    {
        public int Id { get; set; }
        [Required]
        public string SenderId { get; set; }
        [Required]
        public string ReceiverId { get; set; }
    }
}
